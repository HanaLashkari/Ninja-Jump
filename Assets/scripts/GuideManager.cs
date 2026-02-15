using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideManager : MonoBehaviour
{
    public AudioSource clickSound;

    public void OnMenu()
    {
        Debug.Log("Menu clicked");
        if (clickSound != null) clickSound.Play();
        Invoke(nameof(LoadMenuScene), 0.2f);
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}