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

    /* void OnCollisionStay(Collision collision)
     {
         if (collision.gameObject.CompareTag("Stairsend"))
         {
             isStairsend = true;
         }
     }
     void OnCollisionExit(Collision collision)
     {
         if (collision.gameObject.CompareTag("Stairsend"))
         {
             isStairsend = false;
         }
     }*/




    void LateUpdate()
    {
        // �J�������^�[�Q�b�g�ɒǏ]������
        cam.transform.position = target.position + offset;
    }
    // ��ŏ��� public float x = 0.01f;

    Rigidbody rb;
    
    void Update()
    {
        /*
         
        rb = gameObject.GetComponent<Rigidbody>();
        if (isStairsend)
        {
            rb.useGravity = false;
            Debug.Log("ok");
           // gameObject.transform.position += new Vector3(0,0.1f,0);
        }
        else rb.useGravity = true;

        */
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

    void OnFire()//アイテムやドアなどのアクションはこの中で書く
    {
        RaycastHit hit;
        

        Vector3 direction = cam.transform.forward;
        if (Physics.Raycast(cam.transform.position,direction,out hit, distance))
        {
            GameObject hitobj = hit.collider.gameObject;
            if (hitobj.CompareTag("key"))
            {
                keyFlag kf = hitobj.GetComponent<keyFlag>();
                string kn = kf.keyname;

                switch (kn)
                {
                    case "keyA":
                        A = true;
                        hitobj.SetActive(false);
                        break;

                    case "keyB":
                        B = true;
                        hitobj.SetActive(false);
                        break;

                    case "keyC":
                        C = true;
                        hitobj.SetActive(false);
                        break;

                    case "keyD":
                        D = true;
                        hitobj.SetActive(false);
                        break;

                    case "keyE":
                        E = true;
                        hitobj.SetActive(false);
                        break;

                    case "keyF":
                        F = true;
                        hitobj.SetActive(false);
                        break;

                    case "keyG":
                        G = true;
                        hitobj.SetActive(false);
                        break;
                }
            }
            if (hitobj.CompareTag("keydoor"))
            {
                doorName dn = hitobj.GetComponent<doorName>();
                string D_Number = dn.doorNumber;

                void opendoor()
                {
                    hitobj.SetActive(false);//簡易的に非表示にしている
                }

                switch (D_Number)
                {
                    case "doorA":
                        if(A == true)
                        {
                            opendoor();
                        }
                        else if(B == true || C == true || D == true || E == true || F == true || G == true )
                        {
                            StartCoroutine("otherKey");
                        }
                        else
                        {
                            StartCoroutine("noKey");
                        }
                        break;

                    case "doorB":
                        if (B == true)
                        {
                            hitobj.SetActive(false);//簡易的に非表示にしている
                        }
                        else if (A == true || C == true || D == true || E == true || F == true || G == true)
                        {
                            StartCoroutine("otherKey");
                        }
                        else
                        {
                            StartCoroutine("noKey");
                        }
                        break;

                    case "doorC":
                        if (C == true)
                        {
                            opendoor();
                        }
                        else if (B == true || A == true || D == true || E == true || F == true || G == true)
                        {
                            StartCoroutine("otherKey");
                        }
                        else
                        {
                            StartCoroutine("noKey");
                        }
                        break;

                    case "doorD":
                        if (D == true)
                        {
                            opendoor();
                        }
                        else if (B == true || C == true || A == true || E == true || F == true || G == true)
                        {
                            StartCoroutine("otherKey");
                        }
                        else
                        {
                            StartCoroutine("noKey");
                        }
                        break;

                    case "doorE":
                        if (E == true)
                        {
                            opendoor();
                        }
                        else if (B == true || C == true || D == true || A == true || F == true || G == true)
                        {
                            StartCoroutine("otherKey");
                        }
                        else
                        {
                            StartCoroutine("noKey");
                        }
                        break;

                    case "doorF":
                        if (F == true)
                        {
                            opendoor();
                        }
                        else if (B == true || C == true || D == true || E == true || A == true || G == true)
                        {
                            StartCoroutine("otherKey");
                        }
                        else
                        {
                            StartCoroutine("noKey");
                        }
                        break;

                    case "doorG":
                        if (G == true)
                        {
                            opendoor();
                        }
                        else if (B == true || C == true || D == true || E == true || F == true || A == true)
                        {
                            StartCoroutine("otherKey");
                        }
                        else
                        {
                            StartCoroutine("noKey");
                        }
                        break;
                }
            }
        }
    }
    IEnumerator otherKey()
    {
        Text textArea = textObj.GetComponent<Text>();
        textArea.text = "別の鍵が必要だ";
        yield return new WaitForSeconds(5);
        textArea.text = " ";
    }
    IEnumerator noKey()
    {
        Text textArea = textObj.GetComponent<Text>();
        textArea.text = "鍵がかかっている";
        yield return new WaitForSeconds(5);
        textArea.text = " ";
    }


}


