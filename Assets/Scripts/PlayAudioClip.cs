using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class PlayAudioClip : MonoBehaviour
{
    public AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AdditiveSynthesis additiveSynthesis;

    [SerializeField] private bool showAudioBar = true;
    [SerializeField] private Slider audioBarSlider;
    [SerializeField] private TextMeshProUGUI textLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        additiveSynthesis = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>().GetComponent<AdditiveSynthesis>();

        if(showAudioBar)
        {
            // Set up the audio bar slider
            audioBarSlider.gameObject.SetActive(false);
            audioBarSlider.minValue = 0;
            audioBarSlider.maxValue = audioClip.length;
        }
    }

    /// <summary>
    /// Plays the assigned audio clip.
    /// If the audio clip is already playing, stops it. Otherwise, starts playing.
    /// If a different audio clip is assigned, changes the audio clip and starts playing.
    /// </summary>
    public void playClip()
    {
        if (additiveSynthesis != null)
        {
            additiveSynthesis.StopVocals();

            additiveSynthesis.setAdditiveEnable(false);
        }
        if(audioSource.clip == audioClip)
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
        else
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        if(showAudioBar)
        {
            if(audioSource.isPlaying)
                enableSlider();
            else
                disableSlider();
        }
            

    }

    private void Update()
    {
        if(showAudioBar)
            updateSlider();
    }

    /// <summary>
    /// Activates the audio bar slider for playback progress.
    /// </summary>
    private void enableSlider()
    {
        audioBarSlider.gameObject.SetActive(true);

        audioBarSlider.value = 0;
        textLabel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Deactivates the audio bar slider.
    /// </summary>
    private void disableSlider()
    {
        audioBarSlider.gameObject.SetActive(false);
        textLabel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Updates the playback progress on the audio bar slider.
    /// </summary>
    private void updateSlider()
    {
        if ((additiveSynthesis != null && additiveSynthesis.playingCustomTone) || audioSource.clip != audioClip)
        {
            disableSlider();
        }
        if(!audioSource.isPlaying)
            disableSlider();
        if (audioBarSlider != null)
            audioBarSlider.value = audioSource.time;
    }
}

