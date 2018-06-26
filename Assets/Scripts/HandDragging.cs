//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class HandDragging : MonoBehaviour, IManipulationHandler
{
    //A more basic code to allow for hand airtap and drag... used for menu sliders mainly

    [SerializeField]
    float DragSpeed = 1.5f;

    [SerializeField]
    float DragScale = 1.5f;

    [SerializeField]
    float MaxDragDistance = 3f;
        
    Vector3 lastPosition;

    [SerializeField]
    bool draggingEnabled = true;
    
    [SerializeField]
    bool lockZYEnabled = true;

    [SerializeField]
    float minXValue;

    [SerializeField]
    float maxXValue;


    public void SetDragging(bool enabled)
    {
        draggingEnabled = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(gameObject);
        lastPosition = transform.position;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (draggingEnabled)
        {         
            Drag(eventData.CumulativeDelta);
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

    void Drag(Vector3 positon)
    {
        var targetPosition = lastPosition + positon * DragScale;
        if (Vector3.Distance(lastPosition, targetPosition) <= MaxDragDistance)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, DragSpeed);

            if (lockZYEnabled)
            {
                 
                if (transform.localPosition.x < minXValue)
                {
                    transform.localPosition = new Vector3(minXValue, 0, 0);
                } if (transform.localPosition.x > maxXValue)
                {
                    transform.localPosition = new Vector3(maxXValue, 0, 0);
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
                }
                
            }
        }
    }
}
