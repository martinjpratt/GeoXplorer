using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;

public class MakeManipulatable : MonoBehaviour {


	public void AddManipulation(){
		GameObject manipulatableObject = GameObject.FindGameObjectWithTag ("scalable");
		manipulatableObject.AddComponent<TwoHandManipulatable>();
		HoloPort[] holoporter = manipulatableObject.GetComponentsInChildren<HoloPort> ();
		foreach (var hp in holoporter) {
			hp.enabled = false;
		}

		GameObject scaleObject = GameObject.FindGameObjectWithTag ("scaleController");
		scaleObject.GetComponent<ScaleObject> ().enabled = false;
	}

	public void ResetManipulation(){
		GameObject manipulatableObject = GameObject.FindGameObjectWithTag ("scalable");
		Destroy(manipulatableObject.GetComponent<TwoHandManipulatable>());
		HoloPort[] holoporter = manipulatableObject.GetComponentsInChildren<HoloPort> ();
		foreach (var hp in holoporter) {
			hp.enabled = true;
		}

		GameObject scaleObject = GameObject.FindGameObjectWithTag ("scaleController");
		scaleObject.GetComponent<ScaleObject> ().enabled = true;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
