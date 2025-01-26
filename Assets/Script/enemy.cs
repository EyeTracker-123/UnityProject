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

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;

    private bool isWaiting = false;

    void Start(){
        gameObject.SetActive(false);

    }
     void OnEnable()
    {
        isWaiting = false;
        //audioSource.PlayOneShot(audioClip);

    }
    void OnDisable()
    {
        //audioSource.Stop();
    }
    /// <summary>
    /// 移動量が一定量超えたら追いかけモードに変更
    /// 追いかけモードは一定時間で解除
    /// </summary>
    void Update()
    {
       
        //追跡中の動作
        if (agent.isOnNavMesh)
        {
            //agent.isStopped = false;
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
        
            //agent.isStopped = true; // 停止
            Debug.Log("停止中");
        }

    }



    IEnumerator searchWait(){
        
        agent.SetDestination(player.position);

        yield return new WaitForSeconds(2f);
        isWaiting = false;

    }
}