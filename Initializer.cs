using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

public static class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void Execute()
    {
        Debug.Log("Loaded by the Persist Object from the Initializer Script");
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSISTOBJECTS")));
    }
}
