using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Transform player;

    private float startY;
    private float currentScore;

    public float CurrentScore => currentScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (player != null)
        {
            startY = player.position.y;
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = player.position.y - startY;
        currentScore = Mathf.Max(0f, distance);
    }

    public string GetFormattedScore()
    {
        return currentScore.ToString("F2");
    }
}
