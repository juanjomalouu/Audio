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

    [SerializeField] private Slider _sliderFrequency;
    [SerializeField] private Slider _sliderAmplitude;
    [SerializeField] private Slider _sliderPhase;

    [SerializeField] private int _customFrequency;
    [SerializeField] private float _customAmplitude;
    [SerializeField] private float _customPhase;

    [SerializeField] private int _lowFrequency;
    [SerializeField] private float _lowAmplitude;
    [SerializeField] private float _lowPhase;

    [SerializeField] private int _highFrequency;
    [SerializeField] private float _highAmplitude;
    [SerializeField] private float _highPhase;

    private bool lowFreq;
    private bool highFreq;
    private bool lowAmplitude;
    private bool highAmplitude;
    private bool lowPhase;
    private bool highPhase;
    private bool playCustom;


    //[SerializeField] private Slider slider;

    void Start()
    {
        _customAmplitude = 0.5f;
        _customFrequency = 1;
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
        if (_sliderFrequency != null)
        {
            _sliderFrequency.onValueChanged.AddListener((v) =>
            {
                _customFrequency = (int) v;
                adSynthesis.Frequency = _customFrequency;
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

    public void playLowFrequency()
    {
        if (!lowFreq)
        {
            lowFreq = setEverythingFalse();
            adSynthesis.setAdditiveEnable(true);
        }
        else
        {
            lowFreq = false;
            adSynthesis.setAdditiveEnable(false);
        }
        _sliderFrequency.value = _lowFrequency;
        _customFrequency = _lowFrequency;
        adSynthesis.Frequency = _customFrequency;
    }

    public void playHighFrequency()
    {
        if (!highFreq)
        {
            highFreq = setEverythingFalse();
            adSynthesis.setAdditiveEnable(true);
        }
        else
        {
            highFreq = false;
            adSynthesis.setAdditiveEnable(false);
        }
        _sliderFrequency.value = _highFrequency;
        _customFrequency = _highFrequency;
        adSynthesis.Frequency = _highFrequency;
    }
    public void playLowAmplitude()
    {
        if (!lowAmplitude)
        {
            lowAmplitude = setEverythingFalse();
            adSynthesis.setAdditiveEnable(true);
        }
        else
        {
            lowAmplitude = false;
            adSynthesis.setAdditiveEnable(false);
        }
        _sliderAmplitude.value = _lowAmplitude;
        _customAmplitude = _lowAmplitude;
        adSynthesis.Amplitude = _lowAmplitude;
    }

    public void playHighAmplitude()
    {
        if (!highAmplitude)
        {
            highAmplitude = setEverythingFalse();
            adSynthesis.setAdditiveEnable(true);
        }
        else
        {
            highAmplitude = false;
            adSynthesis.setAdditiveEnable(false);
        }
        _sliderAmplitude.value = _highAmplitude;
        _customAmplitude = _highAmplitude;
        adSynthesis.Amplitude = _highAmplitude;
    }

    public void playLowPhase()
    {
        if (!lowPhase)
        {
            lowPhase = setEverythingFalse();
            adSynthesis.setAdditiveEnable(true);
        }
        else
        {
            lowPhase = false;
            adSynthesis.setAdditiveEnable(false);
        }
        _sliderPhase.value = _lowPhase * MathF.PI;
        _customPhase = _lowPhase * MathF.PI;
        adSynthesis.Phase = _lowPhase * MathF.PI;
    }

    public void playHighPhase()
    {
        if (!highPhase)
        {
            highPhase = setEverythingFalse();
            adSynthesis.setAdditiveEnable(true);
        }
        else
        {
            highPhase = false;
            adSynthesis.setAdditiveEnable(false);
        }
        _sliderPhase.value = _highPhase;
        _customPhase = _highPhase;
        adSynthesis.Phase = _highPhase;
    }


    public void PlayCustomAudioADSynthesis()
    {
        audioSource.clip = null;
        if(!playCustom)
        {
            playCustom = setEverythingFalse();
            adSynthesis.setAdditiveEnable(true);
        }
        else
        {
            playCustom=false;
            adSynthesis.setAdditiveEnable(false);
        }
    }

    private bool setEverythingFalse()
    {
        lowFreq = false;
        highFreq = false;
        lowAmplitude = false;
        highAmplitude = false;
        lowPhase = false;
        highPhase = false;
        playCustom = false;
        return true;
    }
}
