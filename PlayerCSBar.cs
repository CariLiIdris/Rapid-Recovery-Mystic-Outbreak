using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCSBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdatePlayerCSBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    public void SetMaxCS(int CS)
    {
        slider.maxValue = CS;
        slider.value = CS;
    }

    public void SetCS(int CS)
    {
        slider.value = CS;
    }
}
