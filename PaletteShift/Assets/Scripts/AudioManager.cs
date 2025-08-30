using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance { get; private set; }
    public float MasterVolume; //added in anticipation for dedicated sound settings UI
    public AudioSource MusicAudioSource;
    public AudioSource SFXAudioSource;

    private void Awake()
    {
        if (AudioManagerInstance != null && AudioManagerInstance != this)
        {
            Destroy(this);
            return;
        }

        AudioManagerInstance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find Audio Sources
        MusicAudioSource = GameObject.Find("Music").GetComponent<AudioSource>();
        SFXAudioSource = GameObject.Find("SFX").GetComponent<AudioSource>();

        //Set Music to loop and have the ambience play
        MusicAudioSource.loop = true;
        MusicAudioSource.Play();
    }

    void Update()
    {
        //Set volume for non-master volumes (music, SFX, etc) 
        MusicAudioSource.volume = 1 * MasterVolume;
        SFXAudioSource.volume = 1 * MasterVolume;
    }
}
