using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveSynthesis : MonoBehaviour
{
    const int nPartials = 10; // Including f0

    public float[] amplitudes;
    public float[] newAmplitudes;

    float Frequency = 480;
    float Amplitude = 1.0f;
    float Phase = 0.0f;

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
                float dif = newAmplitudes[i] - amplitudes[i];
                if (dif >= 0.5)
                    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.01f);
                else if(dif >= 0.2)
                    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.05f);
                else if (dif >= 0.1)
                    newAmplitudes[i] = (float)Mathf.Lerp(newAmplitudes[i], amplitudes[i], 0.1f);
            }
        }
    }

    public void paintWave()
    {
        float j = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            samples2[i] = AddPartials(j, newAmplitudes);

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
            samples[i]= AddPartials(t, newAmplitudes);
            for (int j = 0; j < channels; j++)
            {
                data[i * channels + j] = samples[i];
            }
            t += timestep;
        }
    }

    float AddPartials(float t, float[] newAmplitudes)
    {
        float partialAmplitude = Amplitude / (float)nPartials;
        float sample = 0.0f;

        for (int i = 0; i < nPartials; i++)
        {
            float partialsample = partialAmplitude * newAmplitudes[i] * Mathf.Sin(2.0f * Mathf.PI * ((i + 1) * Frequency) * t + Phase);
            sample += partialsample;
        }
        return sample;
    }
}
