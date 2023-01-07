using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveSynthesis : MonoBehaviour
{
    const int nPartials = 10; // Including f0

    public float[] amplitudes = new float[nPartials];
    public float[] beforeAmplitudes = new float[nPartials];

    float Frequency = 480;
    float Amplitude = 1.0f;
    float Phase = 0.0f;

    float t = 0;
    float timestep;

    public int width = 260;
    public int height = 200;
    [SerializeField] Color waveformColor;
    [SerializeField] Color bgColor;
    public float sat = .5f;

    float sample;
    public float[] samples;
    public float[] finalSamples;
    Draw draw;

    private void Awake()
    {
        samples = new float[200];
        finalSamples = new float[200];
        draw = this.GetComponent<Draw>();
        timestep = 1.0f / AudioSettings.outputSampleRate;
        paintWave();
    }

    public void paintWave()
    {
           
            t = 0;
            for (int i = 0; i < 200; i++)
            {
                samples[i] = AddPartials(t);
                
                //finalSamples[i] = n;
                t += timestep;
            }
            Texture2D texture = draw.PaintWaveformSpectrum2(samples, width, height, waveformColor, bgColor);
            draw.img.overrideSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));


    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        int nsamples = data.Length / channels;
        double currentdsptime = AudioSettings.dspTime;
        for (int i = 0; i < nsamples; i += channels)
        {

            sample = AddPartials(t);
            for (int j = 0; j < channels; j++)
            {
                data[i * channels + j] = sample;
            }
            t += timestep;
        }
    }

    float AddPartials(float t)
    {
        float partialAmplitude = Amplitude / (float)nPartials;
        float sample = 0.0f;

        for (int i = 0; i < nPartials; i++)
        {
            float partialsample = partialAmplitude * amplitudes[i] * Mathf.Sin(2.0f * Mathf.PI * ((i + 1) * Frequency) * t + Phase);
            sample += partialsample;
        }
        return sample;
    }
}
