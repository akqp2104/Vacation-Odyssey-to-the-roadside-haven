using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    
    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip nextLine;
    public AudioClip[] dialogueSFX;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayNextLine()
    {
        SFXSource.PlayOneShot(nextLine);
    }

    public void PlaySFX(int sfxIndex)
    {
        SFXSource.PlayOneShot(dialogueSFX[sfxIndex]);
    }
}
