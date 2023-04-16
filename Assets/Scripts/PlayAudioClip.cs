using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip audioClip;

    private GameObject audio;
    private AudioSource audioSource;
    private AdditiveSynthesis additiveSynthesis;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioSource");
        audioSource = audio.GetComponent<AudioSource>();

        additiveSynthesis = audio.GetComponent<AdditiveSynthesis>();
    }

    // Update is called once per frame
    public void playClip()
    {
        audioSource.panStereo = 0;
        if (audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (audioSource.isPlaying)
            audioSource.Stop();
        else
            audioSource.Play();
        if(additiveSynthesis!= null)
            additiveSynthesis.setAdditiveEnable(false);
    }
}
