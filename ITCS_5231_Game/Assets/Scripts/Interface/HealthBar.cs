using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerVitals player;

    public void DisplayMaxHealth(float hp)
    {
      slider.maxValue = hp;
      slider.value = hp;
    }

    public void DisplayHealth(float hp)
    {
      slider.value = hp;
    }

}
