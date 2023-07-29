using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    [SerializeField] TextMeshProUGUI scoreText;

    void Update()
    {
        // Check if the game time has exceeded 90 seconds
        if (Time.timeSinceLevelLoad >= 90f || score >= 160)
        {
            // End the game and show the final score
            Debug.Log("Game over! Final score: " + score);
            scoreText.text = " Final Score: " + score;
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
}
