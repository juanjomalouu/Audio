using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class PlayAudioClip : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip audioClip;
    private GameObject audio;
    [SerializeField] private AudioSource audioSource;
    private AdditiveSynthesis additiveSynthesis;

    [SerializeField] private bool showAudioBar = true;
    [SerializeField] private Slider audioBarSlider;
    [SerializeField] private TextMeshProUGUI textLabel;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioSource");
        audioSource = audio.GetComponent<AudioSource>();
        additiveSynthesis = audio.GetComponent<AdditiveSynthesis>();

        if(showAudioBar)
        {
            audioBarSlider.gameObject.SetActive(false);
            audioBarSlider.minValue = 0;
            audioBarSlider.maxValue = audioClip.length;
        }
    }

    // Comprobaci�n de si el audioclip actual es distinto al que posee el script en el editor
    // -> Si es id�ntico y est� siendo reproducido, parar�
    // -> Si es id�ntico y no se est� reproduciendo, empezar� a reproducirse.
    // -> Si es distinto, cambiar� el audioClip y comenzar� la reproducci�n.
    public void playClip()
    {
        if (additiveSynthesis != null)
        {
            additiveSynthesis.StopVocals();

            additiveSynthesis.setAdditiveEnable(false);
        }
        if (audioSource.clip != audioClip)
        {
            Debug.Log("ea");
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (audioSource.isPlaying)
        {
            Debug.Log("1");
            audioSource.Stop();
        }
        else
        {
            Debug.Log("2");
            audioSource.Play();
        }
        if(showAudioBar)
        {
            if(audioSource.isPlaying)
                enableSlider();
            else
                disableSlider();
        }
            

    }

    private void Update()
    {
        if(showAudioBar)
            updateSlider();
    }
    //Activar Slider de reproducci�n
    private void enableSlider()
    {
        audioBarSlider.gameObject.SetActive(true);

        audioBarSlider.value = 0;
        textLabel.gameObject.SetActive(false);
    }
    //Desactivar Slider de reproducci�n
    private void disableSlider()
    {
        audioBarSlider.gameObject.SetActive(false);
        textLabel.gameObject.SetActive(true);
    }
    //Actualizar la progresi�n del slider de reproducci�n
    private void updateSlider()
    {
        if ((additiveSynthesis != null && additiveSynthesis.playingCustomTone) || audioSource.clip != audioClip)
        {
            disableSlider();
        }
        if(!audioSource.isPlaying)
            disableSlider();
        if (audioBarSlider != null)
            audioBarSlider.value = audioSource.time;
    }
}

