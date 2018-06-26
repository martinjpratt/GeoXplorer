//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class HandRotate : MonoBehaviour, IManipulationHandler, IFocusable
{

    //Simplified rotation tool for objects on airtap and drag

    GameObject ngo;
    public Font newFont;
    public Material fontMaterial;

    [Tooltip("Speed of interactive rotation via navigation gestures.")]
    [SerializeField]
    float RotationFactor = 50f;

    [SerializeField]
    bool rotatingEnabled = true;
    public void SetRotating(bool enabled)
    {
        rotatingEnabled = enabled;
    }

    [SerializeField]
    bool freeRotate = true;
    public void SetFreeRotate(bool enabled)
    {
        freeRotate = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (rotatingEnabled)
            
        {
            //THIS WORKS

            float horizMotion = Mathf.Sqrt((eventData.CumulativeDelta.x * eventData.CumulativeDelta.x) + (eventData.CumulativeDelta.z* eventData.CumulativeDelta.z));

            float rotX = horizMotion * RotationFactor;
            float rotY = eventData.CumulativeDelta.y * RotationFactor;

            var cam = Camera.main;
            var toContent = (transform.position - cam.transform.position).normalized;
            var right = Vector3.Cross(Vector3.up, toContent).normalized;

            if (right.x >= 0)
            {
                if (eventData.CumulativeDelta.x >= 0 && eventData.CumulativeDelta.z >= 0)
                {
                    transform.Rotate(Vector3.up, -rotX, Space.World);   //Rotation around the y-axis of the scene
                }
                if (eventData.CumulativeDelta.x < 0 && eventData.CumulativeDelta.z >= 0)
                {
                    transform.Rotate(Vector3.up, rotX, Space.World);
                }
                if (eventData.CumulativeDelta.x >= 0 && eventData.CumulativeDelta.z < 0)
                {
                    transform.Rotate(Vector3.up, -rotX, Space.World);
                }
                if (eventData.CumulativeDelta.x < 0 && eventData.CumulativeDelta.z < 0)
                {
                    transform.Rotate(Vector3.up, rotX, Space.World);
                }
            }else
            {
                if (eventData.CumulativeDelta.x >= 0 && eventData.CumulativeDelta.z >= 0)
                {
                    transform.Rotate(Vector3.up, rotX, Space.World);
                }
                if (eventData.CumulativeDelta.x < 0 && eventData.CumulativeDelta.z >= 0)
                {
                    transform.Rotate(Vector3.up, -rotX, Space.World);
                }
                if (eventData.CumulativeDelta.x >= 0 && eventData.CumulativeDelta.z < 0)
                {
                    transform.Rotate(Vector3.up, rotX, Space.World);
                }
                if (eventData.CumulativeDelta.x < 0 && eventData.CumulativeDelta.z < 0)
                {
                    transform.Rotate(Vector3.up, -rotX, Space.World);
                }
            }

                if (freeRotate)
            {
                transform.Rotate(right, rotY, Space.World);        //Rotation around the horzontal right-axis  relative to the camera
            }
            //
        }
    }

	public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }



    public void OnFocusEnter()
    {
        ngo = new GameObject("myTextGO");
        GameObject parentObject = GameObject.FindGameObjectWithTag("Finish");
        ngo.transform.SetParent(parentObject.transform);
        ngo.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        ngo.transform.localPosition = new Vector3(0, 0.85f, 0);
        TextMesh myText = ngo.AddComponent<TextMesh>();
        myText.text = "Tap and drag to rotate";
        myText.alignment = TextAlignment.Left;
        myText.anchor = TextAnchor.MiddleCenter;
        myText.font = newFont;
        myText.fontSize = 100;
        myText.color = Color.blue;
        ngo.GetComponent<Renderer>().material = fontMaterial;
        ngo.AddComponent<Billboard>();
        ngo.GetComponent<Billboard>().PivotAxis = PivotAxis.Y;
    }

    public void OnFocusExit()
    {
        Destroy(ngo);
    }
    
}
