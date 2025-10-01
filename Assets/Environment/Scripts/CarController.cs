using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        SoundsController.Instance.PlayCar(9, true);

        Invoke("DestroyCar", 5f);
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
