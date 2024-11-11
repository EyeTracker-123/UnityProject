using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    Vector3 _velocity;
    Vector3 _camera;
    public float move_speed;
    public float stamina = 1;
    public bool dashflag = false;
    private void Awake()
    {
        // Player InputからInputActionを取得します
        var playerInput = GetComponent<PlayerInput>();
        holdAction = playerInput.actions["Run"];

        // ボタンが押され始めた時と離された時にイベントを登録します
        holdAction.started += OnHoldStarted;
        holdAction.canceled += OnHoldCanceled;
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
    Vector3 _came = new Vector3(0, 0, 0);

    void LateUpdate()
    {
        // カメラをターゲットに追従させる
        cam.transform.position = target.position + offset;
    }
    // 後で消す public float x = 0.01f;
    void Update()
    {
        //↓後で消すかも
        //gameObject.transform.localPosition += _velocity * move_speed;
        
        _came.x += (_camera.x * cameraSpeed) * -1;
        _came.y += _camera.y * cameraSpeed;

        cam_forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        cam_right = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;

        gameObject.transform.localPosition += (cam_right * _velocity.x + cam_forward * _velocity.z) * move_speed;

        //↓一旦保留
        //_came.x = Mathf.Clamp(_came.x, -90, 90);
        // _came.y = Mathf.Clamp(_came.y, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(_came);
        gameObject.transform.localRotation = Quaternion.Euler(0,_came.y,0);

       // target.Rotate(Vector3.up * camera_x);

        //スタミナが1.0より高くなった時に、1に戻す処理
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
    }
    
    void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();
        _velocity = new Vector3(axis.x,0, axis.y);
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
        // イベントの解除
        holdAction.started -= OnHoldStarted;
        holdAction.canceled -= OnHoldCanceled;
    }

    private void OnHoldStarted(InputAction.CallbackContext context)
    {
        // ボタンが押され始めたら実行
        
        StartCoroutine(HoldRoutine());
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        // ボタンが離されたら停止
        
        StopAllCoroutines();
        move_speed = 0.01f;
        dashflag = false;
    }

    private IEnumerator HoldRoutine()
    {
        while (true)
        {            
            if(stamina > 0)
            {
                //スタミナを消費しながら移動速度を上げる
                move_speed = 0.02f;
                stamina -= 0.003f;
                dashflag = true;
                yield return null;

            }
            else
            {
                //スタミナが0未満になるのを阻止し、移動速度を戻す
                move_speed = 0.01f;
                stamina = 0;
                yield return null;
            }
        }
    }
}


