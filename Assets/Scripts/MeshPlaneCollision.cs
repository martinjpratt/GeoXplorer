//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;

public class MeshPlaneCollision : MonoBehaviour {
    //Code to detect if rays have hit a mesh collider

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
}
