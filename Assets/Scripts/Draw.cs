using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    Color waveformColor = Color.green;
    Color bgColor = Color.white;

    public Image img;
    public float[] samples;
    public int dif;

    void Start()
    {
        samples = new float[1024];
    }

    public Texture2D PaintWaveformSpectrum2(float[] samples, int width, int height, Color col, Color bgcol)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        float[] waveform = new float[samples.Length];
        int packSize = (samples.Length / width) + 1;
        int s = 0;
        float multiplier = 2.0f;
        int middle = height / 2;
        for (int i = 0; i < samples.Length; i += packSize)
        {
            waveform[i] = samples[i];
            s++;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tex.SetPixel(x, y, bgcol);
            }
        }
        int diference = 0;
        for (int x = 0; x < waveform.Length; x++)
        {
            int currentSample = (int)(samples[x] * ((float)height) * multiplier);
            for (int j = middle - dif; j < middle + dif; j++)
            {
                if (currentSample + j < height && currentSample + j > 0)
                    tex.SetPixel(x, currentSample + j, col);
                //for (int l = 0; l < Mathf.Abs(diference); l++)
                //{
                //    if (currentSample + j - l < height && currentSample + j - l > 0)
                //        tex.SetPixel(x, currentSample + j - l, col);
                //    if (currentSample + j + l < height && currentSample + j + l > 0)
                //        tex.SetPixel(x, currentSample + j + l, col);
                //}
            }
            if (x!= 0)
                diference = (currentSample) - ((int)(samples[x-1] * ((float)height) * multiplier));
            tex.SetPixel(x, middle, Color.white);
        }
        tex.Apply();

        return tex;
    }
}
