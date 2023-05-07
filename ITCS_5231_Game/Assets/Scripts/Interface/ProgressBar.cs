using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public Checkpoints checkpoint;

    public void DisplayProgressFull(int progress)
    {
      slider.maxValue = progress;
      slider.value = progress;
    }

    public void DisplayProgress(int progress)
    {
      slider.value = progress;
    }

}
