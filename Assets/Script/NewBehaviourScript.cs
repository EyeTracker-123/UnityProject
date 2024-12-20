using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public testScript ts;
    // Start is called before the first frame update
    void Start()
    {
        ts = GetComponent<testScript>();
        Debug.Log(ts.nc);
    }
}
