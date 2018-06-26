//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class FetchRegionalEQ : MonoBehaviour {

    //Script to make the regional earthquake object appear, and also to be cleared

    public GameObject seismicityObject;

    public void FetchEQs()
    {
        if (seismicityObject.activeInHierarchy == false)
        {
            seismicityObject.SetActive(true);
        }
    }

    public void ClearEQs()
    {
        GameObject infoObj = GameObject.FindGameObjectWithTag("infoObject");
        if (infoObj != null)
        {
            infoObj.SetActive(false);
        }


        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in gos1)
        {
            Destroy(go);
        }

        seismicityObject.transform.position = new Vector3(0f, 0f, 0f);
        seismicityObject.transform.Rotate(Vector3.left, 90 - sphericalCoords.meanLat, Space.World);
        seismicityObject.transform.Rotate(Vector3.down, 90 + sphericalCoords.meanLon, Space.World);

        seismicityObject.SetActive(false);
    }
}
