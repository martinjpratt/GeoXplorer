//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapToAnimate : MonoBehaviour, IInputClickHandler {
    //Code to animate on click


    public void OnInputClicked(InputClickedEventData eventData)
    {
        this.GetComponent<Animator>().Play("MotionState");
    }
}
