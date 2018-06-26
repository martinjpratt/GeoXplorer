//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TestAssetBundleDownloader : MonoBehaviour {

    //Test script to downalod an asset bundle

    public GameObject ParentObj;
    GameObject go;
    public GameObject LoadingText;
    public TextMesh AnchorDebugText;
    UnityWebRequest request;

    protected void OnGUI()
    {
        if (request == null || request.isDone)
        {
            LoadingText.GetComponent<TextMesh>().text = "";
            return;
        }
        
        LoadingText.GetComponent<TextMesh>().text = string.Format("Downloaded {0:P2}", request.downloadProgress);
    }

    public IEnumerator GetBundle(string assetBundleName, string assetName)
    {
        string uri = "http://epsc.wustl.edu/~martinpratt/AssetBundles/Windows/" + assetBundleName;
        request = UnityWebRequest.GetAssetBundle(uri, 0);
        yield return request.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        GameObject OutcropAssetBundle = bundle.LoadAsset<GameObject>(assetName);
        go = Instantiate(OutcropAssetBundle);
        go.transform.parent = ParentObj.transform;
        go.tag = "scalable";

        request.Dispose();
        request = null;
    }
    
}
