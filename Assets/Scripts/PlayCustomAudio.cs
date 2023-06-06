using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCustomAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    private AdditiveSynthesis adSynthesis;

    // Sliders to modify the values
    [SerializeField] private Slider _sliderFrequency;
    [SerializeField] private Slider _sliderAmplitude;
    [SerializeField] private Slider _sliderPhase;

    // Customizable variables to start with
    [SerializeField] private int _customFrequency;
    [SerializeField] private float _customAmplitude;
    [SerializeField] private float _customPhase;

    // Minimum variables
    [SerializeField] private int _lowFrequency;
    [SerializeField] private float _lowAmplitude;
    [SerializeField] private float _lowPhase;

    // Maximum variables
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

    /// <summary>
    /// Modifies the frequency slider and sets it to the minimum frequency.
    /// </summary>
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

    /// <summary>
    /// Modifies the frequency slider and sets it to the maximum frequency.
    /// </summary>
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

    /// <summary>
    /// Modifies the amplitude slider and sets it to the minimum amplitude.
    /// </summary>
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

    /// <summary>
    /// Modifies the amplitude slider and sets it to the maximum amplitude.
    /// </summary>
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

    /// <summary>
    /// Modifies the phase slider and sets it to the minimum phase.
    /// </summary>
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

    /// <summary>
    /// Modifies the phase slider and sets it to the maximum phase.
    /// </summary>
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

    /// <summary>
    /// Sets the additive synthesis to play the custom audio or stops it depending on the current state.
    /// </summary>
    public void PlayCustomAudioADSynthesis()
    {
        if (!playCustom && !getSomethingTrue())
        {
            adSynthesis.setAdditiveEnable(true);
            playCustom = setEverythingFalse();
        }
        else 
        {
            adSynthesis.setAdditiveEnable(false);
            playCustom = setEverythingFalse();
            playCustom = false;
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

    /// <summary>
    /// Checks if any modification is currently playing.
    /// </summary>
    private bool getSomethingTrue()
    {
        return lowAmplitude || lowFreq || lowPhase || highAmplitude || highFreq || highPhase;
    }
}
