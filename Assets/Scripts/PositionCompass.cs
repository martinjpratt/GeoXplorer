//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class PositionCompass : MonoBehaviour {
    
    //Shifts the strike and dip tool to the gaze hit position

    public void updatePosition()
    {
        gameObject.transform.position = GazeManager.Instance.HitInfo.point;
    }
}
