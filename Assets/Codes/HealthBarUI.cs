using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(float current, float max)
    {
        slider.value = current / max;
    }
}
