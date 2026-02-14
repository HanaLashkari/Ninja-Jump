using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Update()
    {
        Debug.Log("UI Running");

        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Score:" + ScoreManager.Instance.GetFormattedScore();

        }
    }
}
