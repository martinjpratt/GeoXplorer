//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System.Collections;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Tests;

public class CreateIcons : MonoBehaviour
{
    //Code to create the icons on the globe for model locations. Requires the presence of a 'metadata.txt' file at a local or remote location

    public string metadata;
    public GameObject iconPrefab;
    public GameObject additionalMenuPrefab;
    public GameObject navTools;
    public GameObject modelStage;
    public GameObject modelDownloader;

    float earthRadius = 1f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {

        //Download location of metadata file
        WWW w = new WWW("http://epsc.wustl.edu/~martinpratt/AssetBundles/" + metadata + ".txt");
        //TextAsset w = metadata;

        yield return w;

        var iconList = w.text.Split("\n"[0]);
        for (int i = 0; i < iconList.Length; i++)
        {
            var dataList = iconList[i].Split("\t"[0]);

            GameObject newIconPrefab = Instantiate(iconPrefab);
            newIconPrefab.transform.SetParent(this.transform);

            newIconPrefab.GetComponentInChildren<TextMesh>().text = dataList[0];

            GameObject bc = newIconPrefab.transform.GetChild(1).gameObject;
            bc.AddComponent<BoxCollider>();
            newIconPrefab.name = (dataList[0] + " Button");
            float lon = float.Parse(dataList[1]);
            float lat = float.Parse(dataList[2]);

            float xpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Cos((lon - 90) * Mathf.Deg2Rad) * -1;
            float zpos = earthRadius * Mathf.Cos(lat * Mathf.Deg2Rad) * Mathf.Sin((lon - 90) * Mathf.Deg2Rad) * -1;
            float ypos = earthRadius * Mathf.Sin(lat * Mathf.Deg2Rad);

            newIconPrefab.transform.localPosition = new Vector3(xpos, ypos, zpos);
            newIconPrefab.transform.localEulerAngles = new Vector3(-(90 - lat), -180, 0);
            newIconPrefab.transform.Rotate(Vector3.up, -lon, Space.World);

            float numberOfAdditionals = float.Parse(dataList[3]);
            if (numberOfAdditionals > 1)
            {
                Destroy(bc.GetComponent<TapResponderWithComponent>());
                bc.AddComponent<MenuDropDown>();
                
                for (int j = 0; j < numberOfAdditionals; j++)
                {
                    GameObject newMenuPrefab = Instantiate(additionalMenuPrefab);
                    bc.GetComponent<MenuDropDown>().iList.Add(newMenuPrefab);
                    newMenuPrefab.transform.SetParent(bc.transform);
                    newMenuPrefab.tag = "menuItem";
                    newMenuPrefab.transform.localPosition = new Vector3(0, 0, 0);
                    newMenuPrefab.transform.localScale = new Vector3(1, 1, 1);
                    newMenuPrefab.name = (dataList[(j * 4) + 4] + "Button");
                    newMenuPrefab.GetComponent<MenuAnimationController>().distanceDown = -14 * (j + 1);
                    newMenuPrefab.GetComponent<TextMesh>().text = dataList[(j*4) + 4];
					newMenuPrefab.AddComponent<BoxCollider>().enabled = false;
					newMenuPrefab.GetComponent<Renderer> ().material.color = new Color (0, 0, 0, 0);
                    newMenuPrefab.GetComponentInChildren<TapResponderWithComponent>().GoToObject = navTools;
                    newMenuPrefab.GetComponentInChildren<TapResponderWithComponent>().TurnOffObject = this.transform.parent.gameObject;
                    newMenuPrefab.GetComponentInChildren<TapResponderWithComponent>().modelDownloader = modelDownloader;
                    newMenuPrefab.GetComponentInChildren<TapResponderWithComponent>().assetTitle = dataList[(j * 4) + 4];
                    if (dataList[(j*4) + 7] == "1")
                    {
                        newMenuPrefab.GetComponentInChildren<TapResponderWithComponent>().isAssetBundle = true;
                        newMenuPrefab.GetComponentInChildren<TapResponderWithComponent>().assetName = dataList[(j*4) + 5];
                        newMenuPrefab.GetComponentInChildren<TapResponderWithComponent>().assetBundleName = dataList[(j*4)+6];
                        
                    }
                    if (this.name == "Mars")
                    {
                        newMenuPrefab.GetComponentInChildren<TapResponderWithComponent>().enabled = false;
                    }
                }
            }
            else
            {
                newIconPrefab.GetComponentInChildren<TapResponderWithComponent>().GoToObject = navTools;
                newIconPrefab.GetComponentInChildren<TapResponderWithComponent>().TurnOffObject = this.transform.parent.gameObject;
                newIconPrefab.GetComponentInChildren<TapResponderWithComponent>().modelDownloader = modelDownloader;
                newIconPrefab.GetComponentInChildren<TapResponderWithComponent>().assetTitle = dataList[0];
                if (dataList[6] == "1")
                {
                    newIconPrefab.GetComponentInChildren<TapResponderWithComponent>().isAssetBundle = true;
                    newIconPrefab.GetComponentInChildren<TapResponderWithComponent>().assetName = dataList[4];
                    newIconPrefab.GetComponentInChildren<TapResponderWithComponent>().assetBundleName = dataList[5];
                }

                if (this.name == "Mars")
                {
                    newIconPrefab.GetComponentInChildren<TapResponderWithComponent>().enabled = false;
                }

            }
            }
    }
}
