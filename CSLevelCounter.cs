using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CSLevelCounter : MonoBehaviour
{
    public static CSLevelCounter instance;
    public Player player;

    public TMP_Text csText;
    public int currentCSLevel = 1;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        csText.text = currentCSLevel.ToString();
    }

    public void IncreaseCSLevel(int amount)
    {
        currentCSLevel += amount;
        csText.text = currentCSLevel.ToString();
    }

    private void Update()
    {
        currentCSLevel = player.CSLevel;
        csText.text = currentCSLevel.ToString();
    }
}
