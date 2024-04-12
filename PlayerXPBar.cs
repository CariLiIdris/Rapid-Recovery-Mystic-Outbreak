using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdatePlayerXPBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    public void SetMaxXP(int XP)
    {
        slider.maxValue = XP;
        slider.value = XP;
    }

    public void SetXP(int XP)
    {
        slider.value = XP;
    }
}
