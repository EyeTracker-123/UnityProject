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

    void Update()
    {
        gameObject.transform.position += _velocity * move_speed;

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


