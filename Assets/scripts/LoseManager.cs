using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class LoseManager : MonoBehaviour
{
    public TMP_Text currentScoreText;
    public TMP_Text bestScoreText;
    public AudioSource clickSound;
    private string bestScoreFilePath;
    private float currentScore;
    private float bestScore;

    private void Awake()
    {
        bestScoreFilePath = Path.Combine(Application.persistentDataPath, "bestScore.txt");

        if (!File.Exists(bestScoreFilePath))
        {
            File.WriteAllText(bestScoreFilePath, "0");
            bestScore = 0;
        }
        else
        {
            string content = File.ReadAllText(bestScoreFilePath);
            if (!float.TryParse(content, out bestScore))
                bestScore = 0;
        }
    }

    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            currentScore = ScoreManager.Instance.CurrentScore;
        }
        else
        {
            currentScore = 0;
        }

        if (currentScoreText != null)
            currentScoreText.text = "Score: " + currentScore.ToString("F2");

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            File.WriteAllText(bestScoreFilePath, bestScore.ToString("F2"));
        }

        if (bestScoreText != null)
            bestScoreText.text = "Best Score: " + bestScore.ToString("F2");
    }

    public void OnTryAgain()
    {
        if (clickSound != null) clickSound.Play();
        Invoke(nameof(LoadGameScene), 0.2f);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMenu()
    {
        if (clickSound != null) clickSound.Play();
        Invoke(nameof(LoadMenuScene), 0.2f);
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
