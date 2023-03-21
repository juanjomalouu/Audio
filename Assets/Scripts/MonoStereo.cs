using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoStereo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip instrument1Mono;
    [SerializeField] AudioClip instrument2Mono;
    [SerializeField] AudioClip instrument3Mono;
    [SerializeField] AudioClip instrument4Mono;
    [SerializeField] AudioClip instrument1Stereo;
    [SerializeField] AudioClip instrument2Stereo;
    [SerializeField] AudioClip instrument3Stereo;
    [SerializeField] AudioClip instrument4Stereo;
    [SerializeField] bool playInst1 = false;
    [SerializeField] bool playInst2 = false;
    [SerializeField] bool playInst3 = false;
    [SerializeField] bool playInst4 = false;

    bool isMono = false;

    public void setMono()
    {
        if (isMono)
            audioSource.Stop();
        else
        {
            isMono = true;
            setAudioclips(isMono);
            //audioSource.Play();
        }
    }

    public void setStereo()
    {
        if (!isMono)
            audioSource.Stop();
        else
        {
            isMono = false;
            setAudioclips(isMono);
            //audioSource.Play();
        }
    }

    public void setAudioclips(bool isMono)
    {
        setInstrument(isMono, playInst1, instrument1Mono, instrument1Stereo);
        setInstrument(isMono, playInst2, instrument2Mono, instrument2Stereo);
        setInstrument(isMono, playInst3, instrument3Mono, instrument3Stereo);
        setInstrument(isMono, playInst4, instrument4Mono, instrument4Stereo);
    }

    public void toggleInstrument1()
    {
        playInst1 = !playInst1;            
    }

    public void toggleInstrument2()
    {
        playInst2 = !playInst2;
    }

    public void toggleInstrument3()
    {
        playInst3 = !playInst3;
    }

    public void toggleInstrument4()
    {
        playInst4 = !playInst4;
    }

    public void setInstrument(bool isMono, bool isEnable, AudioClip mono, AudioClip stereo)
    {
        if (isMono && isEnable)
            audioSource.PlayOneShot(mono, 1.0f);
        else
            audioSource.PlayOneShot(stereo, 1.0f);
    }

}
