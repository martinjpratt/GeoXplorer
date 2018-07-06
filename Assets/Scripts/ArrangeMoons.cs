using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeMoons : MonoBehaviour {

	public string hostPlanetCode;
	public GameObject[] moonObject;
	public GameObject[] moonTrailObject;
	public string[] moonCode;
	public TimeOption orbitTimeOption;
	public int[] orbitPeriod;
	public Color[] planetColor;
	public float hostPlanetRadiusKm;
	string todaysDate;
	string timeStepString;

	public enum TimeOption{
		Days,
		Hours,
		Minutes
	};



	// Use this for initialization
	void Start () {
		string todaysDate1 = DateTime.Now.ToString("yyyy-MM-dd");
		string todaysDate2 = DateTime.Now.ToString("HH:mm");
		todaysDate = todaysDate1 + "%20" + todaysDate2;

		StartCoroutine(fetchPlanetLocations(moonObject, moonCode, hostPlanetCode));
	}

	private IEnumerator fetchPlanetLocations(GameObject[] planetObject, string[] planetCode, string hostPlanetCode)
	{
		



		for (int i = 0; i < planetCode.Length; i++)
		{
			if (orbitTimeOption == TimeOption.Days) {
				timeStepString = "d";
			}
			if (orbitTimeOption == TimeOption.Hours) {
				timeStepString = "h";
			}
			if (orbitTimeOption == TimeOption.Minutes) {
				timeStepString = "m";
			}


			string startTime;

			int timeStep;

			startTime = DateTime.Now.AddDays(-orbitPeriod[i]).ToString("yyyy-MM-dd");
			timeStep = 1;

			if (orbitTimeOption == TimeOption.Hours) {
				string startTime1 = DateTime.Now.AddHours(-orbitPeriod[i]).ToString("yyyy-MM-dd");
				string startTime2 = DateTime.Now.AddHours(-orbitPeriod[i]).ToString("HH:mm");
				startTime = startTime1 + "%20" + startTime2;
				timeStep = 1; 	
				if (orbitPeriod[i] < 50) {
					timeStep = 20;
					timeStepString = "m";
				}
			}




			print ("https://ssd.jpl.nasa.gov/horizons_batch.cgi?batch=1&COMMAND=%27" + planetCode [i] + "%27&CENTER=%27" + hostPlanetCode + "%27&MAKE_EPHEM=%27YES%27&TABLE_TYPE=%27VECTOR%27&START_TIME=%27" + startTime + "%27&STOP_TIME=%27" + todaysDate + "%27&STEP_SIZE=%27" + timeStep.ToString() + "%20" + timeStepString + "%27&QUANTITIES=%2718,19%27&CSV_FORMAT=%27YES%27");
			using (WWW w = new WWW("https://ssd.jpl.nasa.gov/horizons_batch.cgi?batch=1&COMMAND=%27" + planetCode[i] + "%27&CENTER=%27" + hostPlanetCode + "%27&MAKE_EPHEM=%27YES%27&TABLE_TYPE=%27VECTOR%27&START_TIME=%27" + startTime +"%27&STOP_TIME=%27" + todaysDate + "%27&STEP_SIZE=%27" + timeStep.ToString() + "%20" + timeStepString + "%27&QUANTITIES=%2718,19%27&CSV_FORMAT=%27YES%27"))
			{
				yield return w;
				string[] stringSeparators0 = new string[] { "$$SOE\n" };
				string[] stringSeparators1 = new string[] { "$$EOE" };
				string[] firstSplit = w.text.Split(stringSeparators0, StringSplitOptions.None);
				string[] secondSplit = firstSplit[1].Split(stringSeparators1, StringSplitOptions.None);
				string[] thirdSplit = secondSplit[0].Split("\n"[0]);

				LineRenderer lr = moonTrailObject[i].GetComponent<LineRenderer>();
				lr.positionCount = thirdSplit.Length - 1;
				lr.startColor = planetColor[i];
				lr.endColor = planetColor[i];
				for (int j = 0; j < thirdSplit.Length - 2; j++)
				{
					string[] thirdSplit2 = thirdSplit[j].Split(","[0]);
					lr.SetPosition(j, new Vector3(float.Parse(thirdSplit2[2]) / hostPlanetRadiusKm, float.Parse(thirdSplit2[4]) / hostPlanetRadiusKm, float.Parse(thirdSplit2[3]) / hostPlanetRadiusKm));
				}


				string[] fourthSplit = thirdSplit[thirdSplit.Length - 2].Split(","[0]);
				planetObject[i].transform.localPosition = new Vector3(float.Parse(fourthSplit[2]) / hostPlanetRadiusKm, float.Parse(fourthSplit[4]) / hostPlanetRadiusKm, float.Parse(fourthSplit[3]) / hostPlanetRadiusKm);
				lr.SetPosition (thirdSplit.Length - 2, new Vector3 (float.Parse (fourthSplit [2]) / hostPlanetRadiusKm, float.Parse (fourthSplit [4]) / hostPlanetRadiusKm, float.Parse (fourthSplit [3]) / hostPlanetRadiusKm));

				//if (thirdSplit.Length - 2 < 100)
				//{
				//	for (int k = thirdSplit.Length - 2; k < 100; k++)
				//	{
				//		lr.SetPosition(k, new Vector3(float.Parse(fourthSplit[2]) / hostPlanetRadiusKm, float.Parse(fourthSplit[4]) / hostPlanetRadiusKm, float.Parse(fourthSplit[3]) / hostPlanetRadiusKm));
				//	}
				//
				//}


			}

		} 

	}

	// Update is called once per frame
	void Update () {

	}
}


