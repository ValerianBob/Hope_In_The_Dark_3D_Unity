using UnityEngine;
using UnityEngine.AI;

public class ZombieControler : MonoBehaviour
{
    public Transform Player;

    private NavMeshAgent agent;

    private Animator animator;

    public int Health;
    public int AttackPower;

    private int index;

    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            agent.destination = Player.position;
            animator.SetTrigger("Run");
        }

        if (Health <= 0 && !isDead)
        {
            isDead = true;

            index = Random.Range(0, 2);

            if (index == 1)
            {
                animator.SetTrigger("Death1");
            }
            else
            {
                animator.SetTrigger("Death2");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isDead)
        {
            animator.SetTrigger("Attack");
        }
    }
}
