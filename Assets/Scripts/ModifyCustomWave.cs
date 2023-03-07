using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyCustomWave : MonoBehaviour
{
    private GameObject audio;
    private AudioSource audioSource;
    private AdditiveSynthesis additiveSynthesis;
    
    [SerializeField] private Slider _sliderF0;
    [SerializeField] private Slider _sliderF1;
    [SerializeField] private Slider _sliderF2;
    [SerializeField] private Slider _sliderF3;
    [SerializeField] private Slider _sliderF4;
    [SerializeField] private Slider _sliderF5;

    private float _f0, _f1, _f2, _f3, _f4, _f5;

    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioSource");
        audioSource = audio.GetComponent<AudioSource>();
        additiveSynthesis = audio.GetComponent<AdditiveSynthesis>();

        _f0 = _sliderF0.value;
        _f1 = _sliderF1.value;
        _f2 = _sliderF2.value;
        _f3 = _sliderF3.value;
        _f4 = _sliderF4.value;
        _f5 = _sliderF5.value;

        _sliderF0.onValueChanged.AddListener((v) =>
        {
            _f0 = v/1000;
            additiveSynthesis.amplitudes[0] = _f0;
            additiveSynthesis.paintWave();
        });
            
        _sliderF1.onValueChanged.AddListener((v) =>
        {
            _f1 = v / 1000;
            additiveSynthesis.amplitudes[1] = _f1;
            additiveSynthesis.paintWave();
        });

        _sliderF2.onValueChanged.AddListener((v) =>
        {
            _f2 = v / 1000;
            additiveSynthesis.amplitudes[2] = _f2;
            additiveSynthesis.paintWave();
        });

        _sliderF3.onValueChanged.AddListener((v) =>
        {
            _f3 = v / 1000;
            additiveSynthesis.amplitudes[3] = _f3;
            additiveSynthesis.paintWave();
        });

        _sliderF4.onValueChanged.AddListener((v) =>
        {
            _f4 = v / 1000;
            additiveSynthesis.amplitudes[4] = _f4;
            additiveSynthesis.paintWave();
        });

        _sliderF5.onValueChanged.AddListener((v) =>
        {
            _f5 = v / 1000;
            additiveSynthesis.amplitudes[5] = _f5;
            additiveSynthesis.paintWave();
        });


    }

}
