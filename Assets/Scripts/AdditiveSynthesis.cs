using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveSynthesis : MonoBehaviour
{
    const int nPartials = 6; // Including f0

    public float[] amplitudes;
    public float[] newAmplitudes;

    public float Frequency = 480;
    public float Amplitude = 1.0f;
    public float Phase = 0.0f;

    float t = 0;
    float timestep;

    public int width = 260;
    public int height = 200;
    [SerializeField] Color waveformColor;
    [SerializeField] Color bgColor;
    
    public float[] samples;
    public float[] samples2;
    Draw draw;

    private void Awake()
    {
        samples = new float[2048];
        samples2 = new float[2048];
        amplitudes = new float[nPartials];
        newAmplitudes = new float[nPartials];
        draw = this.GetComponent<Draw>();
        timestep = 1.0f / AudioSettings.outputSampleRate;
        paintWave();
    }

    private void Update()
    {
        for(int i = 0; i < amplitudes.Length; i++)
        {
            if (newAmplitudes[i] != amplitudes[i])
            {
                float dif = Mathf.Abs(newAmplitudes[i] - amplitudes[i]);
                newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.01f);
                //if (dif >= 0.5)
                //    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.01f);
                //else if (dif >= 0.1)
                //    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.02f);
                //else if (dif >= 0.05)
                //    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.04f);
                //else
                //            newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.08f);
            }
        }
    }

    public void paintWave()
    {
        float j = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            samples2[i] = AddPartials2(j, amplitudes);

            j += timestep;
        }
        Texture2D texture = draw.PaintWaveformSpectrum2(samples2, width, height, waveformColor, bgColor);
        draw.img.overrideSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        int nsamples = data.Length / channels;
        double currentdsptime = AudioSettings.dspTime;

        for (int i = 0; i < nsamples; i += channels)
        {
            samples[i]= AddPartials2(t, newAmplitudes);
            for (int j = 0; j < channels; j++)
            {
                data[i * channels + j] = samples[i];
            }
            t += timestep;
        }
    }

    float AddPartials(float t, float[] ampls)
    {
        float partialAmplitude = Amplitude / (float)nPartials;
        float sample = 0.0f;
        float increment = (Frequency * 2.0f * Mathf.PI) * timestep;
        Phase += increment;
        //if (Phase > 2.0f * Mathf.PI) Phase -= 2.0f * Mathf.PI;
        for (int i = 0; i < nPartials; i++)
        {
            float partialsample = partialAmplitude * ampls[i] * Mathf.Sin((i + 1) * Phase);
            sample += partialsample;
        }
        return sample;
    }

    float AddPartials2(float t, float[] newAmplitudes)
    {
        float partialAmplitude = Amplitude / (float)nPartials;
        float sample = 0.0f;
        for (int i = 0; i < nPartials; i++)
        {
            float partialsample = partialAmplitude * newAmplitudes[i] * Mathf.Sin((i + 1) * (Frequency * 2.0f * Mathf.PI) * t);
            sample += partialsample;
        }
        return sample;
    }
}
