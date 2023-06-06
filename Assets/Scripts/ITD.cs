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
    public float sampleRate;
    public float ITDToneFreq = 200;
    public float newFrequency = 200;
    public float ITDRightPhase = 0;
    public float ITDDelay = 0;
    public float ITDToneAmp = 0.01f;

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

    /// <summary>
    /// Applies the ITD filter to the audio clip being played.
    /// </summary>
    /// <param name="data">Array of audio samples.</param>
    /// <param name="channels">Number of audio channels.</param>
    void OnAudioFilterRead(float[] data, int channels)
    {
        // DSP timing
        double currentDspTime = AudioSettings.dspTime;
        double dataLen = data.Length / channels;   // the actual data length for each channel
        double chunkTime = dataLen / sampleRate;   // the time that each chunk of data lasts
        double dspTimeStep = chunkTime / dataLen;  // the time of each dsp step. (the time that each individual audio sample (actually a float value) lasts)

        if (playingITD)
        {
            // SINE WAVE
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


    /// <summary>
    /// Changes the value of the frequency text panel.
    /// </summary>
    /// <param name="v">The new frequency value.</param>
    private void changeITDFrequencyText(float v)
    {
        labelFreqP1.text = "Frecuencia: " + v + "Hz";
    }

    /// <summary>
    /// Receives the information when the frequency slider is changed and calculates the azimuth.
    /// </summary>
    public void changeITDFrequency()
    {
        ITDToneFreq = freqSlider.value;
        computeITDPhase();
    }
    /// <summary>
    /// Changes the value of ITD according to the slider and calculates the azimuth.
    /// </summary>
    /// <param name="v">The new delay value.</param>
    private void changeITDDelay(float v)
    {
        ITDDelay = v;
        computeITDPhase();
    }

    /// <summary>
    /// Calculates the ITD phase along with the delay and azimuth and modifies the displayed text in the application.
    /// </summary>
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

    /// <summary>
    /// Toggles the ITD playback.
    /// </summary>
    public void playITD()
    {
        playingITD = !playingITD;
    }
}

