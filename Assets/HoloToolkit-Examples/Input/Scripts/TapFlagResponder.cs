// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// This class implements IInputClickHandler to handle the tap gesture.
    /// It increases the scale of the object when tapped.
    /// </summary>
    public class TapFlagResponder : MonoBehaviour, IInputClickHandler
    {
        public GameObject FlagObject;
        public static Vector3 hitPos;
        public static Vector3 hitNorm;

        private void Start()
        {
            
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            var cam = Camera.main;

            Vector3 camPos = cam.transform.position;
            hitPos = GazeManager.Instance.HitPosition;
            hitPos.y += 0.05f;
            hitNorm = GazeManager.Instance.HitInfo.normal;
            GameObject flag = Instantiate(FlagObject);
            flag.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitNorm);
            flag.transform.position = camPos;
            flag.transform.parent = this.transform;

            Rigidbody rb;
            rb = flag.GetComponent<Rigidbody>();
            rb.AddForce((camPos - hitPos) * -80);

        }
    }
}