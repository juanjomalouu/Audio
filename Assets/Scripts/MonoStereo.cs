using UnityEngine;

public class MonoStereo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] AudioClip instrument1Mono;
    [SerializeField] AudioClip instrument2Mono;
    [SerializeField] AudioClip instrument3Mono;
    [SerializeField] AudioClip instrument4Mono;
    [SerializeField] AudioClip instrument1Stereo;
    [SerializeField] AudioClip instrument2Stereo;
    [SerializeField] AudioClip instrument3Stereo;
    [SerializeField] AudioClip instrument4Stereo;
    [SerializeField] bool[] playInstr;

    bool isMono = false;

    public void Start()
    {
        audioSources = GameObject.FindGameObjectWithTag("AudioSource").GetComponents<AudioSource>();
        audioSources[0].clip = instrument1Mono;
        audioSources[1].clip = instrument2Mono;
        audioSources[2].clip = instrument3Mono;
        audioSources[3].clip = instrument4Mono;
        audioSources[4].clip = instrument1Stereo;
        audioSources[5].clip = instrument2Stereo;
        audioSources[6].clip = instrument3Stereo;
        audioSources[7].clip = instrument4Stereo;
        foreach(AudioSource source in audioSources)
        {
            source.volume = 0.0f;
        }
        playInstr = new bool[4];
        for(int i = 0; i < playInstr.Length; i++)
        {
            playInstr[i] = false;
        }
    }

    public void setMonoAudio()
    {
        if (!checkIsSomethingEnable())
        {
            return;
        }
        if (!isMono)
        {
            for(int i = 0; i < audioSources.Length; i++)
            {
                if(i / 4 == 0 && playInstr[i%4])
                    audioSources[i].volume = 1;
                else if (playInstr[i%4])
                    audioSources[i].volume = 0;
            }
        }
        if(audioSources[0].isPlaying && isMono)
        {
            foreach(AudioSource ad in audioSources)
            {
                ad.Stop();
            }
        }
        else if(!audioSources[0].isPlaying)
        {
            foreach (AudioSource ad in audioSources)
            {
                ad.Play();
            }
        }
        isMono = true;
    }

    public void setStereoAudio()
    {
        if(!checkIsSomethingEnable())
        {
            return;
        }
        if (isMono)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                if (i / 4 == 0 && playInstr[i % 4])
                    audioSources[i].volume = 0;
                else if(playInstr[i%4])
                    audioSources[i].volume = 1;
            }
        }
        if (audioSources[0].isPlaying && !isMono)
        {
            foreach (AudioSource ad in audioSources)
            {
                ad.Stop();
            }
        }
        else if(!audioSources[0].isPlaying)
        {
            foreach (AudioSource ad in audioSources)
            {
                ad.Play();
            }
        }
        isMono = false;
    }

    public bool checkIsSomethingEnable()
    {
        foreach (bool enable in playInstr)
        {
            if (enable)
                return true;
        }
        return false;
    }

    public void setVolumeInstrument(int i)
    {
        if (playInstr[i])
        {
            audioSources[i].volume = 0;
            audioSources[i + 4].volume = 0;
        }
        else
        {
            if(isMono)
                audioSources[i].volume = 1;
            else
                audioSources[i + 4].volume = 1;
        }
        playInstr[i] = !playInstr[i];
    }

}
