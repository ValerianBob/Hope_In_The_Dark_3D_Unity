using System.Runtime.CompilerServices;
using UnityEngine;

public class CarryAndDropCargo : MonoBehaviour
{
    public string info;
    public string PressInfo;

    public GameObject cargo;
    public GameObject container;
    public GameObject cable;

    private bool isCarryed = false;
    private bool isFallen = false;

    public void CarryCargo()
    {
        cargo.transform.position = new Vector3(139.58f, 4.8f, 135.824f);

        if (!isCarryed)
        {
            SoundsController.Instance.PlayEnvironment(1, container.transform.position);

            isCarryed = true;
        }
    }

    public void DropCargo()
    {
        cable.gameObject.SetActive(false);

        Rigidbody rb = container.gameObject.AddComponent<Rigidbody>();

        rb.mass = 2f;
        rb.linearDamping = 1f;
        rb.angularDamping = 0.5f;
        rb.useGravity = true;
        rb.isKinematic = false;

        if (!isFallen)
        {
            SoundsController.Instance.PlayEnvironment(2, container.gameObject.transform.position);
        }

        Invoke("ClearRigidbody", 1f);
        Invoke("PlayContainerCrashSound", 0.7f);
    }

    private void ClearRigidbody()
    {
        Rigidbody rb = container.GetComponent<Rigidbody>();

        Destroy(rb);
    }

    private void PlayContainerCrashSound()
    {
        if (!isFallen)
        {
            SoundsController.Instance.PlayEnvironment(3, container.gameObject.transform.position);
            isFallen = true;
        }
    }
}
