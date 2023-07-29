using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Sphere1 : MonoBehaviour
{
    private AudioSource _audioSource;

    private Vector3 _randomRotation;

    public ScoreManager scoreManager;

    public GameSessionManager gameSessionManager;

    //private int targetId;

    public int targetNum;

    public float moveSpeed = 40f;
    public float moveDistance = 300f;

    private bool isMovingForward = true;
    private Vector3 initialPosition;
    public GameData gameData;
    //private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        gameSessionManager = FindObjectOfType<GameSessionManager>();
        gameData = gameSessionManager.gameData;

        if (scoreManager == null || gameSessionManager == null)
        {
            Debug.LogError("ScoreManager or GameSessionManager not found in the scene!");
        }

        gameData.sphereHits = new List<SphereHitData>();

        initialPosition = transform.position;
    }


    private void Update()
    {
        // Calculate the target position based on the current direction of movement
        Vector3 targetPosition;
        if (isMovingForward)
        {
            targetPosition = initialPosition + Vector3.back * moveDistance;
        }
        else
        {
            targetPosition = initialPosition;
        }

        // Move the sphere towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If the sphere has reached the target position, toggle the direction of movement
        if (transform.position == targetPosition)
        {
            isMovingForward = !isMovingForward;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        gameSessionManager = FindObjectOfType<GameSessionManager>();

        if (other.gameObject.CompareTag("Bullet"))
        {
            scoreManager.UpdateScore(20);
            // Destroy(other.gameObject);
            Destroy(gameObject);

            SphereHitData hitData1 = gameSessionManager.gameData.sphereHits.Find(data => data.hitID == targetNum);

            if (hitData1 == null)
            {
                hitData1 = new SphereHitData();
                hitData1.hitID = targetNum; 
                gameSessionManager.gameData.sphereHits.Add(hitData1);   
            }

            hitData1.sphereTime = Time.time - gameSessionManager.startTime;
            Debug.Log("Sphere ID: " + hitData1.hitID + ", Hit Time: " + hitData1.sphereTime);   
        }
    }

}
