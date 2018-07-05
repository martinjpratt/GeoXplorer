using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToPlanet : MonoBehaviour {

	public GameObject sun;

	private Transform targetPlanetTransform;
	Vector3 startPosition;
	Vector3 targetScale;
	Vector3 startScale;
	float transitSpeed = 0.1f;
	float startTime;
	float journeyLength;
	bool selected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (selected) {
			float distCovered = (Time.time - startTime) * transitSpeed;
			float fracJourney = distCovered / journeyLength;
			targetPlanetTransform.position = Vector3.Lerp(startPosition, Vector3.zero, fracJourney);
			targetPlanetTransform.localScale = Vector3.Lerp(startScale, targetScale, fracJourney);
			sun.transform.position = targetPlanetTransform.position - startPosition;
			Vector3 direction = (targetPlanetTransform.position - sun.transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation (direction);
			sun.transform.rotation = lookRotation;
			if (fracJourney >= 1) {
				selected = false;
			}
		}
	}

	public void zoomToEarth (){
		sun.GetComponent<Renderer> ().enabled = false;
		sun.GetComponentInChildren<Light> ().type = LightType.Directional;


		GameObject[] planets = GameObject.FindGameObjectsWithTag ("planet");
		foreach (var planet in planets) {
			if (planet.name != "Earth") {
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
}
