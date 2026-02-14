using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Transform player;

    private float startY;
    private float maxY;
    private float currentScore;

    public float CurrentScore => currentScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
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
            maxY = startY;
        }
    }

    private void Update()
    {
        if (player == null) return;

        PlayerContoroller controller = player.GetComponent<PlayerContoroller>();

        if (controller != null && controller.start)
            return;

        if (player.position.y > maxY)
        {
            maxY = player.position.y;
        }

        currentScore = maxY - startY;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            ResetScore();
        }
    }

    private void ResetScore()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            startY = player.position.y;
            maxY = startY;
            currentScore = 0f;
        }
    }

    public string GetFormattedScore()
    {
        return currentScore.ToString("F2");
    }
}
