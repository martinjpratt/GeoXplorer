//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity;

public class GazeAudio : Singleton<GazeAudio> {

    //Adds sounds to click events

    public AudioClip HighlightEvent;
    public AudioClip RemoveHighlightClip;
    public AudioClip SelectClip;
    public AudioClip DeselectClip;
    public AudioClip DisabledSelectClip;
    public AudioClip ClickClip;
    public AudioClip DisabledClickClip;
    public AudioClip MoveToolsUpClip;
    public AudioClip MoveToolsDownClip;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("ToolSounds has no way to play sounds!");
        }
    }


    public void PlayHighlightSound()
    {
        audioSource.PlayOneShot(HighlightEvent);
    }

    public void PlayRemoveHighlightSound()
    {
        if (audioSource && RemoveHighlightClip)
        {
            audioSource.PlayOneShot(RemoveHighlightClip);
        }
    }

    public void PlaySelectSound()
    {
        if (audioSource && SelectClip)
        {
            audioSource.PlayOneShot(SelectClip);
        }
    }

    public void PlayDeselectSound()
    {
        if (audioSource && DeselectClip)
        {
            audioSource.PlayOneShot(DeselectClip);
        }
    }

    public void PlayDisabledSelectSound()
    {
        if (audioSource && DisabledSelectClip)
        {
            audioSource.PlayOneShot(DisabledSelectClip);
        }
    }

    public void PlayClickSound()
    {
        if (audioSource && ClickClip)
        {
            audioSource.PlayOneShot(ClickClip);
        }
    }

    public void PlayDisabledClickSound()
    {
        if (audioSource && DisabledClickClip)
        {
            audioSource.PlayOneShot(DisabledClickClip);
        }
    }

    public void PlayMoveToolsUpSound()
    {
        if (audioSource && MoveToolsUpClip)
        {
            audioSource.PlayOneShot(MoveToolsUpClip);
        }
    }

    public void PlayMoveToolsDownSound()
    {
        if (audioSource && MoveToolsDownClip)
        {
            audioSource.PlayOneShot(MoveToolsDownClip);
        }
    }
    
}
