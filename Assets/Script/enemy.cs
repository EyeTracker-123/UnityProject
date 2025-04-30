using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    //追いかける対象

    private GameObject player;

    [SerializeField]
    private searchController searchTrigger;
    
    private GameObject gameManager;
    private Camera camera;
    private GameObject eye;
    private GameManager gameManagerScript;
    
    [SerializeField]
    private float searchWaitTime = 1.5f;

    private bool isWaiting = false;

    private bool isTouched = false;

   
    void Start(){
        //searchTriggerに触れた際と出て行った時に関数が起動するように設定している
        searchTrigger.OnTriggerEntered += SetKillMode;
        searchTrigger.OnTriggerExited += OutKillMode;

        eye = GameObject.Find("目玉");
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");
        //mainCameraの取得
        camera = Camera.main;
        // オブジェクトからスクリプト（コンポーネント）を取得
        //gameManagerScript = gameManager.GetComponent<GameManager>();

            
        
        gameObject.SetActive(false);

    }
     void OnEnable()
    {
        isWaiting = false;
        //audioSource.PlayOneShot(audioClip);

    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) // プレイヤーに当たったら
        {
            
            // スクリプトを無効化（非アクティブ化）
            //gameManagerScript.enabled = false;
            //敵のリスポーンを防ぐ
            gameManager.SetActive(false);
            //カメラとプレイヤーを動かせない様にする
            player.SetActive(false);
            
            isTouched = true;
            //player（カメラ）の座標を正面にする
            camera.transform.LookAt(eye.transform);

            Debug.Log("playerに触れました");
        }
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
            if(isTouched == true)
            {

            }
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
        //プレイヤーに触れたら追跡を辞める
        if(isTouched == true)
        {
            agent.speed = 0;
            // 目的地をリセットして止める
            //agent.ResetPath();
            yield break;
        }

        agent.SetDestination(player.transform.position);
        
        yield return new WaitForSeconds(searchWaitTime);
        
        isWaiting = false;

    }
}