using UnityEngine;
using UnityEngine.AI;

public class ZombieControler : MonoBehaviour
{
    public Transform Player;

    private NavMeshAgent agent;

    private Animator animator;

    private CapsuleCollider cc;

    public int Health;
    public int AttackPower;

    public float RangeToAttack;

    public bool isSeePlayer = false;

    private int index;
    private int soundIndex;

    private bool isDead = false;

    private float nextTimeSound = 0f;
    private float nextTimeAttack = 0f;

    private float soundDelay = 0.4f;
    private float attackDelay = 0.5f;

    private float distance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        cc = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);

        SetDeath();
        PursuitPlayer();
        RunningSound();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        soundIndex = Random.Range(0, 3);

        if (soundIndex == 0 && !isDead)
        {
            SoundsController.Instance.PlayZombie(0, transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            animator.SetTrigger("Attack");

            if (Time.time >= nextTimeAttack)
            {
                SoundsController.Instance.PlayZombie(2, transform.position);

                nextTimeAttack = Time.time + attackDelay;
            }
            
        }
    }

    private void PursuitPlayer()
    {
        if (distance <= RangeToAttack && !isDead)
        {
            isSeePlayer = true;
        }

        if (isSeePlayer && !isDead)
        {
            agent.destination = Player.position;
            animator.SetTrigger("Run");
        }
    }

    private void SetDeath()
    {
        if (Health <= 0 && !isDead)
        {
            isSeePlayer = false;
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

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<NavMeshAgent>());
            cc.direction = 2;
            cc.center = new Vector3(0, 0.2f, 0);

            SoundsController.Instance.PlayZombie(3, transform.position);
        }
    }

    private void RunningSound()
    {
        if (Time.time >= nextTimeSound && !isDead && isSeePlayer)
        {
            SoundsController.Instance.PlayZombie(1, transform.position);

            nextTimeSound = Time.time + soundDelay;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeToAttack);
    }
}
