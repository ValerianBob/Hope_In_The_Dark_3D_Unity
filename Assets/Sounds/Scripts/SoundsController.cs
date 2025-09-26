using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance;

    [Header("Sound Clips Settings")]
    public AudioClip[] GunShots;
    public AudioClip[] GunTake;
    public AudioClip[] Looting;
    public AudioClip[] Environment;
    public AudioClip[] Zombie;
    public AudioClip[] Player;
    public AudioClip[] Musics;

    private AudioSource audioSource;

    private AudioSource playerAudioSource;

    [Header("References")]
    public GameObject playerPosition;

    public float soundsDistance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
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

        //Player audio :
        if (playerPosition != null)
        {
            playerAudioSource = playerPosition.GetComponent<AudioSource>();
            if (playerAudioSource == null)
                playerAudioSource = playerPosition.AddComponent<AudioSource>();

            playerAudioSource.spatialBlend = 1f; // 3D sound
            playerAudioSource.minDistance = 1f;
            playerAudioSource.maxDistance = soundsDistance;
            playerAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        }
    }

    public void PlayGunShot(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(GunShots[index], soundPosition, 1f);
    }

    public void PlayGunTake(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(GunTake[index], soundPosition, 1f);
    }

    public void PlayZombie(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Zombie[index], soundPosition, 1f);
    }

    public void PlayEnvironment(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Environment[index], soundPosition, 1f);
    }

    public void PlayLooting(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Looting[index], soundPosition, 1f);
    }

    public void PlayMusic(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Musics[index], soundPosition, 1f);
    }

    public void PlayPlayer(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Player[index], soundPosition, 1f);
    }

    public void PlayFootstep(int index, bool loop = false, float pitch = 1f)
    {
        if (playerAudioSource != null && index < Player.Length)
        {
            playerAudioSource.clip = Player[index];
            playerAudioSource.loop = loop;
            playerAudioSource.pitch = pitch;
            if (!playerAudioSource.isPlaying)
                playerAudioSource.Play();
        }
    }

    public void StopFootstep()
    {
        if (playerAudioSource != null && playerAudioSource.isPlaying)
        {
            playerAudioSource.Stop();
            playerAudioSource.loop = false;
        }
    }
}
