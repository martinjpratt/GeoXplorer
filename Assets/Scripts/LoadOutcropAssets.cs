//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//Adapted from the LoadAsset code in the AssetBundle Manager scripts
//
//June 2018

using UnityEngine;
using System.Collections;
using AssetBundles;
using UnityEngine.Networking;

public class LoadOutcropAssets : MonoBehaviour
{
    public const string AssetBundlesOutputPath = "/AssetBundles/";
    public GameObject ParentObj;
    GameObject go;
    public GameObject LoadingText;
    public TextMesh AnchorDebugText;
    public GameObject gameHoloPoint;
    public bool isHirise = false;
    AssetBundleRequest downloadedAsset;
    public GameObject textureButton;

    string uri;

    // Use this for initialization
    public IEnumerator DownloadOutcropAsset(string assetBundleName, string assetName)
    {

        //uri = "http://epsc.wustl.edu/~martinpratt/AssetBundles2017/Windows/";
        uri = "https://fossett.blob.core.windows.net/outcrops/";
        if (isHirise)
        {
            uri = "https://fossett.blob.core.windows.net/fossett-lab/";
        }
        
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
        /*
        if (newObject.GetComponent<MeshCollider>() == null)
        {
            newObject.AddComponent<MeshCollider>();
            if (isHirise)
            {
                newObject.GetComponent<MeshCollider>().sharedMesh = newObject.GetComponent<MeshFilter>().mesh;
            }
        }
        else
        {
            newObject.GetComponent<MeshCollider>().sharedMesh = newObject.GetComponent<MeshFilter>().mesh;
        }
        */


        newObject.transform.parent = ParentObj.transform;
        //newObject.transform.localPosition = new Vector3(0, 0, 0);
        bundle.Unload(false);
        request.Dispose();

        newObject.transform.localPosition = newObject.transform.position;
        newObject.transform.localEulerAngles = newObject.transform.eulerAngles;
        newObject.tag = "scalable";
        Collider[] goChildren = newObject.GetComponentsInChildren<MeshCollider>();
        foreach (var gos in goChildren)
        {
            gos.gameObject.AddComponent<HoloPort>().holoPoint = gameHoloPoint;
        }
        

        // Calculate and display the elapsed time.
        float elapsedTime = Time.realtimeSinceStartup - startTime;
        LoadingText.SetActive(false);

        if (isHirise)
        {
            textureButton.SetActive(true);
            newObject.AddComponent<UVMapper>();
            newObject.GetComponent<UVMapper>().meshName = assetName;
            newObject.GetComponent<UVMapper>().surfaceType = "sb";
            newObject.GetComponent<UVMapper>().FetchTexture();
        }
        else
        {
            textureButton.SetActive(false);
        }


        Debug.Log(assetName + " was" + " loaded successfully in " + elapsedTime + " seconds");
    }
}
