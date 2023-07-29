using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.Shapes;

public class GameSessionManager : MonoBehaviour
{
    public GameData gameData;
    public float startTime;

    private const float GAME_TIME_LIMIT = 90f;
    public static readonly int SCORE_LIMIT = 160;
    private ScoreManager scoreManager;

    void Start()
    {
        Debug.Log("Script attached to game object: " + gameObject.name);
        scoreManager = FindObjectOfType<ScoreManager>();
        gameData = new GameData();
        gameData.targetHits = new List<TargetHitData>();
        startTime = Time.time;

        // Print the persistent data path to the console
        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
    }

    void Update()
    {
        if (Time.time - startTime >= GAME_TIME_LIMIT || scoreManager.score >= SCORE_LIMIT)
        {
            // Game session is over, serialize game data to JSON
            gameData.timeTaken = Time.time - startTime;
            gameData.score = scoreManager.score; // add this line to store the final score
            string json = JsonUtility.ToJson(gameData);

            string filePath;
            if (Application.isEditor)
            {
                filePath = Application.dataPath + "/game_data.json";
            }
            else
            {
                filePath = Application.persistentDataPath + "/game_data.json";
            }
            File.WriteAllText(filePath, json);
            Debug.Log("File written to: " + filePath);

            // Restart the game or go to the game over screen
            Time.timeScale = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");
        if (other.gameObject.CompareTag("Bullet"))
        {
            // Get the target hit data from game data
            Target target = other.gameObject.GetComponent<Target>();
            //Sphere1 sphere = other.gameObject.GetComponent<Sphere1>();
            int targetId = target.targetNumber;
            //Target target = GetComponent<Target>();
            //int targetId = target.targetNumber;
            TargetHitData hitData = gameData.targetHits.Find(data => data.targetId == targetId);

            Sphere1 sphere = other.gameObject.GetComponent<Sphere1>();
            int sphereId = sphere.targetNum;
            SphereHitData hitData1 = gameData.sphereHits.Find(data => data.hitID == sphereId);

            // If target hit data does not exist, create a new one
            if (hitData == null)
            {
                hitData = new TargetHitData();
                hitData.targetId = targetId;
                gameData.targetHits.Add(hitData);
            }

            // Update the hit time of the target hit data
            hitData.hitTime = Time.time - startTime;

            // Print the target hit data to the console
            Debug.Log("Target ID: " + hitData.targetId + ", Hit Time: " + hitData.hitTime);


            if (hitData1 == null)
            {
                hitData1 = new SphereHitData();
                hitData1.hitID = sphereId;
                gameData.sphereHits.Add(hitData1);
            }

            hitData1.sphereTime = Time.time - startTime;

            Debug.Log("Sphere ID: " + hitData1.hitID + ", Hit Time: " + hitData1.sphereTime);
        }
    }
}