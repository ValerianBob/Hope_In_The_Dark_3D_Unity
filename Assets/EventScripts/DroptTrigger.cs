using UnityEngine;

public class DroptTrigger : MonoBehaviour
{
    public CarryAndDropCargo cadc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cadc.DropCargo();
        }
    }
}
