using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToPlanet : MonoBehaviour {

	public GameObject sun;
	public GameObject sunCorona;

	private Transform targetPlanetTransform;
	Vector3 startPosition;
	Vector3 targetPosition;
	Vector3 targetScale;
	Vector3 startScale;
	Vector3 sunStartPosition;
	float transitSpeed = 0.5f;
	float startTime;
	float journeyLength;
	bool selected = false;
	bool isGoingToSolarSystem = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (selected) {
			float distCovered = (Time.time - startTime) * (transitSpeed * journeyLength);
			float fracJourney = distCovered / journeyLength;
			targetPlanetTransform.position = Vector3.Lerp(startPosition, targetPosition, fracJourney);
			targetPlanetTransform.localScale = Vector3.Lerp(startScale, targetScale, fracJourney);
			if (isGoingToSolarSystem != true) {
				sun.transform.position = targetPlanetTransform.position + (sunStartPosition - startPosition);
			} else {
				sun.transform.position = Vector3.zero;
			}
			Vector3 direction = (targetPlanetTransform.position - sun.transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation (direction);
			sun.transform.rotation = lookRotation;
			if (fracJourney >= 1) {
				selected = false;
			}
		}
	}

	public void zoomToPlanet (string planetName){
		sunCorona.SetActive (false);
		isGoingToSolarSystem = false;
		sun.GetComponent<Renderer> ().enabled = false;
		sun.GetComponentInChildren<Light> ().type = LightType.Directional;
		sunStartPosition = sun.transform.position;


		GameObject[] planets = GameObject.FindGameObjectsWithTag ("planet");
		foreach (var planet in planets) {
			if (planet.name != planetName) {
				Renderer[] rends = planet.GetComponentsInChildren<Renderer>();
				foreach (var rend in rends) {
					rend.enabled = false;
				}
				planet.GetComponent<Collider> ().enabled = false;
				planet.GetComponent<LineRenderer> ().enabled = false;
			} else {
				targetScale = new Vector3 (0.033f, 0.033f, 0.033f);
				targetPlanetTransform = planet.transform;
				startPosition = targetPlanetTransform.position;
				targetPosition = Vector3.zero;
				startScale = targetPlanetTransform.localScale;
				startTime = Time.time;
				journeyLength = Vector3.Distance (startPosition, Vector3.zero);
				selected = true;

				Renderer[] rends = planet.GetComponentsInChildren<Renderer> ();
				foreach (var rend in rends) {
					rend.enabled = true;
				}
				Collider[] cols = planet.GetComponentsInChildren<Collider> ();
				foreach (var col in cols) {
					col.enabled = true;
				}

				LineRenderer[] lrends = planet.GetComponentsInChildren<LineRenderer>();
				lrends [0].enabled = false;
				for (int i = 1; i < lrends.Length; i++) {
					lrends [i].enabled = true;
				}

				if (planet.GetComponent<ArrangeMoons>() != null) {
					planet.GetComponent<ArrangeMoons> ().enabled = true;
				}

			}
		}
	}

	public void zoomToSolarSystem(){
		sunCorona.SetActive (true);
		isGoingToSolarSystem = true;
		sun.GetComponent<Renderer> ().enabled = false;
		sun.GetComponentInChildren<Light> ().type = LightType.Point;
		sunStartPosition = sun.transform.position;

		GameObject[] planets = GameObject.FindGameObjectsWithTag ("planet");

		foreach (var planet in planets) {
			
			if (planet.name != targetPlanetTransform.name) {
				Renderer[] rends = planet.GetComponentsInChildren<Renderer>();
				foreach (var rend in rends) {
					rend.enabled = true;
				}
				planet.GetComponent<Collider> ().enabled = true;

				LineRenderer[] lrends = planet.GetComponentsInChildren<LineRenderer>();
				lrends [0].enabled = true;
				for (int i = 1; i < lrends.Length; i++) {
					lrends [i].enabled = false;
				}

			} else {
				targetPlanetTransform = planet.transform;
				targetPosition = startPosition;
				startPosition = targetPlanetTransform.position; //redifine new start position after setting the target position
				startScale = targetPlanetTransform.localScale;
				targetScale = new Vector3 (0.004f, 0.004f, 0.004f);
				startTime = Time.time;
				journeyLength = Vector3.Distance (targetPosition, Vector3.zero);
				selected = true;

				Renderer[] rends = planet.GetComponentsInChildren<Renderer> ();
				for (int i = 3; i < rends.Length; i++) {
					rends[i].enabled = false;
				}
				Collider[] cols = planet.GetComponentsInChildren<Collider> ();
				for (int i = 1; i < cols.Length; i++) {
					cols[i] .enabled = false;
				}

				LineRenderer[] lrends = planet.GetComponentsInChildren<LineRenderer>();
				lrends [0].enabled = true;
				for (int i = 1; i < lrends.Length; i++) {
					lrends [i].enabled = false;
				}

				if (planet.GetComponent<ArrangeMoons>() != null) {
					planet.GetComponent<ArrangeMoons> ().enabled = false;
				}

			}
		}



	}

}
