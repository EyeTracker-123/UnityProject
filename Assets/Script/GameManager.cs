using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform Playertf;

    private List<GameObject> gameObjects = new List<GameObject>();

    private GameObject[] allObjects;
    void Start()
    {
        allObjects = FindObjectsOfType<GameObject>(true);

        foreach (GameObject obj in allObjects) {
            if (!obj.name.Contains("‘å•”‰®"))
            {

            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
