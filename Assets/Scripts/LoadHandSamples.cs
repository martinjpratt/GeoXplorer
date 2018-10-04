using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;

public class LoadHandSamples : MonoBehaviour {

    string uri;
    public GameObject LoadingText;
    AssetBundleRequest downloadedAsset;
    public GameObject ParentObj;

    // Use this for initialization
    public IEnumerator DownloadHandSampleAsset(string assetBundleName, string assetName)
    {

        uri = "https://fossett.blob.core.windows.net/hand-samples/";


        // Load asset.
        yield return StartCoroutine(InstantiateGameObjectAsync(assetBundleName, assetName));
    }


    protected IEnumerator InstantiateGameObjectAsync(string assetBundleName, string assetName)
    {
        // This is simply to get the elapsed time for this phase of AssetLoading.
        float startTime = Time.realtimeSinceStartup;

        LoadingText.SetActive(true);

        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri + assetBundleName, 0);
        print(request.url);
        yield return request.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        downloadedAsset = bundle.LoadAssetAsync(assetName, typeof(GameObject));
        yield return downloadedAsset;
        GameObject obj = downloadedAsset.asset as GameObject;
        GameObject newObject = Instantiate(obj);
        
        newObject.transform.parent = ParentObj.transform;
        newObject.transform.localPosition = new Vector3(0, 0, 0);
        bundle.Unload(false);
        request.Dispose();

        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localEulerAngles = newObject.transform.eulerAngles;
        newObject.AddComponent<TwoHandManipulatable>();
        newObject.name = assetName;

        // Calculate and display the elapsed time.
        float elapsedTime = Time.realtimeSinceStartup - startTime;
        LoadingText.SetActive(false);


        Debug.Log(assetName + " was" + " loaded successfully in " + elapsedTime + " seconds");
    }
    
}
