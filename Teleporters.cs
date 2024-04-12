using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporters : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private Transform GetDestination()
    {
        return destination;
    }
}
