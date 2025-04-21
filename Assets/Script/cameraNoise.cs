using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class cameraNoise : MonoBehaviour
{
    
    private int count = 0;
    public int doFrame;
    private float rand_1 = 0.0f;
    quaternion cr;
   // private  randpos = this.
    void Start()
    {
       cr = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if(count%doFrame == 0)
        {
            rand_1 = UnityEngine.Random.Range(-1.0f,1.0f);
            cr = Quaternion.Euler(rand_1,rand_1,0);
        }
    }
}
