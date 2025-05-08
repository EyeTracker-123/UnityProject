using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject enemy;  
    [SerializeField]
    private NavMeshAgent agent;
    
    [SerializeField]
    private Transform Playertf;
    private string tagName = "open";
    [SerializeField]
    private int moveThreshold;
    [SerializeField]
    private int followTime;
    private float playerMoveAmount = 0;
    Vector3 pyPosition ;


    private List<GameObject> gameObjects = new List<GameObject>();

    Vector3 tempPosition;
    Vector3 appearPoint;
    private bool isFollowing = false;
    
    void Start(){
        
        tempPosition = Playertf.position;
        //enemy = GameObject.Find("enemy");

        
    }
    /// <summary>
    /// 移動量が一定量超えたら追いかけモードに変更
    /// 追いかけモードは一定時間で解除
    /// </summary>
    void Update()
    {
        setFlag();

        if(playerMoveAmount > moveThreshold){
            SetEnemyPosition();
            StartCoroutine(setMode());
        }
       
    }

    void setFlag(){
        pyPosition = Playertf.position;
        //移動量の加算と追跡フラグの管理
        if(playerMoveAmount > moveThreshold ){
            playerMoveAmount = 0;
            
            isFollowing = true;
            
        }
        else if(isFollowing == false){
            //四捨五入した値を絶対値にして代入
           
            playerMoveAmount += Math.Abs((int)Math.Round(pyPosition.x) - (int)Math.Round(tempPosition.x));
            playerMoveAmount += Math.Abs((int)Math.Round(pyPosition.z) - (int)Math.Round(tempPosition.z));
//            Debug.Log(playerMoveAmount);
        }

         tempPosition = Playertf.position;
      
    }

    IEnumerator setMode(){
        
        yield return new WaitForSeconds(followTime);
        enemy.SetActive(false);
        isFollowing = false;
    }

    void SetEnemyPosition(){
        gameObjects.Clear();
        
        //.where＝条件に当てはまるものだけ取得
        gameObjects = FindObjectsOfType<GameObject>().Where
        (obj => obj.name.Contains("大部屋(") && obj.CompareTag(tagName))
        .ToList();

        

        //OrderByDescending = 値が大きい順に並べ替え
        GameObject furthestObject = gameObjects.OrderByDescending
        //.Destance＝二つのオブジェクトを比較し、差分をとる
        (obj => Vector3.Distance(Playertf.position, obj.transform.position))
        //リストの最初を取得
        .FirstOrDefault();

        enemy.SetActive(true);

        appearPoint = new Vector3(furthestObject.transform.position.x, 1.4f,furthestObject.transform.position.z);
        agent.Warp(appearPoint);

    }
    void startMusic(){

    }

    void stopMusic(){
        
    }
    
    
        //すべてのオブジェクトを取得＞”大部屋”が含まれるオブジェクトを抽出＞配列に変換
        //gameObjects = FindObjectsOfType<GameObject>(true).Where(obj => obj.name.Contains("大部屋(")).ToList();
        /*foreach(GameObject obj in gameObjects){
            Debug.Log(obj.name);
        }*/
        
        
        
  }
