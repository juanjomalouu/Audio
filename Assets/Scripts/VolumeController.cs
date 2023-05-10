using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider _SliderVolume = null;
    public AudioMixer mixer;
    [SerializeField] private ResonanceAudioSource audioSource;
    

    // Subir el volumen de la escena, diferenciando si se trata de una escena Ambisonics o no.
    void Start()
    {
        mixer.SetFloat("exposedVolumeParam", Mathf.Log10(_SliderVolume.value) * 20);
        if (SceneManager.GetActiveScene().name == "Ambisonics")
        {
            _SliderVolume.value = 0;
            audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<ResonanceAudioSource>();
            _SliderVolume.maxValue = 10.0f;
            _SliderVolume.minValue = -50.0f;
            _SliderVolume.onValueChanged.AddListener((v) =>
            {
                audioSource.gainDb = v;     
                if(v <= -50)
                {
                    audioSource.gainDb = -100;
                }   
            });
        }
        else
        {
            _SliderVolume.onValueChanged.AddListener((v) =>
            {
                try
                {
                    mixer.SetFloat("exposedVolumeParam", Mathf.Log10(v) * 20);
                } catch(Exception ex)
                {
                    if(audioSource != null)
                        audioSource.gainDb = v;
                    Debug.Log("Error en " + ex);
                }
            });
        }
    }
}
