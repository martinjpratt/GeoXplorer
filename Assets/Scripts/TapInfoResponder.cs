//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//October 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Text;

public class TapInfoResponder : MonoBehaviour, IInputClickHandler, IHoldHandler
{

    public WorldCoordinate northeast;
    public WorldCoordinate southwest;
    public GameObject infoMarker;
    GameObject marker;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject oldMarker = GameObject.FindGameObjectWithTag("infoMarker");
        Destroy(oldMarker);
        


        //print(GazeManager.Instance.HitObject.name);
        Vector3 localHit = this.transform.InverseTransformPoint(GazeManager.Instance.HitPosition);
        marker = GameObject.Instantiate(infoMarker);
        marker.transform.position = GazeManager.Instance.HitPosition;
        marker.transform.localScale = new Vector3(1f, 1f, 1f);
        marker.transform.parent = this.transform;

        float deltaLat = (northeast.Lat - southwest.Lat) / 10;
        float deltaLon = (northeast.Lon - southwest.Lon) / 10;

        float hitLat = (northeast.Lat + ((localHit.z + 5) * -deltaLat));
        float hitLon = (northeast.Lon + ((localHit.x + 5) * -deltaLon));

        StartCoroutine(FetchUnitInfo(hitLat, hitLon));
    }


    IEnumerator FetchUnitInfo(float lat, float lon)
    {
        //string url = "https://macrostrat.org/api/v2/geologic_units/gmus?lat=" + lat.ToString() + "&lng=" + lon.ToString() + "&format=json";
        string url = "https://macrostrat.org/api/v2/mobile/point?lat=" + lat.ToString() + "&lng=" + lon.ToString();
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            ProcessJson(www.text);
        }
    }


    public void ProcessJson(string jsonString)
    {
        var myObject = JsonUtility.FromJson<FirstLevel>(jsonString);

        StringBuilder result = new StringBuilder();
        string descriptionText = myObject.success.data.desc;
        if (descriptionText.Length > 80)
        {
            
            string fullString = "";
            string[] splitString = descriptionText.Split(" "[0]);
            for (int i = 0; i < splitString.Length; i++)
            {
                if (fullString.Length + splitString[i].Length > 80)
                {
                    result.AppendLine(fullString);
                    fullString = splitString[i];
                }else
                {
                    fullString = fullString + " " + splitString[i];
                }
            }

            if (fullString.Length > 0)
            {
                result.AppendLine(fullString);
            }
        }


        string info = string.Format("{0}\n{1}", myObject.success.data.name, result.ToString());
        marker.GetComponentInChildren<TextMesh>().text = info;
    }

    public void OnHoldStarted(HoldEventData eventData)
    {
        
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {

        

        GameObject[] flagMarker = GameObject.FindGameObjectsWithTag("flag");
        if (flagMarker != null)
        {
            foreach (var item in flagMarker)
            {
                Destroy(item);
            }
            
        }

        Vector3 localHit = this.transform.InverseTransformPoint(GazeManager.Instance.HitPosition);
        marker = GameObject.Instantiate(infoMarker);
        marker.transform.position = GazeManager.Instance.HitPosition;
        marker.transform.localScale = new Vector3(1f, 1f, 1f);
        marker.transform.parent = this.transform;

        float deltaLat = (northeast.Lat - southwest.Lat) / 10;
        float deltaLon = (northeast.Lon - southwest.Lon) / 10;

        float hitLat = (northeast.Lat + ((localHit.z + 5) * -deltaLat));
        float hitLon = (northeast.Lon + ((localHit.x + 5) * -deltaLon));

        GameObject.FindGameObjectWithTag("GameController").GetComponent<MapBuilder>().Latitude = hitLat;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<MapBuilder>().Longitude = hitLon;

        GameObject.FindGameObjectWithTag("GameController").GetComponent<MapBuilder>().ShowMap();

        GameObject[] oldMarker = GameObject.FindGameObjectsWithTag("infoMarker");
        if (oldMarker != null)
        {
            foreach (var item in oldMarker)
            {
                Destroy(item);
            }

        }


    }

    public void OnHoldCanceled(HoldEventData eventData)
    {
        
    }

    [System.Serializable]
    public class ThirdLevel
    {
        public string uid;
		public string rocktype;
        public string age;
        public string name;
        public string desc;
        public string comm;
        public string strat_unit;
		//"map_ref":{
		//	"url":"https://pubs.usgs.gov/of/2007/1010/",
		//	"name":"Lake Mead",
		//	"authors":"Beard, L.S., R.E. Anderson, D.L. Block, R.G. Bohannon, R.J. Brady, S.B. Castor, E.M. Duebendorfer, J.E. Faulds, T.J. Felger, K.A. Howard, M.A. Kuntz, and V.S. Williams",
		//	"isbn_doi":"",
		//	"ref_year":"2007",
		//	"ref_title":"Preliminary Geologic Map of the Lake Mead 30' X 60' Quadrangle, Clark County, Nevada, and Mohave County, Arizona",
		//	"source_id":151,"ref_source":"U.S. Geological Survey \nOpen-File Report 2007-1010\nversion 1.0"
		//	},
		public string col_id;
    }

    [System.Serializable]
    public class SecondLevel
    {
        public ThirdLevel data;
    }

    [System.Serializable]
    public class FirstLevel
    {
        public SecondLevel success;
    }
}



