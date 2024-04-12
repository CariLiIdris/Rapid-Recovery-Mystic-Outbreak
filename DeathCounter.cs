using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class DeathCounter : MonoBehaviour
{
    public static DeathCounter instance;

    public TMP_Text deathText;
    public int currentDeaths = 0;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        deathText.text = "X" + currentDeaths.ToString();
    }

    public void IncreaseDeaths(int amount)
    {
        currentDeaths += amount;
        deathText.text = "X" + currentDeaths.ToString();
    }
}
