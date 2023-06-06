using API_3DTI_Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuneInController : MonoBehaviour
{
    [SerializeField] private API_3DTI_HL API_3DTI_HL;
    [SerializeField] private API_3DTI_HA API_3DTI_HA;
    bool conductive = false;
    bool hearingAid = false;
    bool neurosensor = false;

    public AudioClip audioClip;
    private AudioSource audioSource;

    [SerializeField] private Toggle hearingAidToggle;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        API_3DTI_HL.EnableHearingLossInBothEars(false);
        API_3DTI_HA.EnableHAInBothEars(false);
        API_3DTI_HL.DisableFrequencySmearingSimulation(T_ear.BOTH);
        API_3DTI_HL.DisableTemporalDistortionSimulation(T_ear.BOTH);
        API_3DTI_HL.DisableNonLinearAttenuation(T_ear.BOTH);
    }

    /// <summary>
    /// Plays the audioclip
    /// </summary>
    public void playClip()
    {
        if (audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (audioSource.isPlaying)
            audioSource.Stop();
        else
            audioSource.Play();
    }

    /// <summary>
    /// Enable/Disable Non Linear Attenuation.
    /// </summary>
    public void ToggleConductive()
    {
        if(!conductive)
        {
            API_3DTI_HL.EnableHearingLoss(T_ear.BOTH);
            API_3DTI_HL.EnableNonLinearAttenuation(T_ear.BOTH);
            hearingAidToggle.interactable = true;
        }
        else
        {
            API_3DTI_HL.DisableNonLinearAttenuation(T_ear.BOTH);
            hearingAidToggle.interactable = false;
            hearingAidToggle.isOn = false;
        }
        conductive = !conductive;
        checkIfEverythingDisable(); 
    }


    /// <summary>
    /// Enable/Disable Frequency Smearing Simulation and Temporal Distortion Simulation.
    /// </summary>
    public void ToggleNeuro()
    {
        if (!neurosensor)
        {
            API_3DTI_HL.EnableHearingLoss(T_ear.BOTH);
            API_3DTI_HL.EnableFrequencySmearingSimulation(T_ear.BOTH);
            API_3DTI_HL.EnableTemporalDistortionSimulation(T_ear.BOTH);
        }
        else
        {
            API_3DTI_HL.DisableFrequencySmearingSimulation(T_ear.BOTH);
            API_3DTI_HL.DisableTemporalDistortionSimulation(T_ear.BOTH);
        }
        neurosensor = !neurosensor;
        checkIfEverythingDisable();
    }


    /// <summary>
    /// Enable/Disable Hearing Aid
    /// </summary>
    public void ToggleHA()
    {
        if (!hearingAid && (conductive))
        {
            API_3DTI_HA.EnableHAInBothEars(true);
            hearingAid = true;
        }
        else
        {
            hearingAidToggle.isOn = false;
            API_3DTI_HA.EnableHAInBothEars(false);
            hearingAid = false;
        }
    }


    /// <summary>
    /// Checks if HearingLoss or Neurosensitive deaf is disable
    /// </summary>
    public void checkIfEverythingDisable()
    {
        if (!neurosensor && !conductive)
        {
            API_3DTI_HL.EnableHearingLossInBothEars(false);
            if(hearingAid)
            {
                hearingAidToggle.isOn = false;
                ToggleHA();
            }
        }
    }
}
