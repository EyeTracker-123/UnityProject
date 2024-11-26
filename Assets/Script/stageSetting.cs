using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageSetting : MonoBehaviour
{
    private GameObject player;// = FindAnyObjectByType("player");
    float objectPos;
    float playerPos;
    // Start is called before the first frame update
    void Start()
    {
        objectPos = gameObject.transform.position.x * gameObject.transform.position.x
            + gameObject.transform.position.z * gameObject.transform.position.z;
        playerPos = player.transform.position.x * player.transform.position.x
            + player.transform.position.z * player.transform.position.z;
    }
    private void Update()
    {
        if (playerPos - objectPos < 100)
        {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
