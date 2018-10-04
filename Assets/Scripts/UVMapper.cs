using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVMapper : MonoBehaviour
{

    public Material surfaceMaterial;
    public string meshName;
    public string surfaceType;
    string texturePath;
    string readmeTexturePath;
    public string maxHeight;
    public string minHeight;

    public void FetchTexture()
    {
        CreateTexturePath(meshName);

        surfaceMaterial = (Material)Resources.Load("HiRiseMaterial", typeof(Material));
        StartCoroutine(GoFetchTexture());
        StartCoroutine(FindElevInfo());
    }

    private IEnumerator FindElevInfo()
    {
        using (WWW www = new WWW("https://www.uahirise.org/PDS/EXTRAS/DTM/" + readmeTexturePath))
        {
            yield return www;
            print("https://www.uahirise.org/PDS/EXTRAS/DTM/" + readmeTexturePath);
            string[] lineData = www.text.Split("\n"[0]);
            string[] minHeightHeightData = lineData[13].Split(" "[0]);
            string[] maxHeightHeightData = lineData[14].Split(" "[0]);
            minHeight = minHeightHeightData[7] + " m";
            maxHeight = maxHeightHeightData[7] + " m";
        }
    }


    // Use this for initialization
    IEnumerator GoFetchTexture()
    {
        string url = "https://www.uahirise.org/PDS/EXTRAS/DTM/" + texturePath + surfaceType + ".jpg";

        print(url);
        using (WWW www = new WWW(url))
        {
            //wait for download to complete
            yield return www;

            //assign texture
            Renderer renderer = GetComponent<Renderer>();
            surfaceMaterial.mainTexture = www.texture;
            renderer.material = surfaceMaterial;
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3 maxBounds = mesh.bounds.max;
        Vector3 minBounds = mesh.bounds.min;

        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / (maxBounds.x + minBounds.x), vertices[i].y / -(maxBounds.y + minBounds.y));
        }
        mesh.uv = uvs;
    }


    void CreateTexturePath(string mName)
    {
        string orbNumStr = mName.Substring(6, 6);
        string orbNumStr1 = mName.Substring(13, 4);
        string orbNumStr2 = mName.Substring(18, 6);
        string orbNumStr3 = mName.Substring(25, 4);
        string orbNumStr4 = mName.Substring(0, 34);
        int orbNumInt = int.Parse(orbNumStr);
        int lowerOrbInt = RoundDown(orbNumInt);
        int upperOrbInt = RoundUp(orbNumInt) - 1;
        string missionTime = "ESP";
        if (lowerOrbInt < 11000)
        {
            missionTime = "PSP";
        }

        readmeTexturePath = missionTime + "/ORB_" + lowerOrbInt.ToString("000000") + "_" + upperOrbInt.ToString("000000") + "/" + missionTime + "_" + orbNumStr + "_" + orbNumStr1 + "_" + missionTime + "_" + orbNumStr2 + "_" + orbNumStr3 + "/" + "README.TXT";
        texturePath = missionTime + "/ORB_" + lowerOrbInt.ToString("000000") + "_" + upperOrbInt.ToString("000000") + "/" + missionTime + "_" + orbNumStr + "_" + orbNumStr1 + "_" + missionTime + "_" + orbNumStr2 + "_" + orbNumStr3 + "/" + orbNumStr4;
    }


    int RoundUp(int toRound)
    {
        if (toRound % 100 == 0) return toRound;
        return (100 - toRound % 100) + toRound;
    }

    int RoundDown(int toRound)
    {
        return toRound - toRound % 100;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
