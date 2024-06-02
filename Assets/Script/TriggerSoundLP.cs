using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundLP : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LePrince"))
        {
            audioSource.Play();
        }
    }
}
