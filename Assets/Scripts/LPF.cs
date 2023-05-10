using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
public class LPF : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider freqSlider;
    private AudioLowPassFilter audioLPF;
    private double sampleRate;
    AudioSource audioSource;

    [SerializeField] TextMeshProUGUI labelFreq;
    [SerializeField] TextMeshProUGUI labelDistance;


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

    // Intercambio entre parar/reproducir el audio
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

    //Modificaci�n del filtro LPF que se aplica al audio y escritura de la informaci�n.
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
