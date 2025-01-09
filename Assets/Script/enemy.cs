using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;

    //追いかける対象
    [SerializeField]
    private Transform player;

    private bool flag = false;

    void Update()
    {
        if (_navMeshAgent.isOnNavMesh)
        {
            // プレイヤーの位置に向かって移動
            if (flag == false)
            {
                Debug.Log("on");
                flag = true;
                StartCoroutine(searchWait());
            }
            
        }
        else
        {
            Debug.LogWarning("NavMeshAgent が NavMesh 上にいません！");
        }
    }

    IEnumerator searchWait(){
        
        _navMeshAgent.SetDestination(player.position);

        yield return new WaitForSeconds(2f);
        flag = false;
        Debug.Log("off");

    }
}