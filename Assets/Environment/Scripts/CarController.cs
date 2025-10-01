using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Invoke("DestroyCar", 4f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * 20f);
    }

    private void DestroyCar()
    {
        Destroy(gameObject);
    }
}
