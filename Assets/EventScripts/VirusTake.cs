using UnityEngine;

public class VirusTake : MonoBehaviour
{
    public GameObject DoubleDoor;

    public string VirusInfo;
    public string PressInfo;

    private void OnDestroy()
    {
        DoubleDoor.GetComponent<BoxCollider>().enabled = true;
    }
}
