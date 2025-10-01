using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public Light RedLight;

    private bool isOn = false;
    void Start()
    {
        StartCoroutine("RedLightPinging");
    }

    private IEnumerator RedLightPinging()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            isOn = !isOn;

            RedLight.enabled = isOn;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
