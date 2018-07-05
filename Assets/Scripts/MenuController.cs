using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class MenuController : MonoBehaviour {

	public void menuTagalongEnable(){
		this.GetComponent<SolverHandler>().enabled = true;
		this.GetComponent<SolverRadialView>().enabled = true;
	}

	public void menuTagalongDisable(){
		this.GetComponent<SolverHandler>().enabled = false;
		this.GetComponent<SolverRadialView>().enabled = false;
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
