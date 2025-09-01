using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;

    public Vector3 direction = Vector3.zero;

    public float speed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Invoke("DestroyBullet", 2f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
