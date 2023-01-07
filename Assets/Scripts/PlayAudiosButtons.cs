using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudiosButtons : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    private void playSound()
    {
        audioSource.pitch = 1.0f;
        if (audioSource.clip != audioClip)
        {
            //audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
        }
    }
    public void playSoundEffect()
    {
        audioSource.volume = 1.0f;
        audioSource.panStereo = 0.0f;
        playSound();
        
    }

    public void playLowAmplitudSound()
    {
        audioSource.volume = 0.1f;
        playSound();
    }

    public void playHighAmplitudSound()
    {
        audioSource.volume = 1.0f;
        playSound();
    }
    
    public void playLowPhase()
    {
        audioSource.panStereo = 0.0f;
        playSound();
    }

    public void playHighPhase()
    {
        audioSource.panStereo = 1.0f;
        playSound();
    }

    public void getSettingsVolume()
    {
        audioSource.volume = 1;
    }
}
