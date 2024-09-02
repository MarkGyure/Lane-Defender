using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // For file operations
using Newtonsoft.Json; // Using Newtonsoft.Json for JSON serialization/deserialization
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int currentScore = 0;
    public int highScore = 0;

    private string saveFilePath;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    void Start()
    {
        // Determine the file path for saving the score data
        saveFilePath = Path.Combine(Application.persistentDataPath, "highscore.json");

        // Load the high score at the start
        LoadHighScore();
        UpdateScoreUI();
    }

    // Method to increment the score
    public void AddScore()
    {
        currentScore++;
        CheckHighScore(); // Check and save high score after score changes
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;
    }

    // Method to check and update the high score
    private void CheckHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }
    }

    // Save the high score to a file using JSON
    private void SaveHighScore()
    {
        HighScoreData data = new HighScoreData { highScore = highScore };
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(saveFilePath, jsonData);
    }

    // Load the high score from the file
    private void LoadHighScore()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            HighScoreData data = JsonConvert.DeserializeObject<HighScoreData>(jsonData);
            highScore = data.highScore;
        }
    }
}

// Data structure to store high score
[System.Serializable]
public class HighScoreData
{
    public int highScore;
}
