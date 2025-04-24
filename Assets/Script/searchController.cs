using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchController : MonoBehaviour
{
    //デリゲート宣言後、イベントを追加　.invokeでこのイベントを含んだ関数をすべて起動
    //enemyScriptのモード管理を担う関数に付与
    public delegate void TriggerEvent(Collider other);
    public event TriggerEvent OnTriggerEntered;
    public event TriggerEvent OnTriggerExited;
    void OnTriggerEnter(Collider other)
    {
        //？つけてるのはnullの場合エラーを防ぐため
        OnTriggerEntered?.Invoke(other);
    }
    void OnTriggerExit(Collider other)
    {
        //？つけてるのはnullの場合エラーを防ぐため
        OnTriggerExited?.Invoke(other);
    }
}
