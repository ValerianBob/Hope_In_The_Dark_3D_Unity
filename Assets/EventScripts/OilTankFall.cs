using System.Collections;
using UnityEngine;

public class OilTankFall : MonoBehaviour
{
    public GameObject OilTankToFall;

    public GameObject[] zombies;

    private float angle = 0;

    private bool isFalled = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("FallOilTank");

        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].gameObject.SetActive(true);
        }

        if (!isFalled)
        {
            SoundsController.Instance.PlayEnvironment(0, OilTankToFall.transform.position);

            isFalled = true;
        }
    }

    private IEnumerator FallOilTank()
    {
        while (angle != -90)
        {
            angle += -1;
            OilTankToFall.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return new WaitForSeconds(0.001f);
        }
    }
}
