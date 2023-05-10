using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverb : MonoBehaviour
{

    public AudioSource audioSource;
    private AudioReverbFilter audioReverb;
    private bool isDirect = false;
    private bool isEarly = false;
    private bool isTail = false;
    [SerializeField] AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioReverb = GetComponent<AudioReverbFilter>();
        audioSource.clip = clip;
        audioReverb.dryLevel = -10000;
    }
    //Activar/desactivar reproducción
    public void playStop()
    {
        if(!audioSource.isPlaying)
        {
            Debug.Log("Play");
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

    }
    //Activar/desactivar sonido directo
    public void toggleReverbDirect()
    {
        if (!isDirect)
        {
            audioReverb.dryLevel = 0;
        }
        else
            audioReverb.dryLevel = -10000;
        isDirect = !isDirect;
    }
    //Activar/desactivar primeras reflexiones
    public void toggleReverbEarly()
    {
        if (!isEarly)
        {
            audioReverb.room = -1500;
            audioReverb.reflectionsLevel = 1000;
        }
        else
            audioReverb.reflectionsLevel = -10000;
        isEarly = !isEarly;
    }
    //Activar o desactivar cola de reverberación
    public void toggleReverbTail()
    {
        if (!isTail)
        {
            audioReverb.room = -1500;
            audioReverb.reverbLevel = 1000;
        }
        else
            audioReverb.reverbLevel = -10000;
        isTail = !isTail;
    }
}
