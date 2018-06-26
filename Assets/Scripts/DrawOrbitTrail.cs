//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class DrawOrbitTrail : MonoBehaviour {

    //Draw an orbit trail

    public float orbitRadius;

	// Use this for initialization
	void Start () {
        LineRenderer lr = GetComponentInChildren<LineRenderer>();

        float currentPositionAngle = Vector3.Angle(Vector3.zero, this.transform.localPosition);

        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, new Vector3());
        }

	}
}
