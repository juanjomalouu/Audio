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

    //Number of modifiable amplitudes
    const int nPartials = 12; // Including f0

    public float[] amplitudes;
    public float[] newAmplitudes;

    public int Frequency = 480;
    public float sampleRate;
    public float Amplitude = 1.0f;
    public float Phase = 0.0f;
    public float previousPhase = 0.0f;
    public float phaseModification = 0.0f;

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


    /// <summary>
    /// Paints the waveform on the 'Additive Synthesis' screen.
    /// </summary>
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

    /// <summary>
    /// Creates and assigns the custom tone.
    /// </summary>
    /// <param name="data">The audio data.</param>
    /// <param name="channels">The number of audio channels.</param>
    private void OnAudioFilterRead(float[] data, int channels)
    {
        // Custom tone
        if (playingCustomTone)
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
        // Vocal implementation
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
            // Breath implementation (white noise)
            if (playBreath)
            {
                System.Random random = new System.Random();
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] += (float)random.NextDouble() * breathAmp;
                }
            }
        }
    }
    /// <summary>
    /// Calculates the custom tone based on the amplitudes.
    /// </summary>
    /// <param name="ampls">The array of amplitudes.</param>
    /// <returns>The calculated sample.</returns>
    float AddPartials(float[] ampls)
    {
        float partialAmplitude = Amplitude / (float)nPartials;
        float sample = 0.0f;
        float increment = (Frequency * 2.0f * Mathf.PI) * timestep;
        Phase += increment;
        Phase += phaseModification;
        updateAmplitudes();
        // Phase Control
        if (Phase > 2.0f * Mathf.PI) Phase %= 2.0f * Mathf.PI;
        for (int i = 0; i < nPartials; i++)
        {
            float partialsample = partialAmplitude * ampls[i] * Mathf.Sin((i + 1) * Phase);
            sample += partialsample;
        }
        return sample;
    }

    /// <summary>
    /// Updates the amplitudes smoothly to avoid clicks.
    /// </summary>
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

    /// <summary>
    /// Calculates the custom tone for drawing purposes, without affecting the sound calculation.
    /// </summary>
    /// <param name="t">The time parameter.</param>
    /// <param name="amps">The array of amplitudes.</param>
    /// <returns>The calculated sample.</returns>
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

    /// <summary>
    /// Inverts the boolean value of playVocalCords.
    /// </summary>
    /// <param name="disable">Flag to disable other sound modes.</param>
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

    /// <summary>
    /// Inverts the boolean value of playBreath.
    /// </summary>
    /// <param name="disable">Flag to disable other sound modes.</param>
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

    /// <summary>
    /// Stops the vocal tract sound.
    /// </summary>
    public void StopVocals()
    {
        playBreath = false;
        playVocalCords = false;
    }

    /// <summary>
    /// Activates or deactivates the procedural audio playback and prevents it from being applied to a clip already set.
    /// </summary>
    /// <param name="isEnable">Flag to enable or disable procedural audio playback.</param>

    public void setAdditiveEnable(bool isEnable)
    {
        if(isEnable && !playingCustomTone)
        {
            audioSource.clip = null;
        }
        playingCustomTone = isEnable;
    }

    /// <summary>
    /// Inverts the boolean value of playingCustomTone.
    /// </summary>
    /// <param name="disableVocals">Flag to disable vocal sound modes.</param>

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
