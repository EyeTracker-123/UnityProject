using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerTest : MonoBehaviour
{
    private GameObject test;
    void Start()
    {
        test = FindObjectOfType<GameObject>();

        
    }

    void Update(){

    }

    void CreateCollider(){
        // GameObjectにBox Colliderを追加
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();

        // トリガーとして設定
        boxCollider.isTrigger = true;

        // サイズを設定（必要に応じて変更）
        boxCollider.size = new Vector3(100, 1, 100);

        // 中心位置を設定（必要に応じて変更）
        boxCollider.center = new Vector3(0, 0, 0);
    }

    // トリガーイベントの例
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by {other.gameObject.name}");
    }
}
