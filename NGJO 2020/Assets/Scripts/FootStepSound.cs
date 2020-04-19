using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootStepSound : MonoBehaviour
{
    public AudioClip footstepClip;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        source.PlayOneShot(footstepClip);
    }
}
