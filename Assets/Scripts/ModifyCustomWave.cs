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

            for(int i = 0; i < _slidersCount; i++)
            {
                _f[i] = _sliders[i].value;
            }
            #region Seteo de Sliders
            _sliders[0].onValueChanged.AddListener((v) =>
            {
                _f[0] = v / 1000;
                additiveSynthesis.amplitudes[0] = _f[0];
                additiveSynthesis.paintWave();
            });

            _sliders[1].onValueChanged.AddListener((v) =>
            {
                _f[1] = v / 1000;
                additiveSynthesis.amplitudes[1] = _f[1];
                additiveSynthesis.paintWave();
            });

            _sliders[2].onValueChanged.AddListener((v) =>
            {
                _f[2] = v / 1000;
                additiveSynthesis.amplitudes[2] = _f[2];
                additiveSynthesis.paintWave();
            });

            _sliders[3].onValueChanged.AddListener((v) =>
            {
                _f[3] = v / 1000;
                additiveSynthesis.amplitudes[3] = _f[3];
                additiveSynthesis.paintWave();
            });

            _sliders[4].onValueChanged.AddListener((v) =>
            {
                _f[4] = v / 1000;
                additiveSynthesis.amplitudes[4] = _f[4];
                additiveSynthesis.paintWave();
            });

            _sliders[5].onValueChanged.AddListener((v) =>
            {
                _f[5] = v / 1000;
                additiveSynthesis.amplitudes[5] = _f[5];
                additiveSynthesis.paintWave();
            });

            _sliders[6].onValueChanged.AddListener((v) =>
            {
                _f[6] = v / 1000;
                additiveSynthesis.amplitudes[6] = _f[6];
                additiveSynthesis.paintWave();
            });

            _sliders[7].onValueChanged.AddListener((v) =>
            {
                _f[7] = v / 1000;
                additiveSynthesis.amplitudes[7] = _f[7];
                additiveSynthesis.paintWave();
            });

            _sliders[8].onValueChanged.AddListener((v) =>
            {
                _f[8] = v / 1000;
                additiveSynthesis.amplitudes[8] = _f[8];
                additiveSynthesis.paintWave();
            });

            _sliders[9].onValueChanged.AddListener((v) =>
            {
                _f[9] = v / 1000;
                additiveSynthesis.amplitudes[9] = _f[9];
                additiveSynthesis.paintWave();
            });

            _sliders[10].onValueChanged.AddListener((v) =>
            {
                _f[10] = v / 1000;
                additiveSynthesis.amplitudes[10] = _f[10];
                additiveSynthesis.paintWave();
            });

            _sliders[11].onValueChanged.AddListener((v) =>
            {
                _f[11] = v / 1000;
                additiveSynthesis.amplitudes[11] = _f[11];
                additiveSynthesis.paintWave();
            });
            #endregion
        }


    }
    //Creación onda sinuidal.
    public void setSinusWave()
    {
        _sliders[0].value = 1.0f * 1000;
        for (int i = 1; i < additiveSynthesis.amplitudes.Length; i++)
        {   
            _sliders[i].value = 0.0f;
        }
    }
    //Creación onda sierra.
    public void setSaw()
    {
        for (int i = 0; i < additiveSynthesis.amplitudes.Length; i++)
        {
            float x = 1.0f / (i + 1);
            _sliders[i].value = x * 1000;
        }
    }
    //Creación onda cuadrada
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
    //Creación pulso
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

    //public void setPulseButton()
    //{
    //    if (additiveSynthesis.playingCustomTone)
    //        additiveSynthesis.setAdditiveEnable(false);
    //    else
    //    {
    //        additiveSynthesis.setAdditiveEnable(true);
    //        additiveSynthesis.drawing = isDrawing;
    //        setPulse();
    //        audioSource.Play();
    //        StartCoroutine(waiter());
    //    }
    //}

    //Corutina para hacer que el impulso dure 0.1 segundos
    IEnumerator waiter()
    {
        Debug.Log("Entró");
        yield return new WaitForSeconds(0.1f);
        additiveSynthesis.setAdditiveEnable(false);
        audioSource.Stop();
    }
}
