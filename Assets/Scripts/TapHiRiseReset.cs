using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapHiRiseReset : MonoBehaviour {


	public void ResetHiRiseSelection()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("hiriseLocation");
		foreach (var go in gos) {
			go.GetComponent<HiRiseNameID> ().iconDeselected ();
		}

		GameObject[] gos2 = GameObject.FindGameObjectsWithTag ("hiriseIcon");
		foreach (var go in gos2) {
			Destroy (go);
		}

		GameObject mars = GameObject.FindGameObjectWithTag ("Mars");
		mars.GetComponent<HiRiseIconInteraction> ().iconCounter = 0;
	}
}
