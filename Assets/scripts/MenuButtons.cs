using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public AudioSource clickSound; 

    public void StartGame()
    {
        if (clickSound != null) clickSound.Play();
        Invoke(nameof(LoadGameScene), 0.2f);
        Debug.Log("Start clicked");
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        if (clickSound != null)
            clickSound.Play();

        Debug.Log("Exit clicked");

        Invoke(nameof(QuitGame), 0.2f);
    }

    private void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
