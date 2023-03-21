using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class AdditiveSynthesis : MonoBehaviour
{
    [SerializeField] Color waveformColor;
    [SerializeField] Color bgColor;
    public int width = 260;
    public int height = 200;

    const int nPartials = 12; // Including f0

    public float[] amplitudes;
    public float[] newAmplitudes;

    public float Frequency = 480;
    public float Amplitude = 1.0f;
    public float Phase = 0.0f;
    public float previousPhase = 0.0f;
    public float sampleRate;

    float t = 0;
    float timestep;
    
    public float[] samples;
    
    Draw draw;

    // Vocal tract
    private int vocalFreq = 90;
    private float vocalCordsAmp = 0.5f;
    private float breathAmp = 0.5f;
    bool playVocalCords = false;
    bool playBreath = false;
    private float currentSample = 0.0f;
    public bool playingCustomTone = false;
    private double currentDspTime;

    // Custom tone
    private AudioSource audioSource;
    private double dataLen;     // the data length of each channel
    private double chunkTime;
    private double dspTimeStep;

    //Convolution
    [SerializeField] AudioClip p4impulse;
    [SerializeField] AudioClip p4brir;
    [SerializeField] AudioClip p4dry;
    [SerializeField] AudioClip p4wet;

    public bool drawing = true;

    private bool isRightMuted = false;
    private bool isRightHigher = false;

    private void Awake()
    {
        samples = new float[1024];
        newAmplitudes = new float[nPartials];
        amplitudes = new float[nPartials];
        amplitudes[0] = 1.0f;
        previousPhase = Phase;
        draw = this.GetComponent<Draw>();
        sampleRate = AudioSettings.outputSampleRate;
        timestep = 1.0f / sampleRate;
        audioSource = this.GetComponent<AudioSource>(); 
        paintWave();
    }

    public void paintWave()
    {
        float j = 0;
        float[] samples2 = new float[1024];
        for (int i = 0; i < samples2.Length; i++)
        {
            samples2[i] = AddPartials2(j, amplitudes);

            j += timestep;
        }
        if(drawing)
        {
            Debug.Log("Drawing");
            Texture2D texture = draw.PaintWaveformSpectrum2(samples2, width, height, waveformColor, bgColor);
            draw.img.overrideSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if(playingCustomTone)
        {
            int nsamples = data.Length / channels;
            for (int i = 0; i < nsamples; i += channels)
            {
                samples[i] = AddPartials(newAmplitudes);
                for (int j = 0; j < channels; j++)
                {
                    data[i * channels + j] = samples[i];
                }
            }
        }
        else
        {
            // DSP timing
            currentDspTime = AudioSettings.dspTime;
            dataLen = data.Length / channels;   // the actual data length for each channel
            chunkTime = dataLen / sampleRate;   // the time that each chunk of data lasts
            dspTimeStep = chunkTime / dataLen;  // the time of each dsp step. (the time that each individual audio sample (actually a float value) lasts)
            if (playVocalCords)
            {
                double steps = sampleRate / vocalFreq;
                double increment = vocalCordsAmp / steps;

                // Calculate samples
                double preciseDspTime;
                for (int i = 0; i < dataLen; i++)
                {
                    preciseDspTime = currentDspTime + i * dspTimeStep;

                    currentSample += (float)increment;
                    if (currentSample >= vocalCordsAmp)
                        currentSample = 0.0f;

                    // Apply to both channels
                    for (int j = 0; j < channels; j++)
                    {
                        data[i * channels + j] = currentSample;
                    }
                }
            }
            if(playBreath)
            {
                System.Random random = new System.Random();
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] += (float)random.NextDouble() * breathAmp;
                }
            }
        }
    }

    float AddPartials(float[] ampls)
    {
        float partialAmplitude = Amplitude / (float)nPartials;
        float sample = 0.0f;
        float increment = (Frequency * 2.0f * Mathf.PI) * timestep;
        Phase += increment;
        updateAmplitudes();
        if (Phase > 2.0f * Mathf.PI) Phase -= 2.0f * Mathf.PI;
        for (int i = 0; i < nPartials; i++)
        {
            float partialsample = partialAmplitude * ampls[i] * Mathf.Sin((i + 1) * Phase);
            sample += partialsample;
        }
        return sample;
    }

    void updateAmplitudes()
    {
        for (int i = 0; i < amplitudes.Length; i++)
        {
            if (newAmplitudes[i] != amplitudes[i])
            {
                float dif = Mathf.Abs(newAmplitudes[i] - amplitudes[i]);
                if (dif >= 0.5)
                    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.01f);
                else if (dif >= 0.1)
                    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.02f);
                else if (dif >= 0.05)
                    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.03f);
                else
                    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.05f);
            }
        }
    }

    float AddPartials2(float t, float[] amps)
    {
        float partialAmplitude = Amplitude / (float)nPartials;
        float sample = 0.0f;
        for (int i = 0; i < nPartials; i++)
        {
            float partialsample = partialAmplitude * amps[i] * Mathf.Sin((i + 1) * (Frequency * 2.0f * Mathf.PI) * t);
            sample += partialsample;
        }
        return sample;
    }

    public void changeVocal()
    {
        audioSource.Play();
        playVocalCords = !playVocalCords;
    }
    public void changeBreath()
    {
        GameObject newButton;
        audioSource.Play();
        playBreath = !playBreath;
            
    }

    public void StopVocals()
    {
        playBreath = false;
        playVocalCords = false;
        audioSource.Stop();
    }

    public void startAdditive()
    {
        playingCustomTone = !playingCustomTone;
    }
}
