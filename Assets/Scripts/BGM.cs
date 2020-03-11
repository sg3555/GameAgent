using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip mainTheme;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySound(mainTheme);
        audioSource.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }

    public void SetVolume(float size)
    {
        audioSource.volume = size;
    }


}
