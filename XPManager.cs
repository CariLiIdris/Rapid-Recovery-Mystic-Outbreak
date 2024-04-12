using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager instance;

    public delegate void XPChangeHandler(int amount);
    public event XPChangeHandler OnXPChange;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void AddXP(int amount)
    {
        OnXPChange?.Invoke(amount);
    }
}
