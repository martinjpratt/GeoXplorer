//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class changeImage : MonoBehaviour {

    //Changes the image of the regional earthquake reference surface

    Renderer rend;
    Mesh mesh;
    //float r = 10;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        float shiftX = (180f + sphericalCoords.minLon) / 360;    //minLon
        float shiftY = (90f + sphericalCoords.minLat) / 180;       //minLat
        rend.material.mainTextureOffset = new Vector2(shiftX, shiftY);
        float scaleX = sphericalCoords.minmaxLon / 360f;      //maxLon-minLon
        float scaleY = sphericalCoords.minmaxLat / 180f;     //maxLat-minLat
        rend.material.mainTextureScale = new Vector2(scaleX, scaleY);
        
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        //float[] hLon = new float[11];
        
        //float[] hLat = new float[11];
        float deltaLon = sphericalCoords.minmaxLon / 10;
        float deltaLat = sphericalCoords.minmaxLat / 10;

        int c = 0;

        for(int m = 0;m < 11; m++)
        {
            for(int n = 0; n < 11; n++)
            {
                Vector3 xyz = new Vector3(sphericalCoords.minLon + (n * deltaLon), 0, sphericalCoords.minLat + (m * deltaLat));

                float cosLat = Mathf.Cos(xyz[2] * Mathf.PI / 180f);
                float sinLat = Mathf.Sin(xyz[2] * Mathf.PI / 180f);
                float cosLon = Mathf.Cos(xyz[0] * Mathf.PI / 180f);
                float sinLon = Mathf.Sin(xyz[0] * Mathf.PI / 180f);
                var rad = 10 * ((6371 - xyz[1]) / 6371);
                vertices[c] = new Vector3(rad * cosLat * cosLon, rad * sinLat, rad * cosLat * sinLon);
               
                c++;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
