using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
public class LPF : MonoBehaviour
{
    [SerializeField] Slider freqSlider;
    private AudioLowPassFilter audioLPF;
    private double sampleRate;
    AudioSource audioSource;

    [SerializeField] TextMeshProUGUI labelFreq;
    [SerializeField] TextMeshProUGUI labelDistance;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sampleRate = AudioSettings.outputSampleRate;
        audioLPF = GetComponent<AudioLowPassFilter>();
        audioLPF.cutoffFrequency = 0.5f * (float)sampleRate;
        changeLPF(2000);

        freqSlider.onValueChanged.AddListener((v) =>
        {
            changeLPF(v);
        });
    }

    /// <summary>
    /// Toggles the playback of the audio.
    /// </summary>
    public void PlayStop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// Modifies the LPF filter applied to the audio and updates the displayed information.
    /// </summary>
    /// <param name="v">The new cutoff frequency value.</param>
    private void changeLPF(float v)
    {
        audioLPF.cutoffFrequency = v;
        labelFreq.text = "Frecuencia de corte: " + v + "Hz";

        // Coarse approximation to distance based on cutoff
        //float distance = 15 + Mathf.Exp((20000 - v) * 0.0005f);
        float distance;
        if (v == 20000)
            distance = 0;
        else
            distance = Mathf.Exp((20000 - v) * 0.0005f);
        labelDistance.text = "Distancia: " + (int)distance + "m";
    }
}
