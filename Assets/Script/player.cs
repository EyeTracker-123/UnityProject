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
    // Update is called once per frame
    void Update()
    {
       // Rigidbody rb = GetComponent<Rigidbody>();
       // rb.velocity = _velocity * move_speed

        gameObject.transform.position += _velocity * move_speed;

        //スタミナが1.0より高くなった時に、1に戻す処理
        if(stamina > 1)
        {
            stamina = 1;
            dashflag = false;
        }
        //3割以上のスタミナを回復させる
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
        // イベントの解除
        holdAction.started -= OnHoldStarted;
        holdAction.canceled -= OnHoldCanceled;
    }

    private void OnHoldStarted(InputAction.CallbackContext context)
    {
        // ボタンが押され始めたら実行
        //Debug.Log("Hold started");
        StartCoroutine(HoldRoutine());
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        // ボタンが離されたら停止
        //Debug.Log("Hold canceled");
        StopAllCoroutines();
        move_speed = 0.01f;
        //dashflag = false;
    }

    private IEnumerator HoldRoutine()
    {
        while (true)
        {
            // 押されている間に行いたい処理
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

                //スタミナを3割まで徐々に回復させる
                while (stamina < 0.3f)
                {
                    stamina += 0.003f;
                    yield return null;
                }
            }
            
        }
    }
}


