using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist : MonoBehaviour
{
    public AudioClip[] musics;
    AudioSource audioSource;

    void Start()
    {
        if(musics.Length != 0)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = musics[Random.Range(0, musics.Length)];
            audioSource.Play();
        }
    }

    
    void Update()
    {
        if(audioSource != null)
        {
            if(audioSource.isPlaying == false)
            {
                audioSource.clip = musics[Random.Range(0, musics.Length)];
                audioSource.Play();
            }
        }
    }
}
