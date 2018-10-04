using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiRiseNameID : MonoBehaviour {

	public string Name;
	public string ID;
	public bool selected;


	public void iconSelected(){
		this.GetComponent<Renderer> ().material.color = Color.blue;
		selected = true;
	}

	public void iconDeselected(){
		this.GetComponent<Renderer> ().material.color = Color.white;
		selected = false;
	}


}
