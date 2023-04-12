using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCustomAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public AudioClip audioClip;
    private AdditiveSynthesis adSynthesis;
    [SerializeField] private Slider _sliderFrecuency;
    [SerializeField] private Slider _sliderAmplitude;
    [SerializeField] private Slider _sliderPhase;

    [SerializeField] private int _customFrecuency;
    [SerializeField] private float _customAmplitude;
    [SerializeField] private float _customPhase;

    //[SerializeField] private Slider slider;

    void Start()
    {
        _customAmplitude = 0.5f;
        _customFrecuency = 1;
        _customPhase = 0;

        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        adSynthesis = audioSource.GetComponent<AdditiveSynthesis>();

        if(_sliderAmplitude != null)
        {
            _sliderAmplitude.onValueChanged.AddListener((v) =>
            {
                _customAmplitude = v;
                audioSource.volume = _customAmplitude;
                adSynthesis.Amplitude = _customAmplitude;
                //slider.value = _customAmplitude;
            });
        }
        if (_sliderFrecuency != null)
        {
            _sliderFrecuency.onValueChanged.AddListener((v) =>
            {
                _customFrecuency = (int) v;
                adSynthesis.Frequency = _customFrecuency;
            });
        }
        if (_sliderPhase != null)
        {
            _sliderPhase.onValueChanged.AddListener((v) =>
             {
                 _customPhase = v * 2;
                 adSynthesis.phaseModification = _customPhase * Mathf.PI;
             });
        }
    }

    public void playSoundEffect()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            
        }
        else
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void PlayCustomAudioADSynthesis()
    {
        adSynthesis.playingCustomTone = !adSynthesis.playingCustomTone;
        audioSource.clip = null;
    }

    public void playClip()
    {
        if (audioSource.clip != audioClip && audioSource.isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (audioSource.isPlaying)
            audioSource.Stop();
        else
            audioSource.Play();
    }

    public void ResetPanStereo()
    {
        audioSource.panStereo = 0;
    }
}
