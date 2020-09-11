using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm_controller_RM : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip sound;

    void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    //소리 재생
    public void PlaySound()
    {
        audiosource.clip = sound;
        audiosource.Play();
    }

    //볼륨 설정
    public void SetVolume(float size)
    {
        audiosource.volume = size;
    }

    //소리 반복 여부설정
    public void SetLoop(bool bol)
    {
        audiosource.loop = bol;
    }

    //소리정지
    public void StopSound()
    {
        audiosource.Stop();
    }
}
