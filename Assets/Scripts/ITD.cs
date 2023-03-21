using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class ITD : MonoBehaviour
{

    public bool playingITD = false;
    private float sampleRate;
    public float ITDToneFreq = 200;
    public float newFrequency = 200;
    private float ITDRightPhase = 0;
    private float ITDDelay = 0;
    private float ITDToneAmp = 0.01f;

    [SerializeField] TextMeshProUGUI labelFreqP1;
    [SerializeField] TextMeshProUGUI labelAngleP1;
    [SerializeField] TextMeshProUGUI labelTimeP1;

    [SerializeField] Slider freqSlider;
    [SerializeField] Slider delaySlider;

    const float soundSpeed = 34300; // cm/s
    const float interauralDistance = 22; //cm 

    // Start is called before the first frame update
    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;


        freqSlider.onValueChanged.AddListener((v) =>
        {
            changeITDFrequencyText(v);
        });


        delaySlider.onValueChanged.AddListener((v) =>
        {
            changeITDDelay(v);
        });

    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        // DSP timing
        double currentDspTime = AudioSettings.dspTime;
        double dataLen = data.Length / channels;   // the actual data length for each channel
        double chunkTime = dataLen / sampleRate;   // the time that each chunk of data lasts
        double dspTimeStep = chunkTime / dataLen;  // the time of each dsp step. (the time that each individual audio sample (actually a float value) lasts)
        //updateFrequency();
        if (playingITD)
        {
            // SINE WAVE
            // Calculate samples            
            for (int i = 0; i < dataLen; i++)
            {
                double time = currentDspTime + i * dspTimeStep;

                float sampleL = ITDToneAmp * (float)Math.Sin(time * 2.0 * Math.PI * ITDToneFreq);
                float sampleR = ITDToneAmp * (float)Math.Sin(time * 2.0 * Math.PI * ITDToneFreq + ITDRightPhase);
                data[i * 2] = sampleL;
                data[(i * 2) + 1] = sampleR;
            }
        }
    }

    private void changeITDFrequencyText(float v)
    {
        labelFreqP1.text = "Frecuencia: " + v + "Hz";
    }

    public void changeITDFrequency()
    {
        ITDToneFreq = freqSlider.value;
        computeITDPhase();
    }

    private void changeITDDelay(float v)
    {
        ITDDelay = v;
        computeITDPhase();
    }

    void updateFrequency()
    {
        if(newFrequency != ITDToneFreq)
        {
            float dif = Math.Abs(newFrequency - ITDToneFreq);
            if(dif >= 1500)
                newFrequency = (float)Mathf.Lerp(newFrequency, ITDToneFreq, 0.01f);
            else if (dif >= 1000)
                newFrequency = (float)Mathf.Lerp(newFrequency, ITDToneFreq, 0.02f);
            else if (dif >= 500)
                newFrequency = (float)Mathf.Lerp(newFrequency, ITDToneFreq, 0.03f);
            else
                newFrequency = (float)Mathf.Lerp(newFrequency, ITDToneFreq, 0.05f);
        }
    }

    private void computeITDPhase()
    {
        // One period of the ITD tone is 1/ITDToneFreq seconds
        // In microseconds, it is T=10^6*(1/ITDToneFreq)
        // T is to 2PI radians, as our phase time "t" is to X radians
        // So: X = 2PI*t/T
        ITDRightPhase = (2 * Mathf.PI * ITDDelay * ITDToneFreq) * 0.000001f;
        labelTimeP1.text = "Retardo: " + ITDDelay + "us";

        // Angle
        float maxDelay = (interauralDistance / soundSpeed) * 1000000;
        float angle = -(ITDDelay / maxDelay) * 90;
        if (angle < 0)
            angle = 360 + angle;
        labelAngleP1.text = "Azimuth: " + (int)angle + "º";
    }

    public void playITD()
    {
        playingITD = !playingITD;
    }
}

