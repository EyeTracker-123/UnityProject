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
    private searchController searchTrigger;

    private float searchWaitTime = 1.5f;

    private bool isWaiting = false;

   
    void Start(){
        searchTrigger.OnTriggerEntered += SetKillMode;
        searchTrigger.OnTriggerExited += OutKillMode;
        gameObject.SetActive(false);

    }
     void OnEnable()
    {
        isWaiting = false;
        //audioSource.PlayOneShot(audioClip);

    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("playerに衝突した"); 
        }
        
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
       
        if (agent.isOnNavMesh)
        {

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
          Debug.Log("Navmeshに乗ってない");
        }

    }

    void SetKillMode(Collider other){
        if (other.CompareTag("Player"))
        {
            // 移動速度を設定
            agent.speed = 30.0f; // 単位: メートル/秒

            // 旋回速度を設定
            agent.angularSpeed = 720.0f; // 単位: 度/秒

            searchWaitTime = 0f;
        }
        
    }

    void OutKillMode(Collider other){

        agent.speed = 3.5f;

        agent.angularSpeed = 120.0f;

        searchWaitTime = 1.5f;
    }

    IEnumerator searchWait(){
        
        agent.SetDestination(player.position);

        yield return new WaitForSeconds(searchWaitTime);
        isWaiting = false;

    }
}