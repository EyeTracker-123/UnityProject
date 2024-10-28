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
        // Player InputからInputActionを取得します
        var playerInput = GetComponent<PlayerInput>();
        holdAction = playerInput.actions["Run"];

        // ボタンが押され始めた時と離された時にイベントを登録します
        holdAction.started += OnHoldStarted;
        holdAction.canceled += OnHoldCanceled;
    }

    void Update()
    {
        gameObject.transform.position += _velocity * move_speed;

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


