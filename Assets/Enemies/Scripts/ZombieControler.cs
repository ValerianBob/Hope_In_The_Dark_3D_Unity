using UnityEngine;
using UnityEngine.AI;

public class ZombieControler : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = Player.position;
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
