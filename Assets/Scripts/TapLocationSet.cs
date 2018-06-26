//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TapLocationSet : MonoBehaviour, IInputClickHandler {

    public Material boundingBoxMaterial;

    public static bool topLeftSet = false;
    public static bool bottomRightSet = false;
    public static float topLeftLon;
    public static float topLeftLat;
    public static float bottomRightLon;
    public static float bottomRightLat;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        
        if (topLeftSet == true)
        {
            Vector3 bottomRightHit = GazeManager.Instance.HitPosition;
            GameObject bottomRight = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bottomRight.tag = "SelectionBox";
            bottomRight.transform.position = bottomRightHit;
            bottomRight.transform.parent = this.transform;
            bottomRight.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

            float tbottomRightLon = -Mathf.Atan2(bottomRight.transform.localPosition.x, bottomRight.transform.localPosition.z) * Mathf.Rad2Deg;
            float tbottomRightLat = (Mathf.Acos(bottomRight.transform.localPosition.y) - (Mathf.PI / 2)) * -Mathf.Rad2Deg;

            if (tbottomRightLon > topLeftLon && tbottomRightLat < topLeftLat)
            {
                bottomRightLon = -Mathf.Atan2(bottomRight.transform.localPosition.x, bottomRight.transform.localPosition.z) * Mathf.Rad2Deg;
                bottomRightLat = (Mathf.Acos(bottomRight.transform.localPosition.y) - (Mathf.PI / 2)) * -Mathf.Rad2Deg;

                bottomRightSet = true;
            }

            else
            {
                Destroy(bottomRight);
            }
            
        }
        
        if (topLeftSet !=true)
        {
            Vector3 topLeftHit = GazeManager.Instance.HitPosition;
            GameObject topLeft = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            topLeft.tag = "SelectionBox";
            topLeft.transform.position = topLeftHit;
            topLeft.transform.parent = this.transform;
            topLeft.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

            topLeftLon = -Mathf.Atan2(topLeft.transform.localPosition.x,topLeft.transform.localPosition.z) * Mathf.Rad2Deg;
            topLeftLat = (Mathf.Acos(topLeft.transform.localPosition.y) - (Mathf.PI/2)) * -Mathf.Rad2Deg;

            topLeftSet = true;
        }

        
        if (topLeftSet && bottomRightSet)
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = boundingBoxMaterial;
            lineRenderer.positionCount = 40;
            lineRenderer.widthMultiplier = 0.005f;
            lineRenderer.material.renderQueue = 5000;
            lineRenderer.numCornerVertices = 3;
            lineRenderer.startColor = Color.cyan;
            lineRenderer.endColor = Color.cyan;
            lineRenderer.useWorldSpace = false;

            float minLon = topLeftLon - 90f;
            float minLat = bottomRightLat;
            float maxLon = bottomRightLon - 90f;
            float maxLat = topLeftLat;
            

            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                if (i < 10)
                {
                    float lonPos = Mathf.Lerp(minLon, maxLon, (float)i / 9);
                    float xpos = (1 - ((0 / 6371) * 1)) * Mathf.Cos(maxLat * Mathf.Deg2Rad) * Mathf.Cos(lonPos * Mathf.Deg2Rad) * -1;
                    float zpos = (1 - ((0 / 6371) * 1)) * Mathf.Cos(maxLat * Mathf.Deg2Rad) * Mathf.Sin(lonPos * Mathf.Deg2Rad) * -1;
                    float ypos = (1 - ((0 / 6371) * 1)) * Mathf.Sin(maxLat * Mathf.Deg2Rad);
                    lineRenderer.SetPosition(i, new Vector3(xpos, ypos, zpos));
                }
                if (i > 9 && i < 20)
                {
                    float latPos = Mathf.Lerp(maxLat, minLat, (float)(i - 10) / 9);
                    float xpos = (1 - ((0 / 6371) * 1)) * Mathf.Cos(latPos * Mathf.Deg2Rad) * Mathf.Cos(maxLon * Mathf.Deg2Rad) * -1;
                    float zpos = (1 - ((0 / 6371) * 1)) * Mathf.Cos(latPos * Mathf.Deg2Rad) * Mathf.Sin(maxLon * Mathf.Deg2Rad) * -1;
                    float ypos = (1 - ((0 / 6371) * 1)) * Mathf.Sin(latPos * Mathf.Deg2Rad);
                    lineRenderer.SetPosition(i, new Vector3(xpos, ypos, zpos));
                }
                if (i > 19 && i < 30)
                {
                    float lonPos = Mathf.Lerp(maxLon, minLon, (float)(i - 20) / 9);
                    float xpos = (1 - ((0 / 6371) * 1)) * Mathf.Cos(minLat * Mathf.Deg2Rad) * Mathf.Cos(lonPos * Mathf.Deg2Rad) * -1;
                    float zpos = (1 - ((0 / 6371) * 1)) * Mathf.Cos(minLat * Mathf.Deg2Rad) * Mathf.Sin(lonPos * Mathf.Deg2Rad) * -1;
                    float ypos = (1 - ((0 / 6371) * 1)) * Mathf.Sin(minLat * Mathf.Deg2Rad);
                    lineRenderer.SetPosition(i, new Vector3(xpos, ypos, zpos));
                }
                if (i > 29 && i < 40)
                {
                    float latPos = Mathf.Lerp(minLat, maxLat, (float)(i - 30) / 9);
                    float xpos = (1 - ((0 / 6371) * 1)) * Mathf.Cos(latPos * Mathf.Deg2Rad) * Mathf.Cos(minLon * Mathf.Deg2Rad) * -1;
                    float zpos = (1 - ((0 / 6371) * 1)) * Mathf.Cos(latPos * Mathf.Deg2Rad) * Mathf.Sin(minLon * Mathf.Deg2Rad) * -1;
                    float ypos = (1 - ((0 / 6371) * 1)) * Mathf.Sin(latPos * Mathf.Deg2Rad);
                    lineRenderer.SetPosition(i, new Vector3(xpos, ypos, zpos));
                }
            }
        }
    }
}
