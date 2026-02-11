using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public AudioSource clickSound; 

    public void StartGame()
    {
        if (clickSound != null)
            clickSound.Play();

        SceneManager.LoadScene("Game");
        Debug.Log("Start clicked");
    }

    public void ExitGame()
    {
        if (clickSound != null)
            clickSound.Play();

        Debug.Log("Exit clicked");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
