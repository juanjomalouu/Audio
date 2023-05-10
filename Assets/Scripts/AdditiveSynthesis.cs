using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class AdditiveSynthesis : MonoBehaviour
{
    [SerializeField] Color waveformColor;
    [SerializeField] Color bgColor;
    public int width = 260;
    public int height = 200;

    //Numero de amplitudes que habrá modificables
    const int nPartials = 12; // Including f0

    public float[] amplitudes;
    public float[] newAmplitudes;

    public int Frequency = 480;
    public float Amplitude = 1.0f;
    public float Phase = 0.0f;
    public float phaseModification = 0.0f;
    public float previousPhase = 0.0f;
    public float sampleRate;

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

    public bool drawing = true;

    private void Awake()
    {
        samples = new float[1024];
        newAmplitudes = new float[nPartials];
        amplitudes = new float[nPartials];
        amplitudes[0] = 1.0f;
        draw = this.GetComponent<Draw>();
        sampleRate = AudioSettings.outputSampleRate;
        timestep = 1.0f / sampleRate;
        audioSource = this.GetComponent<AudioSource>(); 
        paintWave();
    }


    //Función con la que pintar la onda de la pantalla 'Additive Synthesis'
    public void paintWave()
    {
        float j = 0;
        float[] samples_paint = new float[1024];
        for (int i = 0; i < samples_paint.Length; i++)
        {
            samples_paint[i] = AddPartials_draw(j, amplitudes);

            j += timestep;
        }
        if(drawing)
        {
            Texture2D texture = draw.PaintWaveformSpectrum2(samples_paint, width, height, waveformColor, bgColor);
            draw.img.overrideSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }

    //Creación y asignación del tono customizado.
    private void OnAudioFilterRead(float[] data, int channels)
    {
        //Tono customizado
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
        }//Implementación del tono vocal
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
            }//Implementación de la respiración (ruido blanco)
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
    //Cálculo del tono customizado
    float AddPartials(float[] ampls)
    {
        float partialAmplitude = Amplitude / (float)nPartials;
        float sample = 0.0f;
        float increment = (Frequency * 2.0f * Mathf.PI) * timestep;
        Phase += increment;
        Phase += phaseModification;
        updateAmplitudes();
        //Control de la fase
        if (Phase > 2.0f * Mathf.PI) Phase %= 2.0f * Mathf.PI;
        for (int i = 0; i < nPartials; i++)
        {
            float partialsample = partialAmplitude * ampls[i] * Mathf.Sin((i + 1) * Phase);
            sample += partialsample;
        }
        return sample;
    }

    //Actualización de las amplitudes de manera suave, para evitar clicks.
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

    //Mismo método que AddPartials, pero evitando cálculos innecesarios y utilizando variables distintas para que no afecte al cálculo del sonido.
    float AddPartials_draw(float t, float[] amps)
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

    //Invertir el valor booleano de playVocalCords
    public void changeVocal(bool disable = false)
    {
        if (disable)
        {
            playBreath = false;
            playingCustomTone = false;
        }
        audioSource.clip = null;
        audioSource.Play();
        playVocalCords = !playVocalCords;
    }
    //Invertir el valor booleano de changeBreath
    public void changeBreath(bool disable = false)
    {
        if (disable)
        {
            playVocalCords = false;
            playingCustomTone = false;
        }
        audioSource.clip = null;
        audioSource.Play();
        playBreath = !playBreath;
            
    }
    //Parar reproducción de tracto vocal.
    public void StopVocals()
    {
        playBreath = false;
        playVocalCords = false;
        audioSource.Stop();
    }
    //Activar la reproducción de audio procedural y evitar aplicarla a un clip ya puesto.
    public void setAdditiveEnable(bool isEnable)
    {
        if(isEnable && !playingCustomTone)
        {
            audioSource.clip = null;
        }
        playingCustomTone = isEnable;
    }
    //Invertir el valor del audio procedural
    public void toggleAdditive(bool disableVocals = false)
    {
        if(!playingCustomTone)
            audioSource.clip = null;
        if (disableVocals)
        {
            playBreath = playVocalCords = false;
        }
        playingCustomTone = !playingCustomTone;
    }
}
