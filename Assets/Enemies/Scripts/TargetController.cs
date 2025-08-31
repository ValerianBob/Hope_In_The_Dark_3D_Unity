using System.Collections;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public SpawnController spawnController;

    private void Start()
    {
        spawnController = GameObject.Find("ScriptsObject").GetComponent<SpawnController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            spawnController.currentTargetCount -= 1;
        }
    }
}

