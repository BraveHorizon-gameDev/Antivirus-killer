using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    private bool _isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<PlayerRaycast>().canShoot = false;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<PlayerRaycast>().canShoot = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}