using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadNewAssetBundle : MonoBehaviour {

    public const string AssetBundlesOutputPath = "/AssetBundles/";
    public string assetBundleName;
    public string assetName;
    public string locationURL;
    public GameObject ParentObj;
    public GameObject DebugText;
    GameObject loadingText;
    GameObject go;
    AssetBundleRequest downloadedAsset;
    //bool loadingBool = false;
    
    // Use this for initialization
    void Start () {
        StartCoroutine(InstantiateObject());
	}

    IEnumerator InstantiateObject()
    {
        string uri = locationURL + assetBundleName;
        
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri, 0);
        print(request.url);
        yield return request.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        downloadedAsset = bundle.LoadAssetAsync(assetName, typeof(GameObject));
        yield return downloadedAsset;
        GameObject obj = downloadedAsset.asset as GameObject;
        GameObject newObject = Instantiate(obj);
        newObject.transform.parent = ParentObj.transform;
        newObject.transform.localPosition = newObject.transform.position;
        newObject.transform.localEulerAngles = newObject.transform.eulerAngles;
        bundle.Unload(false);
        request.Dispose();
    }
    
}
