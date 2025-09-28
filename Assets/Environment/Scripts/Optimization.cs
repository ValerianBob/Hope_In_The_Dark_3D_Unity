using UnityEngine;
using UnityEngine.Playables;

public class Optimization : MonoBehaviour
{
    public GameObject[] ObjectsToHide;
    public GameObject[] ZombiesToHide;

    public GameObject[] ObjectsToShow;
    public GameObject[] ZombiesToShow;

    private void OnTriggerEnter(Collider other)
    {
        foreach (var obj in ObjectsToHide)
        {
            obj.gameObject.SetActive(false);
        }

        foreach (var obj in ZombiesToHide)
        {
            obj.gameObject.SetActive(false);
        }

        foreach (var obj in ObjectsToShow)
        {
            obj.gameObject.SetActive(true);
        }

        foreach (var obj in ZombiesToShow)
        {
            obj.gameObject.SetActive(true);
        }
    }
}
