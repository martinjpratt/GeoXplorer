//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class CalculateDistance : MonoBehaviour {

    //Code to calcilate the distance (direct, horizontal and vertical) between two points on a model

    public GameObject ball1;
    public GameObject ball2;
    public GameObject directText;
    public GameObject horizontalText;
    public GameObject verticalText;
    float dist;
    LineRenderer line;
    LineRenderer line2;
    public Material measuringTape;
    public GameObject scaleBar;

    // Use this for initialization
    void Start () {

        //Add a line renderer that will be defined by 4 points to form a triangle
        line = gameObject.AddComponent<LineRenderer>();
        line.material = measuringTape;
        line.widthMultiplier = 0.01f;
        line.positionCount = 4;
        line.SetPosition(0, ball1.transform.position);
        line.SetPosition(1, ball2.transform.position);
        line.SetPosition(2, new Vector3(ball1.transform.localPosition.x, ball2.transform.position.y, ball1.transform.position.z));
        line.SetPosition(3, ball1.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        dist = Vector3.Distance(ball1.transform.position, ball2.transform.position) * float.Parse(scaleBar.GetComponent<TextMesh>().text);
        float yDist = Mathf.Abs(ball2.transform.position.y - ball1.transform.position.y) * float.Parse(scaleBar.GetComponent<TextMesh>().text);
        float xDist = Mathf.Abs(Vector2.Distance(new Vector2(ball2.transform.position.x, ball2.transform.position.z), new Vector2(ball1.transform.position.x, ball1.transform.position.z))) * float.Parse(scaleBar.GetComponent<TextMesh>().text);

        line.SetPosition(0, ball1.transform.position);
        line.SetPosition(1, ball2.transform.position);
        line.SetPosition(2, new Vector3(ball1.transform.position.x, ball2.transform.position.y, ball1.transform.position.z));
        line.SetPosition(3, ball1.transform.position);

        directText.GetComponent<TextMesh>().text = "Direct: " + dist.ToString("#.00");
        horizontalText.GetComponent<TextMesh>().text = "Horizontal: " + xDist.ToString("#.00");
        verticalText.GetComponent<TextMesh>().text = "Vertical: " + yDist.ToString("#.00");
    }
}
