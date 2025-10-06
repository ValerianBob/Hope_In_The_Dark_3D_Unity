using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [HideInInspector]
    public static PlayerSettings Instance;

    public float PlayerSensitivity;

    public float SoundVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
