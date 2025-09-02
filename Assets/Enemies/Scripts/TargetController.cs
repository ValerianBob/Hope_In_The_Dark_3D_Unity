using System.Collections;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public SpawnController spawnController;

    private void Start()
    {
        spawnController = GameObject.Find("ScriptsObject").GetComponent<SpawnController>();
    }

    public void Death()
    {
        Destroy(gameObject);
        spawnController.currentTargetCount -= 1;
    }
}

