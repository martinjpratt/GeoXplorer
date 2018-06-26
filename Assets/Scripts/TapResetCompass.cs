//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    public class TapResetCompass : MonoBehaviour, IInputClickHandler, IFocusable
    {

        public GameObject resetObject;

        Material cachedMaterial;
        Color originalColor;
        Vector3 originalPosition;

        private void Awake()
        {
            cachedMaterial = GetComponent<Renderer>().material;
            originalColor = cachedMaterial.GetColor("_Color");

            //Define start position
            originalPosition = resetObject.transform.localPosition;
        }

        public void OnFocusEnter()
        {
            cachedMaterial.SetColor("_Color", Color.red);
        }

        public void OnFocusExit()
        {
            cachedMaterial.SetColor("_Color", originalColor);
        }

        //Set original position on AirTap
        public void OnInputClicked(InputClickedEventData eventData)
        {
            resetObject.transform.localPosition = originalPosition;
            
            cachedMaterial.SetColor("_Color", originalColor);
        }
    }
}
