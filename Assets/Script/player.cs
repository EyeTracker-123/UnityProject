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
        // Player Input����InputAction���擾���܂�
        var playerInput = GetComponent<PlayerInput>();
        holdAction = playerInput.actions["Run"];

        // �{�^����������n�߂����Ɨ����ꂽ���ɃC�x���g��o�^���܂�
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
    public Vector3 _came = new Vector3(0, 0, 0);

    void LateUpdate()
    {
        // �J�������^�[�Q�b�g�ɒǏ]������
        cam.transform.position = target.position + offset;
    }
    // ��ŏ��� public float x = 0.01f;
    void Update()
    {
        //����ŏ�������
        //gameObject.transform.localPosition += _velocity * move_speed;
        

        _came.x += (_camera.x * cameraSpeed) * -1;
        _came.y += _camera.y * cameraSpeed;

        cam_forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        cam_right = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;

        gameObject.transform.localPosition += (cam_right * _velocity.x + cam_forward * _velocity.z) * move_speed;

        //����U�ۗ�
        //_came.x = Mathf.Clamp(_came.x, -90, 90);
        // _came.y = Mathf.Clamp(_came.y, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(_came);
        gameObject.transform.localRotation = Quaternion.Euler(0,_came.y,0);

       // target.Rotate(Vector3.up * camera_x);

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
        move_speed = 0.01f;
        dashflag = false;
    }

    private IEnumerator HoldRoutine()
    {
        while (true)
        {            
            if(stamina > 0)
            {
                //�X�^�~�i������Ȃ���ړ����x���グ��
                move_speed = 0.02f;
                stamina -= 0.003f;
                dashflag = true;
                yield return null;

            }
            else
            {
                //�X�^�~�i��0�����ɂȂ�̂�j�~���A�ړ����x��߂�
                move_speed = 0.01f;
                stamina = 0;
                yield return null;
            }
        }
    }
}


