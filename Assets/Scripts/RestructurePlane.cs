using UnityEngine;

public class RestructurePlane : MonoBehaviour {

    public float minLat;
    public float maxLat;
    public float minLon;
    public float maxLon;

    // Use this for initialization
    void Start () {

        float deltaLat = (maxLat - minLat) / 10;
        float deltaLon = (maxLon - minLon) / 10;
        float rad = 0.7f;
        int c = 0;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verts = mesh.vertices;

        for (int m = 0; m < 11; m++)
        {
            for (int n = 0; n < 11; n++)
            {
                Vector3 xyz = new Vector3(minLon + (n * deltaLon), 0, minLat + (m * deltaLat));

                float cosLat = Mathf.Cos(xyz[2] * Mathf.PI / 180f);
                float sinLat = Mathf.Sin(xyz[2] * Mathf.PI / 180f);
                float cosLon = Mathf.Cos(xyz[0] * Mathf.PI / 180f);
                float sinLon = Mathf.Sin(xyz[0] * Mathf.PI / 180f);
                verts[c] = new Vector3(rad * cosLat * cosLon, (rad * sinLat) - 0.5f, (rad * cosLat * sinLon) + 2.898f);

                c++;
            }
        }

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
