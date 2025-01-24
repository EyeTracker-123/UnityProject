using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform Playertf;

   
    void Start()
    {
        //すべてのオブジェクトを取得＞”大部屋”が含まれるオブジェクトを抽出＞配列に変換
        //gameObjects = FindObjectsOfType<GameObject>(true).Where(obj => obj.name.Contains("大部屋(")).ToList();
        /*foreach(GameObject obj in gameObjects){
            Debug.Log(obj.name);
        }*/
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
