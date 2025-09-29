using System.Collections;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light light;

    private bool show = false;

    void Start()
    {
        light = transform.GetChild(0).GetComponent<Light>();

        StartCoroutine("LightLagging");
    }

    private void Update()
    {
        Debug.Log(show);
    }

    private IEnumerator LightLagging()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            light.enabled = show;
            show = !show;
        }
    }
}
