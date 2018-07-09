//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class MenuAnimationController : MonoBehaviour {

    //Script to annimate dropdown menus

    public float distanceDown;
    float speed = 100.0F;
    private float startTime;
    private float journeyLength;
	private bool selected = false;
	private bool opened = false;

    // Use this for initialization
    void Start () {
        
        
    }
	
	// Update is called once per frame
	void Update () {
		if (selected == true && opened == false) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.localPosition = Vector3.Lerp (Vector3.zero, new Vector3 (0, distanceDown, 0), fracJourney);
			this.GetComponent<Renderer> ().material.color = Color.Lerp (new Color (0, 0, 0, 0), new Color (1, 1, 1, 1), fracJourney);
			if (fracJourney >= 1) {
				selected = false;
				opened = true;
			}
		} else if (selected == true && opened == true){
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.localPosition = Vector3.Lerp (new Vector3 (0, distanceDown, 0), Vector3.zero, fracJourney);
			this.GetComponent<Renderer> ().material.color = Color.Lerp (new Color (1, 1, 1, 1), new Color (0, 0, 0, 0), fracJourney);
			if (fracJourney >= 1) {
				selected = false;
				opened = false;
			}
		}


    }

	public void dropDownMenu(){
		startTime = Time.time;
		if (!opened) {
			journeyLength = Vector3.Distance (transform.localPosition, new Vector3 (0, distanceDown, 0));
		} else {
			journeyLength = Vector3.Distance (transform.localPosition, Vector3.zero);
		}

		selected = true;
	}

}
