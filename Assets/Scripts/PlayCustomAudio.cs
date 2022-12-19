using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCustomAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public AudioClip audioClip;

    [SerializeField] private Slider _sliderFrecuency;
    [SerializeField] private Slider _sliderAmplitude;
    [SerializeField] private Slider _sliderPhase;

    [SerializeField] private float _customFrecuency;
    [SerializeField] private float _customAmplitude;
    [SerializeField] private float _customPhase;
    void Start()
    {
        _customAmplitude = 0.5f;
        _customFrecuency = 1;
        _customPhase = 0;

        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();

        _sliderAmplitude.onValueChanged.AddListener((v) =>
        {
            _customAmplitude = v;
            audioSource.volume = _customAmplitude;
        });
        _sliderFrecuency.onValueChanged.AddListener((v) =>
        {
            _customFrecuency = v;
            audioSource.pitch = _customFrecuency;
        });
        _sliderPhase.onValueChanged.AddListener((v) =>
        {
            _customPhase = v;
            audioSource.panStereo = _customPhase;
        });
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
}
