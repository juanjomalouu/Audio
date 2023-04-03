using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class VolumeController : MonoBehaviour
{
    private AudioSource[] audioSources;

    [SerializeField] private Slider _SliderVolume = null;
    [SerializeField] private Slider _SliderVolume2 = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GameObject.FindGameObjectWithTag("AudioSource").GetComponents<AudioSource>();

        _SliderVolume.onValueChanged.AddListener((v) =>
        {
            foreach(AudioSource source in audioSources)
            {
                source.volume = v;
            }
            if(_SliderVolume2 != null)
                _SliderVolume2.value = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
