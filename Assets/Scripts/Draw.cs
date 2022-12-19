using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    Color waveformColor = Color.green;
    Color bgColor = Color.white;

    public Image img;
    public float[] samples;

    void Start()
    {
        samples = new float[200];
    }

    public Texture2D PaintWaveformSpectrum2(float[] samples, int width, int height, Color col, Color bgcol)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        float[] waveform = new float[samples.Length];
        int packSize = (samples.Length / width) + 1;
        int s = 0;
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

        for (int x = 0; x < waveform.Length; x++)
        {
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 337, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 338, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 339, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 340, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 341, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 342, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 343, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 344, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 345, col);

            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 346, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 347, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 348, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 349, col);
            
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 350, col);

            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 351, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 352, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 353, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 354, col);

            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 355, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 356, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 357, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 358, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 359, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 360, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 361, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 362, col);
            tex.SetPixel(x, (int)(samples[x] * ((float)height) * 2) + 363, col);


            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 96, col);
            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 97, col);
            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 98, col);
            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 99, col);
            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 100, col);
            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 101, col);
            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 102, col);
            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 103, col);
            //tex.SetPixel(x, (int)(samples[x] * ((float)height) * 0.75f) + 104, col);
            tex.SetPixel(x, 350, Color.white);
        }
        tex.Apply();

        return tex;
    }
}
