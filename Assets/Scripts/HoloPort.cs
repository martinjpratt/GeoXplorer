//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class HoloPort : MonoBehaviour, IHoldHandler {

    //Code to control teleportation behaviour with an airtap and hold gesture

    public GameObject holoPoint;
    GameObject newHoloPoint;
    Vector3 holoportPosition;
    bool moveStarted = false;
    Vector3 translateObjectVector;
    float startTime;
    float journeyLength;
    Transform[] startTransforms;
    Vector3[] startPositions;

    public void OnHoldCanceled(HoldEventData eventData)
    {
        Destroy(newHoloPoint);
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        translateObjectVector = holoportPosition - cameraPosition;
        
        startTime = Time.time;
        journeyLength = Vector3.Distance(holoportPosition, cameraPosition);
        GameObject parentObject = GameObject.FindGameObjectWithTag("scalable");
        startTransforms = parentObject.GetComponentsInChildren<Transform>();

        Vector3[] startpos = new Vector3[startTransforms.Length];

        for (int i = 0; i < startTransforms.Length; i++)
        {
            if (startTransforms[i].parent.gameObject.tag == "scalable")
            {
                startpos[i] = startTransforms[i].position;
            }
            
        }

        startPositions = startpos;
        

        moveStarted = true;
    }

    public void OnHoldStarted(HoldEventData eventData)
    {
        Vector3 hitPosition = GazeManager.Instance.HitPosition;
        holoportPosition = new Vector3(hitPosition.x, hitPosition.y + 1.5f, hitPosition.z);
        newHoloPoint = Instantiate(holoPoint,hitPosition,Quaternion.identity);
        newHoloPoint.transform.parent = transform;
        newHoloPoint.transform.position = new Vector3(hitPosition.x,hitPosition.y + 0.1f,hitPosition.z);
    }
    	
	// Update is called once per frame
	void Update () {
        if (moveStarted)
        {

            float distCovered = (Time.time - startTime) * 2f;
            float fracJourney = distCovered / journeyLength;


            for (int i = 0; i < startTransforms.Length; i++)
            {
                if (startTransforms[i].parent.gameObject.tag == "scalable")
                {
                    startTransforms[i].position = Vector3.Lerp(startPositions[i], startPositions[i] - translateObjectVector, fracJourney);
                }

            }
            if (fracJourney > 1)
            {

                Destroy(newHoloPoint);
                moveStarted = false;
            }
        }
    }
}
