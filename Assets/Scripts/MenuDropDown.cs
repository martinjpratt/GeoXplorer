//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MenuDropDown : MonoBehaviour, IInputClickHandler, IFocusable
{
    //Code to animate the menu dropdown for multiple model locations at a particular site

    public List<GameObject> iList = new List<GameObject>();
    bool selected = false;
    Material cachedMaterial;
    Color originalColor;
    Animator anim;

    private void Awake()
    {
        cachedMaterial = GetComponent<Renderer>().material;
        originalColor = cachedMaterial.GetColor("_Color");
    }

    public void OnFocusEnter()
    {
        cachedMaterial.SetColor("_Color", Color.red);
    }

    public void OnFocusExit()
    {
        cachedMaterial.SetColor("_Color", Color.white);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (selected != true)
        {
            foreach (var item in iList)
            {
                item.SetActive(true);
                anim = item.GetComponent<Animator>();
                anim.Play("FadeIn");

            }
            GazeAudio.Instance.PlayClickSound();
            cachedMaterial.SetColor("_Color", originalColor);
            selected = true;
        }
        else
        {
            foreach (var item in iList)
            {
                anim = item.GetComponent<Animator>();
                anim.Play("FadeOut");
            }
            selected = false;
        }
        
    }
}
