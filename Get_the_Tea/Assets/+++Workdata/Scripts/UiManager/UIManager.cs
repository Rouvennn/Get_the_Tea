using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Health")]
    public Image[] heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Collectibles")]
    public TMP_Text collectibleText;

    [Header("Score")]
    public TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void UpdateHealth(int current, int max)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < current)
                heartImages[i].sprite = fullHeart;
            else
                heartImages[i].sprite = emptyHeart;

            heartImages[i].enabled = i < max;
        }
    }

    public void UpdateCollectibles(int collected, int total)
    {
        collectibleText.text = $"{collected} / {total}";
    }

    public void UpdateScore(int points)
    {
        scoreText.text = $"Score: {points}";
    }
}
