using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public Slider slider;
    public EnemyVitals enemy;

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
