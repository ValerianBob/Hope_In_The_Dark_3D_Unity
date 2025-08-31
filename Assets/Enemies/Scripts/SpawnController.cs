using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject targetPrefab;

    public int currentTargetCount = 0; 
    public int targetCount = 20;

    void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    private IEnumerator SpawnTargets()
    {
        while (true)
        {
            if (currentTargetCount != targetCount)
            {
                Instantiate(targetPrefab, new Vector3(Random.Range(0.4f, 90f), Random.Range(5f, 20f), Random.Range(-10, -40)),
                                targetPrefab.transform.rotation);
                
                currentTargetCount += 1;
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}