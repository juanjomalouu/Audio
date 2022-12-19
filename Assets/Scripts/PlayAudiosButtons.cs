using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudiosButtons : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSoundEffect(float volume = 1.0f)
    {
        audioSource.volume = volume;
        if(audioSource.clip != audioClip)
        {
            audioSource.Stop();
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

    public void playLowAmplitudSound()
    {
        playSoundEffect(0.1f);
    }

    public void playHighAmplitudSound()
    {
       
        playSoundEffect(1.0f);
    }
    
    public void playLowPhase()
    {
        audioSource.panStereo = 0;
        playSoundEffect(1.0f);
    }

    public void playHighPhase()
    {
        audioSource.panStereo = 1;
        playSoundEffect(1.0f);
    }

    public void getSettingsVolume()
    {
        audioSource.volume = 1;
    }
}
