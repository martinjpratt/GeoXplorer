using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Tests;
using HoloToolkit.Examples.InteractiveElements;

public class HiRiseIconInteraction : MonoBehaviour, IInputClickHandler, IFocusable {

	public GameObject mainCursor;
	public GameObject iconParent;
	public GameObject iconPrefab;

    public GameObject hiriseButton;
	public int iconCounter = 0;

	public GameObject goToObject;
	public GameObject turnOfObject;
	public GameObject modelDownloader;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnInputClicked(InputClickedEventData eventData)
	{
		Collider[] hitColliders = Physics.OverlapSphere(mainCursor.transform.position, 0.1f);



		foreach (var col in hitColliders) {
			if (col.gameObject.tag == "hiriseLocation") {
				if (!col.GetComponent<HiRiseNameID> ().selected) {
					col.GetComponent<HiRiseNameID> ().iconSelected ();

					GameObject newIcon = Instantiate (iconPrefab);
					newIcon.transform.parent = iconParent.transform;
					newIcon.GetComponent<TextMesh> ().text = col.GetComponent<HiRiseNameID> ().Name;
					newIcon.AddComponent<BoxCollider> ();
					newIcon.transform.localPosition = new Vector3 (-0.1f, (-1 - iconCounter) * 0.1f, 0);

					newIcon.GetComponent<TapResponderWithComponent> ().GoToObject = goToObject;
					newIcon.GetComponent<TapResponderWithComponent> ().TurnOffObject = turnOfObject;
					newIcon.GetComponent<TapResponderWithComponent> ().modelDownloader = modelDownloader;
					newIcon.GetComponent<TapResponderWithComponent> ().assetName = col.GetComponent<HiRiseNameID> ().ID + ".IMG";
					newIcon.GetComponent<TapResponderWithComponent> ().assetBundleName = col.GetComponent<HiRiseNameID> ().ID.ToLower() + "-bundle";
					newIcon.GetComponent<TapResponderWithComponent> ().assetTitle = col.GetComponent<HiRiseNameID> ().Name;
                    newIcon.GetComponent<TapResponderWithComponent>().isHirise = true;
                    newIcon.GetComponent<TapResponderWithComponent>().sharingSelected = GameObject.FindGameObjectWithTag("sharingToggle").GetComponent<InteractiveToggle>().HasSelection;

                    iconCounter += 1;
				}
			}

		}
	}

    public void OnFocusEnter()
    {
        if (hiriseButton.GetComponent<InteractiveToggle>().HasSelection)
        {
            GameObject.FindGameObjectWithTag("cursorDisc").GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void OnFocusExit()
    {
        if (hiriseButton.GetComponent<InteractiveToggle>().HasSelection)
        {
            GameObject.FindGameObjectWithTag("cursorDisc").GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
