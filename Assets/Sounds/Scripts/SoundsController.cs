using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance;

    [Header("Sound Clips Settings")]
    public AudioClip[] GunShots;
    public AudioClip[] Looting;
    public AudioClip[] Musics;

    private AudioSource audioSource;

    public GameObject playerPosition;

    public float soundsDistance = 200f;

    void Awake()
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

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 5f; 
        audioSource.maxDistance = soundsDistance;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
    }

    public void PlayGunShot(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(GunShots[index], soundPosition, 1f);
    }

    public void PlayLooting(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Looting[index], soundPosition, 1f);
    }

    public void PlayMusic(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Musics[index], soundPosition, 1f);
    }
}
