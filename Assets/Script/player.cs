using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    Vector3 _velocity;
    public float move_speed;
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
    }

    private IEnumerator HoldRoutine()
    {
        while (true)
        {
            // 押されている間に行いたい処理
            Debug.Log("Holding...");
            move_speed = 0.02f;
            yield return null;
        }
    }
}


