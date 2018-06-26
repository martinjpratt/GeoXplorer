//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//Adapted from the LoadAsset code in the AssetBundle Manager scripts
//
//June 2018

using UnityEngine;
using System.Collections;
using AssetBundles;

public class LoadOutcropAssets : MonoBehaviour
{
    public const string AssetBundlesOutputPath = "/AssetBundles/";
    public GameObject ParentObj;
    GameObject go;
    public GameObject LoadingText;
    public TextMesh AnchorDebugText;
    public GameObject gameHoloPoint;

    // Use this for initialization
    public IEnumerator DownloadOutcropAsset(string assetBundleName, string assetName)
    {
        yield return StartCoroutine(Initialize());

        // Load asset.
        yield return StartCoroutine(InstantiateGameObjectAsync(assetBundleName, assetName));
    }

    // Initialize the downloading URL.
    // eg. Development server / iOS ODR / web URL
    void InitializeSourceURL()
    {
        //AssetBundleManager.SetSourceAssetBundleURL("http://epsc.wustl.edu/~martinpratt/AssetBundles2017");
        //return;

        // If ODR is available and enabled, then use it and let Xcode handle download requests.
#if ENABLE_IOS_ON_DEMAND_RESOURCES
        if (UnityEngine.iOS.OnDemandResources.enabled)
        {
            AssetBundleManager.SetSourceAssetBundleURL("odr://");
            return;
        }
#endif
//#if DEVELOPMENT_BUILD || UNITY_EDITOR
        // With this code, when in-editor or using a development builds: Always use the AssetBundle Server
        // (This is very dependent on the production workflow of the project.
        //      Another approach would be to make this configurable in the standalone player.)
//        AssetBundleManager.SetDevelopmentAssetBundleServer();
//        return;
//#else
        // Use the following code if AssetBundles are embedded in the project for example via StreamingAssets folder etc:
        //AssetBundleManager.SetSourceAssetBundleURL(Application.dataPath + "/");
        // Or customize the URL based on your deployment or configuration
        AssetBundleManager.SetSourceAssetBundleURL("http://epsc.wustl.edu/~martinpratt/AssetBundles2017/");
        return;
//#endif
    }
    


    // Initialize the downloading url and AssetBundleManifest object.
    protected IEnumerator Initialize()
    {
        // Don't destroy this gameObject as we depend on it to run the loading script.
        DontDestroyOnLoad(gameObject);

        InitializeSourceURL();

        // Initialize AssetBundleManifest which loads the AssetBundleManifest object.
        var request = AssetBundleManager.Initialize();
        if (request != null)
            yield return StartCoroutine(request);
    }

    protected IEnumerator InstantiateGameObjectAsync(string assetBundleName, string assetName)
    {
        // This is simply to get the elapsed time for this phase of AssetLoading.
        float startTime = Time.realtimeSinceStartup;

        LoadingText.SetActive(true);
        // Load asset from assetBundle.
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);

        // Get the asset.
        GameObject prefab = request.GetAsset<GameObject>();

        if (prefab != null)
            go = GameObject.Instantiate(prefab);

        go.transform.parent = ParentObj.transform;

        go.transform.localPosition = go.transform.position;
        go.transform.localEulerAngles = go.transform.eulerAngles;
        go.tag = "scalable";
        Collider[] goChildren = go.GetComponentsInChildren<MeshCollider>();
        foreach (var gos in goChildren)
        {
            gos.gameObject.AddComponent<HoloPort>().holoPoint = gameHoloPoint;
        }

        // Calculate and display the elapsed time.
        float elapsedTime = Time.realtimeSinceStartup - startTime;
        LoadingText.SetActive(false);
        Debug.Log(assetName + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime + " seconds");
    }
}
