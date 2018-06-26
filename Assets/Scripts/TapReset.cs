//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapReset : MonoBehaviour, IInputClickHandler, IFocusable
{
    //Resets the target object to it's original position on airtap

    public GameObject resetObject;

    Material cachedMaterial;
    Color originalColor;
    public Vector3 originalPosition;

    private void Awake()
    {
        cachedMaterial = GetComponent<Renderer>().material;
        originalColor = cachedMaterial.GetColor("_Color");

        originalPosition = resetObject.transform.eulerAngles;
        //Debug.Log(originalPosition);
    }

    public void OnFocusEnter()
    {
        cachedMaterial.SetColor("_Color", Color.red);
    }

    public void OnFocusExit()
    {
        cachedMaterial.SetColor("_Color", originalColor);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        resetObject.transform.eulerAngles = originalPosition;

        cachedMaterial.SetColor("_Color", originalColor);
    }
    
}
