//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class HitGround : MonoBehaviour {

    //Script to remove a rigid body component from an object

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(GetComponent<Rigidbody>());
        
    }
}
