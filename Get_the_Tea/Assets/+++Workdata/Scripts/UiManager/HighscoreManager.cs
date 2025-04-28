using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class HighscoreEntry
{
    public string playerName;
    public int score;

    public HighscoreEntry(string name, int score)
    {
        playerName = name;
        this.score = score;
    }
}

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance;

    [Header("UI References")]
    public TMP_InputField nameInputField;
    public TMP_Text highscoreText;
    public GameObject gameOverPanel;

    private List<HighscoreEntry> highscores = new List<HighscoreEntry>();
    private const int maxHighscores = 10;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadHighscores();
    }

    public void OnPlayerDeath()
    {
        gameOverPanel.SetActive(true);
    }

    public void SubmitScore()
    {
        string name = nameInputField.text.ToUpper();
        if (string.IsNullOrEmpty(name)) name = "---"; // fallback

        name = name.Length > 3 ? name.Substring(0, 3) : name; // Clamp to 3 letters

        highscores.Add(new HighscoreEntry(name, RoomManager.Instance.points));
        highscores.Sort((a, b) => b.score.CompareTo(a.score)); // Sort descending

        if (highscores.Count > maxHighscores)
            highscores.RemoveAt(highscores.Count - 1); // Keep top 10 only

        SaveHighscores();
        UpdateHighscoreDisplay();
    }

    private void UpdateHighscoreDisplay()
    {
        highscoreText.text = "";

        for (int i = 0; i < highscores.Count; i++)
        {
            HighscoreEntry entry = highscores[i];
            highscoreText.text += $"{i + 1}. {entry.playerName} - {entry.score}\n";
        }
    }

    private void SaveHighscores()
    {
        for (int i = 0; i < highscores.Count; i++)
        {
            PlayerPrefs.SetString($"HighscoreName{i}", highscores[i].playerName);
            PlayerPrefs.SetInt($"HighscoreScore{i}", highscores[i].score);
        }
        PlayerPrefs.Save();
    }

    private void LoadHighscores()
    {
        highscores.Clear();
        for (int i = 0; i < maxHighscores; i++)
        {
            string name = PlayerPrefs.GetString($"HighscoreName{i}", "---");
            int score = PlayerPrefs.GetInt($"HighscoreScore{i}", 0);

            if (score > 0)
                highscores.Add(new HighscoreEntry(name, score));
        }

        highscores.Sort((a, b) => b.score.CompareTo(a.score));
        UpdateHighscoreDisplay();
    }

    public void RestartGame()
    {
        RoomManager.Instance.GetNextRoom();
        RoomManager.Instance.points = 0;
        gameOverPanel.SetActive(false);
    }
}
