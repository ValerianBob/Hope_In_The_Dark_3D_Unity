using UnityEngine;

public class TestMusicSound : MonoBehaviour
{
    void Start()
    {
        SoundsController.Instance.PlayMusic(0, transform.position);
    }
}
