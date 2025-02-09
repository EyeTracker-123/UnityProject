using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorName : MonoBehaviour
{
    public string doorNumber;
    void Start()
    {
        doorNumber = gameObject.name;
    }
}
