using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchEyeDate : MonoBehaviour
{
    public TETConnect eyeTracker;
    
    // Start is called before the first frame update
    void Start()
    {
        eyeTracker = GetComponent<TETConnect>();
    }

    // Update is called once per frame
    void Update()
    {
        bool cate = eyeTracker.TET.values.frame.fix;
        Debug.Log(cate);
    }
}
