using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    private Animator door_animation;
    private GameObject hitobj;
    Vector3 _velocity;
    Vector3 _camera;
    public float move_speed;
    private float ms;
    public float stamina = 1;
    public bool dashflag = false;
    public GameObject[] stagePos;
    private int ii = 0;

    public Text textObj;

    private void Awake()
    {
        // Player Input����InputAction���擾���܂�
        var playerInput = GetComponent<PlayerInput>();
        holdAction = playerInput.actions["Run"];

        // �{�^����������n�߂����Ɨ����ꂽ���ɃC�x���g��o�^���܂�
        holdAction.started += OnHoldStarted;
        holdAction.canceled += OnHoldCanceled;
        ms = move_speed;



        for(int i=0;i<stagePos.Length;i++)
        {
            if(Math.Abs(gameObject.transform.position.z - 
            stagePos[i].transform.position.z) < 50)
            {
                stagePos[i].SetActive(true);
            }
            else
            {
                stagePos[i].SetActive(false);
            }
            //Debug.Log(stagePos[i]);
        }
        //Debug.Log(stagePos.Length);
    }

    public Camera cam;
    public float cameraSpeed = 5f;
    public Transform target;  
    private Vector3 offset = new Vector3(0,0,0);
    private float camera_x;
    private float camera_y;

    Vector3 cam_forward = new Vector3(0, 0, 0);
    Vector3 cam_right = new Vector3(0, 0, 0);

    private float xRotation;
    public Vector3 _came = new Vector3(0, 0, 0);

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stage"))
        {
            other.gameObject.SetActive(true);
        }
    }*/

    public bool isStairsend = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stairsend"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,collision.transform.position.y+1.5f, gameObject.transform.position.z);

        }
    }

   




    void LateUpdate()
    {
        // �J�������^�[�Q�b�g�ɒǏ]������
        cam.transform.position = target.position + offset;
    }
    // ��ŏ��� public float x = 0.01f;

    Rigidbody rb;
    
    void Update()
    {
       
        _came.x += (_camera.x * cameraSpeed) * -1;
        _came.y += _camera.y * cameraSpeed;

        cam_forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        cam_right = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;

        gameObject.transform.localPosition += (cam_right * _velocity.x + cam_forward * _velocity.z) * ms;

        //����U�ۗ�
        //_came.x = Mathf.Clamp(_came.x, -90, 90);
        // _came.y = Mathf.Clamp(_came.y, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(_came);
        gameObject.transform.localRotation = Quaternion.Euler(0,_came.y,0);

       // target.Rotate(Vector3.up * camera_x);

//        Debug.Log(ii);

        float posdiffZ = Math.Abs(gameObject.transform.position.z - 
            stagePos[ii].transform.position.z);

        float possdiffX = Math.Abs(gameObject.transform.position.x - 
            stagePos[ii].transform.position.x);

        if(posdiffZ < 50 && possdiffX < 50)stagePos[ii].SetActive(true);
        else stagePos[ii].SetActive(false);

        if(ii < stagePos.Length-1)ii++;
        else ii=0;

        

        //�X�^�~�i��1.0��荂���Ȃ������ɁA1�ɖ߂�����
        if (dashflag == false)
        {
            if (stamina == 1) return;
            if (stamina > 1)
            {
                stamina = 1;
            }
            else
            {
                stamina += 0.003f;
            }
        }

       
        //����ŏ�������
        //gameObject.transform.localPosition += _velocity * move_speed;
        

    }
    Vector3 moveDirection;

    public float speed = 3f;
    public float gravity = 10f;
    public float jumpSpeed = 5f;

    float PlayerY;
  
    void OnMove(InputValue value)
    {
        
        var axis = value.Get<Vector2>();
      
            _velocity = new Vector3(axis.x, 0, axis.y);
        
    }
    void OnLook(InputValue value)
    {
        var ori = value.Get<Vector2>();
        _camera = new Vector3(ori.y, ori.x, 0);
        //camera_x = ori.x;
       //amera_y = ori.y;
        //xRotation -= camera_y;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
       
    }
    
    private InputAction holdAction;

    private void OnDestroy()
    {
        // �C�x���g�̉���
        holdAction.started -= OnHoldStarted;
        holdAction.canceled -= OnHoldCanceled;
    }

    private void OnHoldStarted(InputAction.CallbackContext context)
    {
        // �{�^����������n�߂�����s
        
        StartCoroutine(HoldRoutine());
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        // �{�^���������ꂽ���~
        
        StopAllCoroutines();
        ms = move_speed;
        dashflag = false;
    }

    private IEnumerator HoldRoutine()
    {
        while (true)
        {            
            if(stamina > 0)
            {

                //�X�^�~�i������Ȃ���ړ����x���グ��
                ms = move_speed*2;
                stamina -= 0.002f;
                dashflag = true;
                yield return null;

            }
            else
            {
                //�X�^�~�i��0�����ɂȂ�̂�j�~���A�ړ����x��߂�
                ms = move_speed;
                stamina = 0;
                yield return null;
            }
        }
    }

    public float distance = 5.0f;

    public bool A = false;
    public bool B = false;
    public bool C = false;
    public bool D = false;
    public bool E = false;
    public bool F = false;
    public bool G = false;
    string tKey;
    void OnFire()//アイテムやドア"など"のアクションはこの中で書く
    {
        RaycastHit hit;
        

        Vector3 direction = cam.transform.forward;
        if (Physics.Raycast(cam.transform.position,direction,out hit, distance))
        {
            hitobj = hit.collider.gameObject;
            if (hitobj.CompareTag("key"))
            {
                keyFlag kf = hitobj.GetComponent<keyFlag>();
                string kn = kf.keyname;
               
                switch (kn)
                {
                    case "keyA":
                        A = true;
                        tKey = "A";
                        hitobj.SetActive(false);
                        StartCoroutine("getKey");
                        break;

                    case "keyB":
                        B = true;
                        tKey = "B";
                        hitobj.SetActive(false);
                        StartCoroutine("getKey");
                        break;

                    case "keyC":
                        C = true;
                        tKey = "C";
                        hitobj.SetActive(false);
                        StartCoroutine("getKey");
                        break;

                    case "keyD":
                        D = true;
                        tKey = "D";
                        hitobj.SetActive(false);
                        StartCoroutine("getKey");
                        break;

                    case "keyE":
                        E = true;
                        tKey = "E";
                        hitobj.SetActive(false);
                        StartCoroutine("getKey");
                        break;

                    case "keyF":
                        F = true;
                        tKey = "F";
                        hitobj.SetActive(false);
                        StartCoroutine("getKey");
                        break;

                    case "keyG":
                        G = true;
                        tKey = "G";
                        hitobj.SetActive(false);
                        StartCoroutine("getKey");
                        break;
                }
            }
            if (hitobj.CompareTag("keydoor"))
            {
                doorName dn = hitobj.GetComponent<doorName>();
                string D_Number = dn.doorNumber;

                void opendoor()
                {
                    StartCoroutine("noKey");
                    //アニメーションが終わるまで待機
                    StartCoroutine("waitanime");
                }

                switch (D_Number)
                {
                    case "doorA":
                        tKey = "A";
                        if(A == true)
                        {
                            opendoor();
                        }
                        else
                        {
                            StartCoroutine("otherKey");
                        }
                        break;

                    case "doorB":
                        tKey = "B";
                        if (B == true)
                        {
                            opendoor();
                        }
                        else
                        {
                            StartCoroutine("otherKey");
                        }
                        break;

                    case "doorC":
                        tKey = "C";
                        if (C == true)
                        {
                            opendoor();
                        }
                        else
                        {
                            StartCoroutine("otherKey");
                        }
                        break;

                    case "doorD":
                        tKey = "D";
                        if (D == true)
                        {
                            opendoor();
                        }
                        else
                        {
                            StartCoroutine("otherKey");
                        }
                        break;

                    case "doorE":
                        tKey = "E";
                        if (E == true)
                        {
                            opendoor();
                        }
                        else
                        {
                            StartCoroutine("otherKey");
                        }
                        break;

                    case "doorF":
                        tKey = "F";
                        if (F == true)
                        {
                            opendoor();
                        }
                        else
                        {
                            StartCoroutine("otherKey");
                        }
                        break;

                    case "doorG":
                        tKey = "G";
                        if (G == true)
                        {
                            opendoor();
                        }
                        else
                        {
                            StartCoroutine("otherKey");
                        }
                        break;
                }
            }
        }
    }
    IEnumerator otherKey()
    {
        Text textArea = textObj.GetComponent<Text>();
        textArea.text = tKey+"の鍵が必要だ";
        yield return new WaitForSeconds(5);
        textArea.text = " ";
        yield break;
    }
    IEnumerator noKey()
    {
        Text textArea = textObj.GetComponent<Text>();
        textArea.text = tKey+"の鍵を使った";
        yield return new WaitForSeconds(5);
        textArea.text = " ";
        yield break;
    }
    IEnumerator getKey() 
    {
        Text textArea = textObj.GetComponent<Text>();
        textArea.text = tKey +"の鍵を手に入れた";
        yield return new WaitForSeconds(4);
        textArea.text = " ";
        yield break;
    }
    IEnumerator waitanime()
    {
        door_animation = hitobj.GetComponent<Animator>();
        Debug.Log("アニメーション開始");
        door_animation.SetBool("isOpening", true);
        yield return new WaitForSeconds(2);
        Debug.Log("非表示");
        hitobj.SetActive(false);//簡易的に非表示にしている

    }

}


