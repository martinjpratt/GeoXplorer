//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class DepthSliderManager : MonoBehaviour {
    //Controls the text shown on the depth slider

    float minDepth = 0f;
    float maxDepth = 800f;
    public GameObject startSphere;
    public GameObject endSphere;
    public GameObject selectionCylinder;
    public GameObject startDepthText;
    public GameObject endDepthText;
    float depthDelta;
    
    // Use this for initialization
    void Start () {
        float magSpan = maxDepth - minDepth;
        depthDelta = 1f / magSpan;
        startDepthText.GetComponent<TextMesh>().text = minDepth.ToString();
        endDepthText.GetComponent<TextMesh>().text = maxDepth.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        float startPos = startSphere.transform.localPosition.x;
        float endPos = endSphere.transform.localPosition.x;
        float halfWay = (endPos + startPos) / 2f;
        float halfScale = (endPos - startPos) / 2f;
        selectionCylinder.transform.localPosition = new Vector3(halfWay, 0, 0);
        selectionCylinder.transform.localScale = new Vector3(selectionCylinder.transform.localScale.x, halfScale, selectionCylinder.transform.localScale.z);

        float newStartPos = startSphere.transform.localPosition.x + 0.5f;
        float startDepths = newStartPos / depthDelta;
        float newMinDepth = minDepth + startDepths;
        startDepthText.GetComponent<TextMesh>().text = ((int)newMinDepth).ToString();

        float newEndPos = endSphere.transform.localPosition.x - 0.5f;
        float endDepths = newEndPos / depthDelta;
        float newMaxDepth = maxDepth + endDepths;
        endDepthText.GetComponent<TextMesh>().text = ((int)newMaxDepth).ToString();
    }
}
