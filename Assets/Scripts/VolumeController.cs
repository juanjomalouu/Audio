using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private Slider _SliderVolume = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();

        _SliderVolume.onValueChanged.AddListener((v) =>
        {
            audioSource.volume = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
