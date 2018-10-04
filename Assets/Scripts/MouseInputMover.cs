using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputMover : MonoBehaviour {

	public Transform target;
	public float xSpeed = 12.0f;
	public float ySpeed = 12.0f;
	public float scrollSpeed = 10.0f;
	public float zoomMin = 1.0f;
	public float zoomMax = 20.0f;
	public float distance ;
	public Vector3 position;
	public bool isActivated;
	float x = 0.0f;
	float y = 0.0f;

	// Use this for initialization
	void Start () {
		transform.LookAt (Vector3.zero);
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}

	void LateUpdate () {
		// only update if the mousebutton is held down
		if (Input.GetMouseButtonDown(0)){
			isActivated = true;
		} 

		// if mouse button is let UP then stop rotating camera
		if (Input.GetMouseButtonUp(0))
		{
			isActivated = false;
		} 

		if (target && isActivated) { 
			//  get the distance the mouse moved in the respective direction
			x += Input.GetAxis("Mouse X") * xSpeed;
			y -= Input.GetAxis("Mouse Y") * ySpeed;	 
			// when mouse moves left and right we actually rotate around local y axis	
			//transform.RotateAround(target.position,transform.up, x);
			transform.RotateAround(Vector3.zero,transform.up, x);
			// when mouse moves up and down we actually rotate around the local x axis	
			//transform.RotateAround(target.position,transform.right, y);
			transform.RotateAround(Vector3.zero,transform.right, y);
			// reset back to 0 so it doesn't continue to rotate while holding the button
			x=0;
			y=0; 	
		} else {		
			// see if mouse wheel is used 	
			if (Input.GetAxis("Mouse ScrollWheel") != 0) 
			{	
				// get the distance between camera and target
				//distance = Vector3.Distance (transform.position , target.position);	
				distance = Vector3.Distance (transform.position , Vector3.zero);	
				// get mouse wheel info to zoom in and out	
				distance = ZoomLimit(distance - Input.GetAxis("Mouse ScrollWheel")*scrollSpeed, zoomMin, zoomMax);
				// position the camera FORWARD the right distance towards target
				//position = -(transform.forward*distance) + target.position;
				position = -(transform.forward*distance) + Vector3.zero;
				// move the camera
				transform.position = position; 
			}
		}
	}

	public static float ZoomLimit(float dist, float min, float max)
	{
		if (dist < min)
			dist= min;
		if (dist > max)
			dist= max; 
		return dist;
	}
}