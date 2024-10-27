using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    Vector3 _velocity;
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
    // Update is called once per frame
    void Update()
    {
       // Rigidbody rb = GetComponent<Rigidbody>();
       // rb.velocity = _velocity * move_speed

        gameObject.transform.position += _velocity * move_speed;

        //�X�^�~�i��1.0��荂���Ȃ������ɁA1�ɖ߂�����
        if(stamina > 1)
        {
            stamina = 1;
            dashflag = false;
        }
        //3���ȏ�̃X�^�~�i���񕜂�����
        if(stamina >= 0.3 && dashflag)
        {
            stamina += 0.003f;
        }
        Debug.Log("st="+stamina+" df="+dashflag);
    }
    
    void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();
        _velocity = new Vector3(axis.x,0, axis.y);
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
        //Debug.Log("Hold started");
        StartCoroutine(HoldRoutine());
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        // �{�^���������ꂽ���~
        //Debug.Log("Hold canceled");
        StopAllCoroutines();
        move_speed = 0.01f;
        //dashflag = false;
    }

    private IEnumerator HoldRoutine()
    {
        while (true)
        {
            // ������Ă���Ԃɍs����������
            //Debug.Log("Holding...");
            if(stamina > 0)
            {
                move_speed = 0.02f;
                stamina -= 0.003f;
                yield return null;

            }
            else
            {
                // yield return new WaitForSeconds(2);
                move_speed = 0.01f;

                dashflag = true;

                //�X�^�~�i��3���܂ŏ��X�ɉ񕜂�����
                while (stamina < 0.3f)
                {
                    stamina += 0.003f;
                    yield return null;
                }
            }
            
        }
    }
}


