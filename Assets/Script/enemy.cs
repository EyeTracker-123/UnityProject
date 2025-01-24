using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    //追いかける対象
    [SerializeField]
    private Transform player;
    private float playerMoveAmount = 0;
    Vector3 pyPosition ;

    Vector3 tempPosition;
    private bool isFollowing = false;
    private bool isWaiting = false;
    [SerializeField]
    private int moveThreshold;
    void Start(){
        
        tempPosition = player.position;

    }
    /// <summary>
    /// 移動量が一定量超えたら追いかけモードに変更
    /// 追いかけモードは一定時間で解除
    /// </summary>
    void Update()
    {
        pyPosition = player.position;
        //移動量の加算と追跡フラグの管理
        if(playerMoveAmount > moveThreshold ){
            playerMoveAmount = 0;
            
            isFollowing = true;
            StartCoroutine(modeTime());
        }
        else if(isFollowing == false){
            //四捨五入した値を絶対値にして代入
            //Debug.Log(player.position.x);
            //Debug.Log(tempPosition.x);
            playerMoveAmount += Math.Abs((int)Math.Round(pyPosition.x) - (int)Math.Round(tempPosition.x));
            playerMoveAmount += Math.Abs((int)Math.Round(pyPosition.z) - (int)Math.Round(tempPosition.z));
            Debug.Log(playerMoveAmount);
        }

        //追跡中の動作
        if (agent.isOnNavMesh && isFollowing == true)
        {
            agent.isStopped = false;
            // プレイヤーの位置に向かって移動
                if (isWaiting == false)
                {
                    
                    isWaiting = true;
                    StartCoroutine(searchWait());
                    Debug.Log("追跡中");
                }
            
        }
        else
        {
        
            agent.isStopped = true; // 停止
            Debug.Log("停止中");
        }

        tempPosition = player.position;
    }

    IEnumerator searchWait(){
        
        agent.SetDestination(player.position);

        yield return new WaitForSeconds(2f);
        isWaiting = false;

    }
    IEnumerator modeTime(){

        yield return new WaitForSeconds(5f);

        isFollowing = false;
    }
}