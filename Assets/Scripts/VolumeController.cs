using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    //private RectTransform handleRect;
    //public float rotationSpeed = 100f;
    //private bool isAudioPlaying = false;

    // Increase the volume of the scene, differentiating between Ambisonics and non-Ambisonics scenes.
    void Start()
    {
        //handleRect = _SliderVolume.handleRect.GetComponentInChildren<RectTransform>();
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
                if (v <= -50)
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
                }
                catch (Exception ex)
                {
                    if (audioSource != null)
                        audioSource.gainDb = v;
                    Debug.Log("Error en " + ex);
                }
            });
        }
    }

    //void Update()
    //{
    //    CheckAudioPlaying();
    //    // Rotate the handle based on the audio playback
    //    if (isAudioPlaying)
    //    {
    //        Debug.Log("a");
    //        // Apply the rotation to the handle
    //        //handleRect.rotation = Quaternion.Euler(0f, 0f, handleRect.rotation.z+rotationSpeed);
    //        handleRect.transform.eulerAngles = new Vector3(handleRect.transform.eulerAngles.x, handleRect.transform.eulerAngles.y, handleRect.transform.eulerAngles.z+0.05f);
    //    }
    //    else
    //    {
    //        handleRect.rotation = Quaternion.Euler(0f, 0f, 0f);
    //    }
    //}

    //private void CheckAudioPlaying()
    //{
    //    AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

    //    foreach (AudioSource audioSource in audioSources)
    //    {
    //        if (audioSource.isPlaying)
    //        {
    //            isAudioPlaying = true;
    //            return;
    //        }
    //    }

    //    isAudioPlaying = false;
    //}
}
