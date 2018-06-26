//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class CalculateStrikeAndDip : MonoBehaviour {
    
    //Does what is says, calculates the angle between the two spheres of the strike and dip tool to calculate said values.

    public GameObject ball1;
    public GameObject ball2;
    public GameObject dipText;
    public GameObject strikeText;
    public Material planeMaterial;
    float dist;
    GameObject quad;

    // Use this for initialization
    void Start () {
        dist = Vector3.Distance(ball1.transform.position, ball2.transform.position);
        quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.parent = this.transform;
        quad.GetComponent<MeshRenderer>().material = planeMaterial;
        quad.GetComponent<MeshCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update () {
        
        dist = Vector3.Distance(ball1.transform.position,ball2.transform.position);
        quad.transform.localPosition = ((ball1.transform.localPosition + ball2.transform.localPosition) / 2);
        quad.transform.localScale = new Vector3(dist, dist, dist);
        float theta = Mathf.Atan((ball2.transform.localPosition.x - ball1.transform.localPosition.x) / (ball2.transform.localPosition.z - ball1.transform.localPosition.z)) * Mathf.Rad2Deg;
        float phiLength = Mathf.Sqrt(Mathf.Pow((ball2.transform.localPosition.x - ball1.transform.localPosition.x),2) + Mathf.Pow((ball2.transform.localPosition.z - ball1.transform.localPosition.z),2));
        float phi = Mathf.Atan((ball2.transform.localPosition.y - ball1.transform.localPosition.y) / phiLength) * Mathf.Rad2Deg;

        if (ball2.transform.localPosition.z < 0)
        {
            quad.transform.localEulerAngles = new Vector3(90 + phi, theta, 0);
        } else
        {
            quad.transform.localEulerAngles = new Vector3(90 - phi, theta, 0);
        }

        strikeText.GetComponent<TextMesh>().text = "Strike: " + ((int)theta + 90).ToString();
        dipText.GetComponent<TextMesh>().text = "Dip: " + ((int)phi * -1).ToString();

    }
}
