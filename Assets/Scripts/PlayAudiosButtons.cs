using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PlayAudiosButtons : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    [SerializeField] private Slider slider;
    private AdditiveSynthesis adSynthesis;
    // Start is called before the first frame update
    void Start()
    {

        //    audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        //    adSynthesis = audioSource.GetComponent<AdditiveSynthesis>();

        //}
        //// Comprobaci�n de si el audioclip actual es distinto al que posee el script en el editor
        //// -> Si es id�ntico y est� siendo reproducido, parar�
        //// -> Si es id�ntico y no se est� reproduciendo, empezar� a reproducirse.
        //// -> Si es distinto, cambiar� el audioClip y comenzar� la reproducci�n.
        //private void playSound()
        //{
        //    adSynthesis.setAdditiveEnable(false);
        //    audioSource.pitch = 1.0f;
        //    if (audioSource.clip != audioClip)
        //    {
        //        audioSource.clip = audioClip;
        //        audioSource.Play();
        //    }
        //    else
        //    {
        //        if (audioSource.isPlaying)
        //        {
        //            audioSource.Stop();
        //        }
        //        else
        //        {
        //            audioSource.Play();
        //        }
        //    }
        //}
        //public void playSoundEffect()
        //{
        //    audioSource.volume = slider.value;
        //    audioSource.panStereo = 0.0f;
        //    playSound();
        //}
        ////Bajar Volumen, parar la reproducci�n de audio procedural y comenzar la reproducci�n del audio
        //public void playLowAmplitudSound()
        //{
        //    adSynthesis.setAdditiveEnable(false);
        //    audioSource.volume = slider.value/10;
        //    playSound();
        //}
        ////Subir Volumen, parar la reproducci�n de audio procedural y comenzar la reproducci�n del audio
        //public void playHighAmplitudSound()
        //{
        //    adSynthesis.setAdditiveEnable(false);
        //    audioSource.volume = slider.value;
        //    playSound();
    }
}
