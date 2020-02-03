using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;                // AudioSorceコンポーネント格納用
    public AudioClip[] sound = new AudioClip[10];   // 10個の効果音の格納用。

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    // 再生
    public void Play (int id) 
    {
        audioSource.PlayOneShot(sound[id]);
    }

    // 再生中か？
    public bool IsPlaying () 
    {
        return audioSource.isPlaying;
    }
}