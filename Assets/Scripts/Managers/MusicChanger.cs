using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    private static MusicChanger _instance;
    public static MusicChanger Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Music Changer is null.");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    AudioSource source;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public AudioSource GetAudioSource()
    {
        return source;
    }

}
