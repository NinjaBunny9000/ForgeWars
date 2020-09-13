using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ToolClang : MonoBehaviour
{

    AudioSource clangSound;

    public ParticleSystem sparks;
    public AudioClip hammerClang;

    // Start is called before the first frame update
    void Start()
    {
        clangSound = GetComponent<AudioSource>();
    }

    public void PlayClang()
    {
        clangSound.Play(0);
    }

    public void PlaySparks()
    {
        // sparks.Stop();
        // sparks.Play();
    }
}
