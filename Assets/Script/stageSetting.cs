/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageSetting : MonoBehaviour
{
    private GameObject playerObj;// = FindAnyObjectByType("player");
    float objectPos;
    float playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("player");

        objectPos = gameObject.transform.position.x * gameObject.transform.position.x
            + gameObject.transform.position.z * gameObject.transform.position.z;
    }
    private void Update()
    {
        playerPos = playerObj.transform.position.x * playerObj.transform.position.x
            + playerObj.transform.position.z * playerObj.transform.position.z;

        if (playerPos - objectPos < 100)
        {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}*/
