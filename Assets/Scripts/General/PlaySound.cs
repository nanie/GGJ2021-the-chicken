using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;
    public bool randomPitch1 = false;
    public bool randomPitch2 = false;
    public void PlayOneShot()
    {
        if (randomPitch1)
            audioSource.pitch = Random.Range(.8f, 1.3f);
        else
            audioSource.pitch = 1;

        audioSource.PlayOneShot(clip);
    }
    public void PlayOneShot2()
    {
        if (randomPitch2)
            audioSource.pitch = Random.Range(.8f, 1.3f);
        else
            audioSource.pitch = 1;
        audioSource.PlayOneShot(clip2);
    }
}
