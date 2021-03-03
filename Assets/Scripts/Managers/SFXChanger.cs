using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXChanger : MonoBehaviour
{
    private static SFXChanger _instance;
    public static SFXChanger Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SFX Changer is null.");
            return _instance;
        }
    }


    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] List<AudioClip> sfxList = new List<AudioClip>();

    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(sfxList[0]);
    }
    
    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(sfxList[1]);
    }

    public void PlayGravitySound()
    {
        audioSource.PlayOneShot(sfxList[2]);
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

}
