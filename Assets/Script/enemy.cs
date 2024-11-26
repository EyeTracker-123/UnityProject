using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;

    //追いかける対象
    [SerializeField]
    private Transform _player;

    void Update()
    {
        if (_navMeshAgent.isOnNavMesh)
        {
            // プレイヤーの位置に向かって移動
            _navMeshAgent.SetDestination(_player.position);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent が NavMesh 上にいません！");
        }
    }
}