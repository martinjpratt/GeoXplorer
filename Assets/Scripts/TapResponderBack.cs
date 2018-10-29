//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class TapResponderBack : MonoBehaviour, IFocusable, IInputClickHandler {
    //Code to allow the user to go back to a previous model stage

    public GameObject GoToObject;
    public GameObject TurnOffObject;

    Material cachedMaterial;
    //Color originalColor;

    private void Awake()
    {
        cachedMaterial = GetComponent<Renderer>().material;
        //originalColor = cachedMaterial.GetColor("_Color");
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
        GameObject[] flags = GameObject.FindGameObjectsWithTag("flag");
        foreach (GameObject fl in flags)
        {
            Destroy(fl);
        }

        GameObject outcropObject = GameObject.FindGameObjectWithTag("scalable");
        Destroy(outcropObject);

        Renderer[] rends = TurnOffObject.GetComponentsInChildren<Renderer>();
        foreach (var rend in rends)
        {
            rend.enabled = false;
        }
        Collider[] cols = TurnOffObject.GetComponentsInChildren<Collider>();
        foreach (var col in cols)
        {
            col.enabled = false;
        }

        Renderer[] goToRends = GoToObject.GetComponentsInChildren<Renderer>();
        foreach (var rend in goToRends)
        {
            rend.enabled = true;
        }
        Collider[] goToCols = GoToObject.GetComponentsInChildren<Collider>();
        foreach (var col in goToCols)
        {
            col.enabled = true;
        }

        
        GazeAudio.Instance.PlayClickSound();
        cachedMaterial.SetColor("_Color", Color.white);
    }
}
