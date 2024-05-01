using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject WinnerScreen;

    private int score = 0;

    public void IncrementScore()
    {
        score++;
        UpdateScoreUI();
        SpecificScore();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
        

    }

    public void SpecificScore()
    {
        if (score >= 25)
        {
            WinnerScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}