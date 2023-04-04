using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PlayAudiosButtons : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    [SerializeField] private Slider slider;
    private AdditiveSynthesis adSynthesis;
    // Start is called before the first frame update
    void Start()
    {

        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        adSynthesis = audioSource.GetComponent<AdditiveSynthesis>();

    }

    private void playSound()
    {
        audioSource.pitch = 1.0f;
        if (audioSource.clip != audioClip)
        {
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
        audioSource.volume = slider.value;
        audioSource.panStereo = 0.0f;
        playSound();
        
    }

    public void playLowAmplitudSound()
    {
        adSynthesis.playingCustomTone = false;
        audioSource.volume = slider.value/10;
        playSound();
    }

    public void playHighAmplitudSound()
    {
        adSynthesis.playingCustomTone = false;
        audioSource.volume = slider.value;
        playSound();
    }
}
