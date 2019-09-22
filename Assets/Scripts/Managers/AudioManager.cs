using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]
    private AudioSource bgAudioSource;
    [SerializeField]
    private AudioSource sfxAudioSource;

    private void OnEnable()
    {
        instance = this;
    }

    public void PlayBackgroundMusic(bool flag)
    {
        if (flag)
            bgAudioSource.Play();
        else
            bgAudioSource.Stop();
    }

    public void PlayOneShot(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    private void OnDisable()
    {
        instance = null;
    }
}
