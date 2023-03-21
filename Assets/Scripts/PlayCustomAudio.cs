using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCustomAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public AudioClip audioClip;

    [SerializeField] private Slider _sliderFrecuency = null;
    [SerializeField] private Slider _sliderAmplitude;
    [SerializeField] private Slider _sliderPhase;

    [SerializeField] private float _customFrecuency;
    [SerializeField] private float _customAmplitude;
    [SerializeField] private float _customPhase;
    
    private bool isRightHigher = false;
    private bool isRightMuted = false;
    private float beforeVolume;


    void Start()
    {
        _customAmplitude = 0.5f;
        _customFrecuency = 1;
        _customPhase = 0;

        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();

        if(_sliderAmplitude != null)
        {
            _sliderAmplitude.onValueChanged.AddListener((v) =>
            {
                _customAmplitude = v;
                audioSource.volume = _customAmplitude;
            });
        }
        if (_sliderFrecuency != null)
        {
            _sliderFrecuency.onValueChanged.AddListener((v) =>
            {
                _customFrecuency = v;
                audioSource.pitch = _customFrecuency;
            });
        }
        if (_sliderPhase != null)
        {
            _sliderPhase.onValueChanged.AddListener((v) =>
             {
                 _customPhase = v;
                 audioSource.panStereo = _customPhase;
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
    public void rightMuted()
    {
        if(audioSource.panStereo == -1 && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.panStereo = -1;
            audioSource.volume = 1.0f;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void rightHigher()
    {
        if (audioSource.panStereo == 0.5f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.panStereo = 0.5f;
            audioSource.volume = 1.0f;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }


}
