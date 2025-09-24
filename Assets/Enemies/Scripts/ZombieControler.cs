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

    private int index;
    private int soundIndex;

    private bool isDead = false;

    private float nextTimeSound = 0f;
    private float nextTimeAttack = 0f;

    private float soundDelay = 0.4f;
    private float attackDelay = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        cc = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (!isDead)
        {
            agent.destination = Player.position;
            animator.SetTrigger("Run");
        }

        if (Time.time >= nextTimeSound && !isDead)
        {
            SoundsController.Instance.PlayZombie(1, transform.position);

            nextTimeSound = Time.time + soundDelay;
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

            Destroy(GetComponent<Rigidbody>());
            cc.direction = 2;
            cc.center = new Vector3(0, 0.2f, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        soundIndex = Random.Range(0, 3);
        Debug.Log("sound : " + soundIndex);
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
}
