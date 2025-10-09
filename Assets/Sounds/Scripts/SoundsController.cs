using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance;

    [Header("Sound Clips Settings")]
    public AudioClip[] GunShots;
    public AudioClip[] GunTake;
    public AudioClip[] GunReload;
    public AudioClip[] Looting;
    public AudioClip[] Environment;
    public AudioClip[] Zombie;
    public AudioClip[] Player;
    public AudioClip[] Menu;
    public AudioClip[] Musics;

    private AudioSource audioSource;

    private AudioSource playerAudioSource;
    private AudioSource playerReloadSource;
    private AudioSource helicopterAudioSource;
    private AudioSource carAudioSource;

    [Header("References")]
    public GameObject playerPosition;
    public GameObject helicopterPosition;
    public GameObject carPosition;

    public float soundsDistance;
    public float currentVolume;

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

        //Player
        if (playerPosition != null)
        {
            playerAudioSource = playerPosition.GetComponent<AudioSource>();
            if (playerAudioSource == null)
                playerAudioSource = playerPosition.AddComponent<AudioSource>();

            playerAudioSource.spatialBlend = 1f;
            playerAudioSource.minDistance = 1f;
            playerAudioSource.maxDistance = soundsDistance;
            playerAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        }

        //Reload
        playerReloadSource = playerPosition.AddComponent<AudioSource>();
        playerReloadSource.spatialBlend = 1f;
        playerReloadSource.minDistance = 1f;
        playerReloadSource.maxDistance = soundsDistance;
        playerReloadSource.rolloffMode = AudioRolloffMode.Logarithmic;

        //Helicopter
        if (helicopterPosition != null)
        {
            helicopterAudioSource = helicopterPosition.GetComponent<AudioSource>();
            if (helicopterAudioSource == null)
                helicopterAudioSource = helicopterPosition.AddComponent<AudioSource>();

            helicopterAudioSource.spatialBlend = 1f;
            helicopterAudioSource.minDistance = 1f;
            helicopterAudioSource.maxDistance = soundsDistance;
            helicopterAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        }

        //Car
        if (carPosition != null)
        {
            carAudioSource = carPosition.GetComponent<AudioSource>();
            if (carAudioSource == null)
                carAudioSource = carPosition.AddComponent<AudioSource>();

            carAudioSource.spatialBlend = 1f;
            carAudioSource.minDistance = 1f;
            carAudioSource.maxDistance = soundsDistance;
            carAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        }
    }

    private void Update()
    {
        currentVolume = PlayerSettings.Instance.SoundVolume;

        playerAudioSource.volume = currentVolume;
        helicopterAudioSource.volume = currentVolume;

        if (carAudioSource != null)
        {
            carAudioSource.volume = currentVolume;
        }
    }

    public void PlayGunShot(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(GunShots[index], soundPosition, currentVolume);
    }

    public void PlayGunTake(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(GunTake[index], soundPosition, currentVolume);
    }

    public void PlayGunReload(int index)
    {
        if (playerReloadSource != null && index < GunReload.Length)
        {
            playerReloadSource.PlayOneShot(GunReload[index], currentVolume);
        }
    }

    public void PlayZombie(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Zombie[index], soundPosition, currentVolume);
    }

    public void PlayEnvironment(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Environment[index], soundPosition, currentVolume);
    }

    public void PlayLooting(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Looting[index], soundPosition, currentVolume);
    }

    public void PlayMusic(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Musics[index], soundPosition, currentVolume);
    }

    public void PlayPlayer(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Player[index], soundPosition, currentVolume);
    }

    public void PlayMenu(int index, Vector3 soundPosition)
    {
        AudioSource.PlayClipAtPoint(Menu[index], soundPosition, currentVolume);
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

    public void PlayCar(int index, bool loop = false)
    {
        if (carAudioSource != null && index < Environment.Length)
        {
            carAudioSource.clip = Environment[index];
            carAudioSource.loop = loop;
            if (!carAudioSource.isPlaying)
                carAudioSource.Play();
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
