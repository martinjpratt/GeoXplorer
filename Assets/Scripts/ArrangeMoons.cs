using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeMoons : MonoBehaviour {

	public string hostPlanetCode;
	public GameObject[] moonObject;
	public GameObject[] moonTrailObject;
	public string[] moonCode;
	public int[] orbitPeriod;
	public Color[] planetColor;
	public float hostPlanetRadiusKm;
	string todaysDate;


	// Use this for initialization
	void Start () {
		todaysDate = DateTime.Now.ToString("yyyy-MM-dd");

		StartCoroutine(fetchPlanetLocations(moonObject, moonCode, hostPlanetCode));
	}

	private IEnumerator fetchPlanetLocations(GameObject[] planetObject, string[] planetCode, string hostPlanetCode)
	{

		for (int i = 0; i < planetCode.Length; i++)
		{
			string startTime;

			int timeStep;
			if (orbitPeriod[i] > 100)
			{
				startTime = DateTime.Now.AddDays(-orbitPeriod[i]).ToString("yyyy-MM-dd");
				if (DateTime.Now.AddDays(-orbitPeriod[i]) < DateTime.Parse("1900-01-08"))
				{
					startTime = "1900-01-09";
				}
				timeStep = 1+ (orbitPeriod[i] / 100);
			}
			else
			{
				startTime = DateTime.Now.AddDays(-orbitPeriod[i]).ToString("yyyy-MM-dd");
				timeStep = 1;
			}

			print ("https://ssd.jpl.nasa.gov/horizons_batch.cgi?batch=1&COMMAND=%27" + planetCode [i] + "%27&CENTER=%27" + hostPlanetCode + "%27&MAKE_EPHEM=%27YES%27&TABLE_TYPE=%27VECTOR%27&START_TIME=%27" + startTime + "%27&STOP_TIME=%27" + todaysDate + "%27&STEP_SIZE=%27" + timeStep.ToString () + "%20d%27&QUANTITIES=%2718,19%27&CSV_FORMAT=%27YES%27");
			using (WWW w = new WWW("https://ssd.jpl.nasa.gov/horizons_batch.cgi?batch=1&COMMAND=%27" + planetCode[i] + "%27&CENTER=%27" + hostPlanetCode + "%27&MAKE_EPHEM=%27YES%27&TABLE_TYPE=%27VECTOR%27&START_TIME=%27" + startTime +"%27&STOP_TIME=%27" + todaysDate + "%27&STEP_SIZE=%27" + timeStep.ToString() + "%20d%27&QUANTITIES=%2718,19%27&CSV_FORMAT=%27YES%27"))
			{
				yield return w;
				string[] stringSeparators0 = new string[] { "$$SOE\n" };
				string[] stringSeparators1 = new string[] { "$$EOE" };
				string[] firstSplit = w.text.Split(stringSeparators0, StringSplitOptions.None);
				string[] secondSplit = firstSplit[1].Split(stringSeparators1, StringSplitOptions.None);
				string[] thirdSplit = secondSplit[0].Split("\n"[0]);

				LineRenderer lr = moonTrailObject[i].GetComponent<LineRenderer>();
				lr.startColor = planetColor[i];
				lr.endColor = planetColor[i];
				for (int j = 0; j < thirdSplit.Length - 2; j++)
				{
					string[] thirdSplit2 = thirdSplit[j].Split(","[0]);
					lr.SetPosition(j, new Vector3(float.Parse(thirdSplit2[2]) / hostPlanetRadiusKm, float.Parse(thirdSplit2[4]) / hostPlanetRadiusKm, float.Parse(thirdSplit2[3]) / hostPlanetRadiusKm));
				}


				string[] fourthSplit = thirdSplit[thirdSplit.Length - 2].Split(","[0]);

				planetObject[i].transform.localPosition = new Vector3(float.Parse(fourthSplit[2]) / hostPlanetRadiusKm, float.Parse(fourthSplit[4]) / hostPlanetRadiusKm, float.Parse(fourthSplit[3]) / hostPlanetRadiusKm);
				if (thirdSplit.Length - 2 < 100)
				{
					for (int k = thirdSplit.Length - 2; k < 100; k++)
					{
						lr.SetPosition(k, new Vector3(float.Parse(fourthSplit[2]) / hostPlanetRadiusKm, float.Parse(fourthSplit[4]) / hostPlanetRadiusKm, float.Parse(fourthSplit[3]) / hostPlanetRadiusKm));
					}

				}


			}

		} 

	}

	// Update is called once per frame
	void Update () {

	}
}


