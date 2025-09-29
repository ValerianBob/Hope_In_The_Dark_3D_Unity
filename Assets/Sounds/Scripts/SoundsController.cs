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
    public AudioClip[] Menu;
    public AudioClip[] Musics;

    private AudioSource audioSource;

    private AudioSource playerAudioSource;
    private AudioSource helicopterAudioSource;

    [Header("References")]
    public GameObject playerPosition;
    public GameObject helicopterPosition;

    public float soundsDistance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

        if (helicopterPosition != null)
        {
            helicopterAudioSource = helicopterPosition.GetComponent<AudioSource>();
            if (helicopterAudioSource == null)
                helicopterAudioSource = helicopterPosition.AddComponent<AudioSource>();

            helicopterAudioSource.spatialBlend = 1f; // 3D sound
            helicopterAudioSource.minDistance = 1f;
            helicopterAudioSource.maxDistance = soundsDistance;
            helicopterAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
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

    public void PlayMenu(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Menu[index], soundPosition, 1f);
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

    public void PlayHelicopter(int index, bool loop = false)
    {
        if (helicopterAudioSource != null && index < Environment.Length)
        {
            helicopterAudioSource.clip = Environment[index];
            helicopterAudioSource.loop = loop;
            if (!helicopterAudioSource.isPlaying)
                helicopterAudioSource.Play();
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
