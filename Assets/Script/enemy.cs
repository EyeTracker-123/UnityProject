using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject visibility;
    private CapsuleCollider cc;
    // Start is called before the first frame update
    void Awake()
    {
        cc = visibility.GetComponent<CapsuleCollider>();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
