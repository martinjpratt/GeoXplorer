// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using HoloToolkit.Sharing.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//using TriLib.Samples;

namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// This class implements IInputClickHandler to handle the tap gesture.
    /// It increases the scale of the object when tapped.
    /// </summary>
    public class TapResponderWithComponent : MonoBehaviour, IInputClickHandler, IFocusable
    {
        public GameObject GoToObject;
        public GameObject TurnOffObject;
        public GameObject modelDownloader;
        public bool isAssetBundle;
        public bool isHirise = false;
        public string assetBundleName;
        public string assetName;
        public string assetTitle;
        public bool sharingSelected = false;

        Material cachedMaterial;
        Color originalColor;

        private void Awake()
        {
            cachedMaterial = GetComponent<Renderer>().material;
            originalColor = cachedMaterial.GetColor("_Color");
        }


        public void OnFocusEnter()
        {
			if (cachedMaterial.color.a == 1) {
				cachedMaterial.SetColor("_Color", Color.red);				
			}

        }

        public void OnFocusExit()
        {
			if (cachedMaterial.color.a == 1) {
				cachedMaterial.SetColor ("_Color", Color.white);
			}
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            GameObject[] flags = GameObject.FindGameObjectsWithTag("flag");
            foreach (GameObject fl in flags) {
                Destroy(fl);
            }

            GameObject titleObject = GameObject.FindGameObjectWithTag("Title");
            titleObject.GetComponent<TextMesh>().text = assetTitle;
            if (assetTitle.Length > 20)
            {
                StringBuilder result = new StringBuilder();
                string fullString ="";
                string[] splitString = assetTitle.Split(" "[0]);
                for (int i = 0; i < splitString.Length; i++)
                {

                    if(fullString.Length + splitString[i].Length > 20)
                    {
                        result.AppendLine(fullString);
                        fullString = splitString[i];
                    }
                    else
                    {
                        fullString = fullString + " " + splitString[i];
                    }
                }

                if (fullString.Length > 0)
                {
                    result.AppendLine(fullString);
                }

                titleObject.GetComponent<TextMesh>().text = result.ToString();
            }


            if (!sharingSelected)
            {
                Renderer[] rends = TurnOffObject.GetComponentsInChildren<Renderer>();
                foreach (var rend in rends)
                {
                    rend.enabled = false;
                }
                Collider[] cols = TurnOffObject.GetComponentsInChildren<Collider>();
                foreach (var col in cols)
                {
                    col.enabled = false;
                }

                Renderer[] goToRends = GoToObject.GetComponentsInChildren<Renderer>();
                foreach (var rend in goToRends)
                {
                    rend.enabled = true;
                }
                Collider[] goToCols = GoToObject.GetComponentsInChildren<Collider>();
                foreach (var col in goToCols)
                {
                    col.enabled = true;
                }
            }
            
            //if (!isAssetBundle)
            //{
            //    modelDownloader.GetComponent<ModelDownloader>().enabled = true;
            //}
            //else
            //{
            if (isHirise)
            {
                modelDownloader.GetComponent<LoadOutcropAssets>().isHirise = true;
            }
            else
            {
                modelDownloader.GetComponent<LoadOutcropAssets>().isHirise = false;
            }


            if (sharingSelected)
            {
                GameObject.FindGameObjectWithTag("syncObjectSpawner").GetComponent<SyncObjectSpawner>().SpawnAssetBundle(assetBundleName, assetName, "http://epsc.wustl.edu/~martinpratt/AssetBundles2017/Windows/");
            }
            else
            {
                StartCoroutine(modelDownloader.GetComponent<LoadOutcropAssets>().DownloadOutcropAsset(assetBundleName, assetName));
            }
            

            GazeAudio.Instance.PlayClickSound();
            cachedMaterial.SetColor("_Color", Color.white);
            

        }
       

        //public void OnInputClicked(InputClickedEventData eventData)
        //{

        //        }
    }
}