//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//
//June 2018

using UnityEngine;
using UnityEngine.SceneManagement;

public class GeoSceneSwitcher : MonoBehaviour {
    
    //First attempt at a sceneswitcher, not used as causes scenes to crash

    public string sceneName;

    public void switchScenes()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
