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
        float multiplier = 1.0f;
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
            for(int j = middle - dif; j < middle + dif; j++)
            {
                if((int)(samples[x] * ((float)height) * multiplier) + j < height && (int)(samples[x] * ((float)height) * multiplier) + j > 0)
                    tex.SetPixel(x, (int)(samples[x] * ((float)height) * multiplier) + j, col);
                for (int l = 0; l < Mathf.Abs(diference); l++)
                {
                    if ((int)(samples[x] * ((float)height) * multiplier) + j - l < height && (int)(samples[x] * ((float)height) * multiplier) + j - l > 0)
                        tex.SetPixel(x, (int)(samples[x] * ((float)height) * multiplier) + j - l, col);
                    if ((int)(samples[x] * ((float)height) * multiplier) + j + l < height && (int)(samples[x] * ((float)height) * multiplier) + j + l > 0)
                        tex.SetPixel(x, (int)(samples[x] * ((float)height) * multiplier) + j + l, col);
                }
            }
            if (x!= 0)
                diference = ((int)(samples[x] * ((float)height) * multiplier)) - ((int)(samples[x-1] * ((float)height) * multiplier));
            tex.SetPixel(x, middle, Color.white);
        }
        tex.Apply();

        return tex;
    }
}
