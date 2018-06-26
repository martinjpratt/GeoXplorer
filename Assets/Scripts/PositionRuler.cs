//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class PositionRuler : MonoBehaviour {

    //place the measuring ruler at the gaze hit position

    public GameObject measureSphere;

    public void updatePosition()
    {
        gameObject.transform.position = GazeManager.Instance.HitInfo.point;
    }

    public void measurePosition()
    {
        measureSphere.transform.position = GazeManager.Instance.HitInfo.point;
    }
}
