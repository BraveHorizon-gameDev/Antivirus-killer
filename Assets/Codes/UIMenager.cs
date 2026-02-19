using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header(header: "Health")]
    public Slider healthSlider;

    [Header(header: "Wave")]
    public TextMeshProUGUI waveText;

    [Header(header: "Game Over")]
    public GameObject gameOverPanel;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(obj: gameObject);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }

    public void UpdateHealth(float current, float max)
    {
        if (healthSlider != null)
            healthSlider.value = current / max;
        else
            Debug.LogWarning(message: "Health Slider is null!");
    }

    public void UpdateWave(int wave)
    {
        if (waveText != null)
            waveText.text = "WAVE " + wave.ToString();
        else
            Debug.LogWarning(message: "WaveText is null!");
    }
}