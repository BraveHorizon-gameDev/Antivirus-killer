using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject worldSelectPanel;
    public GameObject creditsPanel;

    void Start()
    {
        ShowMainMenu();
    }

    public void StartGame()
    {
        // SceneManager.LoadScene("MOTHERBOARD ARENA");
        mainMenuPanel.SetActive(false);
        worldSelectPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log(message: "Quit Game");
    }

    public void ShowCredits()
    {
        mainMenuPanel.SetActive(value: false);
        creditsPanel.SetActive(value: true);
    }

    public void BackToMenu()
    {
        creditsPanel.SetActive(value: false);
        worldSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(value: true);
    }

    void ShowMainMenu()
    {
        mainMenuPanel.SetActive(value: true);
        creditsPanel.SetActive(value: false);
    }

    public void LoadMotherboard()
    {
        SceneManager.LoadScene("MOTHERBOARD ARENA");
    }

    public void LoadStick()
    {
        SceneManager.LoadScene("STICK ARENA");
    }
}
