using System.Collections;
using UnityEngine;

public class OilTankFall : MonoBehaviour
{
    public GameObject OilTankToFall;

    private float angle = 0;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("FallOilTank");
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
