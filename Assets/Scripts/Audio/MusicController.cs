using System.Collections; 
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] public AudioSource AudioSrc;
    public bool IsSingleton;
    public Object[] BGMusicPlaylist; // Allows a user's customisation of a music playlist for different scenes.

    private static MusicController instance;
    private Coroutine autoplay;

    private void Awake()
    {
        ConfigSingleton();

        LoadResources();
    }

    public void Start()
    {
        CheckAudioListener();
        AudioSrc.loop = false;
        AudioSrc.Play();
    }

    // Update is called once per frame
    public void Update()
    {
        if(!AudioSrc.isPlaying)
        {
            autoplay = StartCoroutine(PlayRandomTrack()); // Begin to play a random track.
        }
    }

    /// <summary>
    /// Method to ensure there is only one audio listener throughout the 
    /// scene. Destroys any surplus.
    /// </summary>
    private void CheckAudioListener()
    {
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        // Ignore the first AudioListener in the array.
        if(audioListeners.Length > 1) 
        {
            for(int i = 1; i < audioListeners.Length; i++)
            {                
                DestroyImmediate(audioListeners[i]); // Remove other audio listeners.
            }
        }
    }

    /// <summary>
    /// Method to determine whether to set this object up as a singleton 
    /// or not depending on the boolean set by user.
    /// </summary>
    private void ConfigSingleton()
    {
        if(IsSingleton)
        {
            // Apply singleton design pattern.
            DontDestroyOnLoad(this);

            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Method to auto-load all music from the Resource-->Music folder if 
    /// no specific music had been specified by the user in the Inspector.
    /// </summary>
    private void LoadResources()
    {
        if(BGMusicPlaylist == null || BGMusicPlaylist.Length == 0)
        {
            // Load all music files in the Assets-->Resources-->Music folder into BGMusicPlaylist.
            BGMusicPlaylist = Resources.LoadAll("Music", typeof(AudioClip));
        }

        if(AudioSrc == null)
        {
            AudioSrc = GetComponent<AudioSource>();
        }

        // Initialise the first file as the clip to be played.
        AudioSrc.clip = BGMusicPlaylist[0] as AudioClip;
    }

    /// <summary>
    /// Shuffles a random track from the loaded list into the Music Player.
    /// </summary>
    protected IEnumerator PlayRandomTrack()
    {
        AudioSrc.clip = BGMusicPlaylist[Random.Range(0, BGMusicPlaylist.Length)] as AudioClip; // Load a random track.
        AudioSrc.Play();
        yield return new WaitUntil(() => !AudioSrc.isPlaying); // Wait until the audio source stops playing.
    }

    /// <summary>
    /// Method to skip the current track playing. Will cause the  
    /// computer to select another track via Update().
    /// </summary>
    public void SkipTrack()
    {
        if(autoplay != null)
        {
            StopCoroutine(autoplay); // Stop the autoplay coroutine.
            AudioSrc.Stop(); // Stop playing the current track.
        }
    }
}
