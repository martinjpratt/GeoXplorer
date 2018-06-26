//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class ResetSearchMenu : MonoBehaviour {

    //Reset the earthquake search menu and clear the spheres on the globe

    public GameObject timeSphere1;
    public GameObject timeSphere2;
    public GameObject magSphere1;
    public GameObject magSphere2;
    public GameObject depthSphere1;
    public GameObject depthSphere2;


    public void ResetSearch()
    {
        GameObject[] boxSphere = GameObject.FindGameObjectsWithTag("SelectionBox");
        foreach (var sph in boxSphere)
        {
            Destroy(sph);
            TapLocationSet.bottomRightLat = 0;
            TapLocationSet.bottomRightLon = 0;
            TapLocationSet.topLeftLat = 0;
            TapLocationSet.topLeftLon = 0;
            TapLocationSet.bottomRightSet = false;
            TapLocationSet.topLeftSet = false;
        }

        GameObject earthObj = GameObject.FindGameObjectWithTag("InteriorEarth");
        Destroy(earthObj.GetComponent<LineRenderer>());

        timeSphere1.transform.localPosition = new Vector3(-1, 0, 0);
        timeSphere2.transform.localPosition = new Vector3(1, 0, 0);
        magSphere1.transform.localPosition = new Vector3(-0.5f, 0, 0);
        magSphere2.transform.localPosition = new Vector3(0.5f, 0, 0);
        depthSphere1.transform.localPosition = new Vector3(-0.5f, 0, 0);
        depthSphere2.transform.localPosition = new Vector3(0.5f, 0, 0);
    }
}
