//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//October 2018


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SearchMetadata : MonoBehaviour {
    
    public GameObject OutcropIconPrefab;
    public string planet;

    float earthRadius = 1f;


    void Start()
    {
        StartCoroutine(ReadJSON());
    }

    private IEnumerator ReadJSON()
    {
        WWW rawJSON = new WWW("https://salty-oasis-92702.herokuapp.com/fetchAllModels");
        yield return rawJSON;
        
        List<GeoXObject> outcrops = JsonConvert.DeserializeObject<List<GeoXObject>>(rawJSON.text);
        foreach (GeoXObject blob in outcrops)
        {
            string blobName = blob.name;
            if (blobName.Contains("manifest"))
            {
                if (blob.metadata.restricted != "true")
                {
                    if (blob.metadata.latitude != null && blob.metadata.longitude != null)
                    {

                        if (blob.metadata.planetarybody == null && this.gameObject.name == "Earth")
                        {

                            float lon = float.Parse(blob.metadata.longitude);
                            float lat = float.Parse(blob.metadata.latitude);

                            float xpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Cos((lon - 90) * Mathf.Deg2Rad) * -1;
                            float zpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Sin((lon - 90) * Mathf.Deg2Rad) * -1;
                            float ypos = earthRadius * Mathf.Sin(lat * Mathf.Deg2Rad);

                            GameObject newOutcropLocation = Instantiate(OutcropIconPrefab, this.transform);
                            newOutcropLocation.transform.localPosition = new Vector3(xpos, ypos, zpos);

                            string modelName = blob.metadata.modelname;
                            string prefabName = blob.metadata.prefabname;

                            newOutcropLocation.GetComponent<OutcropNameID>().Latitude = lat;
                            newOutcropLocation.GetComponent<OutcropNameID>().Longitude = lon;
                            newOutcropLocation.GetComponent<OutcropNameID>().bundleString = blobName.Replace(".manifest", "");
                            newOutcropLocation.GetComponent<OutcropNameID>().prefabName = prefabName;
                            newOutcropLocation.GetComponent<OutcropNameID>().modelName = modelName;
                            newOutcropLocation.GetComponent<OutcropNameID>().authorName = blob.metadata.author;
                        }

                        if (blob.metadata.planetarybody == planet)
                        {
                            float lon = float.Parse(blob.metadata.longitude);
                            float lat = float.Parse(blob.metadata.latitude);

                            float xpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Cos((lon - 90) * Mathf.Deg2Rad) * -1;
                            float zpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Sin((lon - 90) * Mathf.Deg2Rad) * -1;
                            float ypos = earthRadius * Mathf.Sin(lat * Mathf.Deg2Rad);

                            GameObject newOutcropLocation = Instantiate(OutcropIconPrefab, this.transform);
                            newOutcropLocation.transform.localPosition = new Vector3(xpos, ypos, zpos);

                            string modelName = blob.metadata.modelname;
                            string prefabName = blob.metadata.prefabname;

                            newOutcropLocation.GetComponent<OutcropNameID>().Latitude = lat;
                            newOutcropLocation.GetComponent<OutcropNameID>().Longitude = lon;
                            newOutcropLocation.GetComponent<OutcropNameID>().bundleString = blobName.Replace(".manifest", "");
                            newOutcropLocation.GetComponent<OutcropNameID>().prefabName = prefabName;
                            newOutcropLocation.GetComponent<OutcropNameID>().modelName = modelName;
                            newOutcropLocation.GetComponent<OutcropNameID>().authorName = blob.metadata.author;
                        }

                    }
                }
            }
        }

    }
    
}


public class GeoXObject
{
    public string name;
    public string url;
    public outcropMetadata metadata;
}


public class outcropMetadata
{
    public string author;
    public string datetaken;
    public string description;
    public string latitude;
    public string longitude;
    public string modelname;
    public string organization;
    public string prefabname;
    public string region;
    public string planetarybody;
    public string restricted;
}