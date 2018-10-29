using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPoint : MonoBehaviour {

    public void ClearPointSelection()
    {
        Destroy(GameObject.FindGameObjectWithTag("infoMarker"));
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
