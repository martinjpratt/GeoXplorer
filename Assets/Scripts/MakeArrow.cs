//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class MakeArrow : MonoBehaviour {
    
    //Turns a mesh in an arrowhead

	// Use this for initialization
	void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        vertices[0] = new Vector3(0, 0, 0);
        vertices[3] = new Vector3(-0.5f, 0, 0);
        vertices[2] = new Vector3(0, -0.5f, 0);

        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        gameObject.transform.eulerAngles = new Vector3(90, 0, 45);
    }
}
