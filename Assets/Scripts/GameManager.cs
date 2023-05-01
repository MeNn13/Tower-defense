using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isPlaying = true;

    public void PauseGame()
    {
        if (isPlaying)
        {
            Time.timeScale = 1.0f;
            isPlaying = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            isPlaying = true;
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
