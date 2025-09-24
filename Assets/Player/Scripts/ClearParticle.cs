using UnityEngine;

public class ClearParticle : MonoBehaviour
{
    void Start()
    {
        Invoke("Clear", 1f);
    }

    private void Clear()
    {
        Destroy(gameObject);
    }
}
