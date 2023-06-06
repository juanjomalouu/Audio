using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class ModifyCustomWave : MonoBehaviour
{
    private GameObject audio;
    private AudioSource audioSource;
    private AdditiveSynthesis additiveSynthesis;

    private static int _slidersCount = 12;
    [SerializeField] private Slider[] _sliders;
    [SerializeField] private float[] _f; 
    [SerializeField] private bool isDrawing;
    
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioSource");
        audioSource = audio.GetComponent<AudioSource>();
        additiveSynthesis = audio.GetComponent<AdditiveSynthesis>();
        if (isDrawing)
        {
            // Set initial values for f[] array
            for (int i = 0; i < _slidersCount; i++)
            {
                _f[i] = _sliders[i].value;
            }

            // Add listeners to the sliders
            for (int i = 0; i < _slidersCount; i++)
            {
                int index = i; // Create a local copy of the index variable for the lambda expression
                _sliders[i].onValueChanged.AddListener((v) =>
                {
                    _f[index] = v / 1000;
                    additiveSynthesis.amplitudes[index] = _f[index];
                    additiveSynthesis.paintWave();
                });
            }
        }


    }
    /// <summary>
    /// Sets all sliders to create a sinusoidal wave.
    /// </summary>
    public void setSinusWave()
    {
        _sliders[0].value = 1.0f * 1000;
        for (int i = 1; i < additiveSynthesis.amplitudes.Length; i++)
        {   
            _sliders[i].value = 0.0f;
        }
    }

    /// <summary>
    /// Sets all sliders to create a sawtooth wave.
    /// </summary>
    public void setSaw()
    {
        for (int i = 0; i < additiveSynthesis.amplitudes.Length; i++)
        {
            float x = 1.0f / (i + 1);
            _sliders[i].value = x * 1000;
        }
    }

    /// <summary>
    /// Sets all sliders to create a square wave.
    /// </summary>
    public void setSquare()
    {
        for (int i = 0; i < additiveSynthesis.amplitudes.Length; i++)
        {
            float x = 0.0f;
            if (i % 2 == 0)
                x = 1.0f / (i + 1);
            _sliders[i].value = x * 1000;
        }
    }

    /// <summary>
    /// Sets all sliders to create a pulse wave.
    /// </summary>
    public void setPulse()
    { 
        for(int i = 0; i < additiveSynthesis.amplitudes.Length; i++)
        {
            additiveSynthesis.amplitudes[i] = 0.3f;
            try
            {
                _sliders[i].value = 0.3f * 1000;
            }
            catch(Exception ex)
            {
                Debug.Log("");
            }
        }
    }

    // Coroutine to make the pulse last for 0.1 seconds
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.1f);
        additiveSynthesis.setAdditiveEnable(false);
        audioSource.Stop();
    }
}
