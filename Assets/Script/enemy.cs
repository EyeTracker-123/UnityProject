using System;
using System.Collections;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;


    //追いかける対象

    private GameObject player;

    [SerializeField]
    private searchController searchTrigger;
    
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    
    [SerializeField]
    private RawImage backimage;
    Color currentColor;
    private GameObject gameManager;
    private Animator animator;
    private Camera camera;
    private GameObject eye;
    private GameManager gameManagerScript;
    [SerializeField]
    int fadeBackgroundSpeed = 0;
    [SerializeField]
    private float searchWaitTime = 1.5f;
    [SerializeField]
    private float zoomFOV = 5f;
    [SerializeField]
    private float zoomSpeed = 1.5f;
    [SerializeField]
    private float alpha = 0;
    
    private bool isWaiting = false;

    private bool isTouched = false;

    Color backgroundColor;

    void Awake()
    {
        
    }
    void Start(){
        //searchTriggerに触れた際と出て行った時に関数が起動するように設定
        searchTrigger.OnTriggerEntered += SetKillMode;
        searchTrigger.OnTriggerExited += OutKillMode;

        //virtualCamera  = transform.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        //virtualCamera.gameObject.SetActive(false);
        eye = GameObject.Find("目玉");
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");
        //mainCameraの取得
        camera = Camera.main;

        backgroundColor = backimage.color;
        backimage.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, alpha);
        Debug.Log(backimage.color);
                 
       //ui.FadeTo(0f,1f);
        
        //フラグが立つまで非表示
        gameObject.SetActive(false);

    }
    
        //backimage.color = new Color(c.r, c.g, c.b, alpha);

     void OnEnable()
    {
        //有効時に追跡フラグを立てる
        isWaiting = false;
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
            checkDistance();

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

    void checkDistance()
    {
        //ｘ、ｚの差分の合計を計算（Mathf.Absは絶対値に変換）
        float diffX = Mathf.Abs(transform.position.x - player.transform.position.x);
        float diffZ = Mathf.Abs(transform.position.z - player.transform.position.z);
        float totalDiff = diffX + diffZ;

        if (totalDiff < 3f)
        {
            Debug.Log("xとzの差分の合計が5を下回りました");
            
            
            //gameManagerを停止させて敵のリスポーンを防ぐ
            gameManager.SetActive(false);

            //カメラとプレイヤーを動かせない様にする
            player.SetActive(false);

            //背景を非表示
            backimage.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, 0);
            //virtualCameraがついているgameObjectを有効化
            //virtualCamera.gameObject.SetActive(true);
            virtualCamera.Priority = 20;

            //追跡処理を停止させる
            isTouched = true;

            //捕縛アニメーション実行
            animator.SetBool("isGeting",true);

            StartCoroutine(wait());
            //Mathf.Lerp （現在、目的値、移動にかかる時間）time.deltaTimeはfpsに依存させないようにするおまじない
            //virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);
            //virtualCamera.Priority = 20;
            //player（カメラ）の座標を正面にする
            //camera.transform.LookAt(eye.transform);


        

            agent.isStopped = true;


            
        }
        else if(totalDiff < 30.0f)
        {
            

            //alphaを更新することでLerpが取る値が少しずつ目的値（今回は１）に近づける
            alpha = Mathf.Lerp(alpha,1,Time.deltaTime * fadeBackgroundSpeed);
            backimage.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, alpha);

            Debug.Log(backimage.color);
            //ui.FadeTo(1f,2f);

            //float alpha = 1.0f;
            
            /*float newAlpha = Mathf.Lerp(backgroundColor.a, targetAlpha, Time.deltaTime * (targetAlpha / 10));
            Debug.Log(newAlpha);
            backimage.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);*/
        }
        else
        {
            alpha = Mathf.Lerp(alpha,0,Time.deltaTime * fadeBackgroundSpeed);
            backimage.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, alpha);
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        //アニメーションが実行されてから時間を空けてカメラをズーム
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);
        yield return new WaitForSeconds(4f);
        //現在のシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    /*private IEnumerator FadeAlphaCoroutine(float targetAlpha, float duration)
    {
        Color currentColor = backimage.color;
        float startAlpha = currentColor.a;
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            backimage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        backimage.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }*/
    void OutKillMode(Collider other){

        agent.speed = 3.5f;

        agent.angularSpeed = 120.0f;

        searchWaitTime = 1.5f;
    }

    IEnumerator searchWait(){
        //プレイヤーに触れたら追跡を辞める
        if(isTouched == true)
        {
            Debug.Log("speed0");
            agent.SetDestination(transform.position);
            // 目的地をリセットして止める
            //agent.ResetPath();
            yield break;
        }

        agent.SetDestination(player.transform.position);
        
        yield return new WaitForSeconds(searchWaitTime);
        
        isWaiting = false;

    }
}