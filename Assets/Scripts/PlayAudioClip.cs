using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class PlayAudioClip : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip audioClip;
    private GameObject audio;
    private AudioSource audioSource;
    private AdditiveSynthesis additiveSynthesis;

    [SerializeField] private bool showAudioBar = true;
    [SerializeField] private Slider audioBarSlider;
    [SerializeField] private TextMeshProUGUI textLabel;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioSource");
        audioSource = audio.GetComponent<AudioSource>();
        additiveSynthesis = audio.GetComponent<AdditiveSynthesis>();

        if(showAudioBar)
        {
            audioBarSlider.gameObject.SetActive(false);
            audioBarSlider.minValue = 0;
            audioBarSlider.maxValue = audioClip.length;
        }
    }

    // Update is called once per frame
    public void playClip()
    {
        if (audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (audioSource.isPlaying)
            audioSource.Stop();
        else
            audioSource.Play();
        if(showAudioBar)
        {
            if(audioSource.isPlaying)
                enableSlider();
            else
                disableSlider();
        }
            
        if(additiveSynthesis!= null)
        {
            additiveSynthesis.setAdditiveEnable(false);
        }
    }

    private void Update()
    {
        if(showAudioBar)
            updateSlider();
    }

    private void enableSlider()
    {
        audioBarSlider.gameObject.SetActive(true);

        audioBarSlider.value = 0;
        textLabel.gameObject.SetActive(false);
    }

    private void disableSlider()
    {
        audioBarSlider.gameObject.SetActive(false);
        textLabel.gameObject.SetActive(true);
    }

    private void updateSlider()
    {
        if ((additiveSynthesis != null && additiveSynthesis.playingCustomTone) || audioSource.clip != audioClip)
        {
            disableSlider();
        }
        if (audioBarSlider != null)
            audioBarSlider.value = audioSource.time;
    }
}

