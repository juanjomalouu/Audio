using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider _SliderVolume = null;
    public AudioMixer mixer;
    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        mixer.SetFloat("exposedVolumeParam", Mathf.Log10(_SliderVolume.value) * 20);
        if (SceneManager.GetActiveScene().name == "Ambisonics")
=======
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        //audio = GameObject.FindGameObjectWithTag("AudioSource");
        _SliderVolume.onValueChanged.AddListener((v) =>
>>>>>>> parent of c14175d (Ambisonics Update)
        {
            try
            {
                mixer.SetFloat("exposedVolumeParam", Mathf.Log10(v) * 20);
            } catch(Exception ex)
            {
                audioSource.volume = v;
                Debug.Log("Error en " + ex);
            }
    });
    }
}
