//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System.Collections;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapEarthquakeResponder : MonoBehaviour, IInputClickHandler, IFocusable
{

    //Code to allow the user to bring up info on an earthquake using an AirTap. The regional earthquake object shifts to bring the tapped earthquake into the center of the scene.

    public GameObject parentObject;
    public GameObject infoObject;
    public GameObject textObject;
    Renderer rend;

    // Use this for initialization
    void Start()
    {
        parentObject = GameObject.FindGameObjectWithTag("table");
    }
    

    private IEnumerator Check()
    {
        WWW w = new WWW("https://earthquake.usgs.gov/earthquakes/eventpage/us20008l41#moment-tensor");
        yield return w;
        Debug.Log(w.text);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnFocusEnter()
    {
        rend = GazeManager.Instance.HitObject.GetComponent<Renderer>();
        rend.material.SetColor("_EmissionColor", Color.blue);
    }

    public void OnFocusExit()
    {
        rend.material.SetColor("_EmissionColor", Color.black);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        infoObject.SetActive(true);


        parentObject.transform.position -= gameObject.transform.position - new Vector3(0, 0, 1);

        //StartCoroutine(Check());

        for (int i = sphericalCoords.dataEventList.Length - 1; i >= 0; i--)
        {
            if (sphericalCoords.dataEventList[i].Contains(gameObject.GetComponent<IDNumber>().IDValue))
            {
                var ndataEventList = sphericalCoords.dataEventList[i].Split("|"[0]);
                var timeData = ndataEventList[1].Split("T"[0]);
                string time = (timeData[0] + " " + timeData[1]);
                string lat = ndataEventList[2];
                string lon = ndataEventList[3];
                string dep = ndataEventList[4];
                string mag = ndataEventList[10];
                string name = ndataEventList[12];
                textObject.GetComponent<TextMesh>().text = (name + "\n" + time + "\n\nLatitude: " + lat + "\nLongitude: " + lon + "\nDepth: " + dep + " km\nMagnitude: " + mag);
                return;
            }
        }
    }
}
