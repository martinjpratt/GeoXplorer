using HoloToolkit.Unity.InputModule.Tests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSharingOnOff : MonoBehaviour {

    public GameObject masterObject;

    public void FindIconsAndShare()
    {
        TapResponderWithComponent[] tapResps = masterObject.GetComponentsInChildren<TapResponderWithComponent>();
        foreach (var tapResp in tapResps)
        {
            tapResp.sharingSelected = true;
        }
    }


    public void FindIconsAndNotShare()
    {
        TapResponderWithComponent[] tapResps = masterObject.GetComponentsInChildren<TapResponderWithComponent>();
        foreach (var tapResp in tapResps)
        {
            tapResp.sharingSelected = false;
        }
    }
}
