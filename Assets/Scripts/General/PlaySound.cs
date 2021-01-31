using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;
    public void PlayOneShot()
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlayOneShot2()
    {
        audioSource.PlayOneShot(clip2);
    }
}
