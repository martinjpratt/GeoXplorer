//Written by Martin Pratt, Fossett Lab for Virtual Planetary Exploration
//Washington University in St. Louis
//Adapted from a DAE downloader file to be more specific to AusGeol models
//
//June 2018

//Very much a work in progress

using UnityEngine;
using System.Net;
using System.IO;
using System;
//using TriLib;
using System.Collections;
using UnityEngine.Networking;
using Unity.IO.Compression;
using System.Text;

#if WINDOWS_UWP
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System;
#endif


public class DAEDownloader : MonoBehaviour
{
    
    /*
    public void Start()
    {
        WebClient webClient = new WebClient();
        Stream data = webClient.OpenRead("http://www.ausgeol.org/data/public/AusGeolSites/BoatHarbour8/BoatHarbour8_collada.zip");
        // This stream cannot be opened with the ZipFile class because CanSeek is false.
        StartCoroutine(NzipFromStream(data, @".\temp"));

        using (var assetLoader = new AssetLoader())
        {
            var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
            assetLoaderOptions.Scale = 0.5f;
            assetLoaderOptions.DontLoadCameras = true;
            assetLoaderOptions.DontLoadLights = true;
            assetLoaderOptions.GenerateMeshColliders = true;
            assetLoader.LoadFromFile("./temp/BoatHarbour8_collada.dae", assetLoaderOptions, this.gameObject);
            Shader shader1 = Shader.Find("Unlit/Texture");
            GetComponentInChildren<Renderer>().material.shader = shader1;
        }
    }

    private IEnumerator NzipFromStream(Stream zipStream, string outFolder)
    {
        ZipInputStream zipInputStream = new ZipInputStream(zipStream);
        ZipEntry zipEntry = zipInputStream.GetNextEntry();
        while (zipEntry != null)
        {
            String entryFileName = zipEntry.Name;
            // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
            // Optionally match entrynames against a selection list here to skip as desired.
            // The unpacked length is available in the zipEntry.Size property.

            byte[] buffer = new byte[4096];     // 4K is optimum

            // Manipulate the output filename here as desired.
            String fullZipToPath = Path.Combine(outFolder, entryFileName);
            string directoryName = Path.GetDirectoryName(fullZipToPath);
            if (directoryName.Length > 0)
                Directory.CreateDirectory(directoryName);

            // Skip directory entry
            string fileName = Path.GetFileName(fullZipToPath);
            if (fileName.Length == 0)
            {
                zipEntry = zipInputStream.GetNextEntry();
                continue;
            }

            // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
            // of the file, but does not waste memory.
            // The "using" will close the stream even if an exception occurs.
            using (FileStream streamWriter = File.Create(fullZipToPath))
            {
                StreamUtils.Copy(zipInputStream, streamWriter, buffer);
            }
            zipEntry = zipInputStream.GetNextEntry();
        }
        yield return zipEntry;
    }
    */

    /*
    public void Start()
    {
        StartCoroutine(Decompress());
        
    }

    private IEnumerator Decompress()
    {
        WWW www = new WWW("http://www.ausgeol.org/data/public/AusGeolSites/BoatHarbour8/BoatHarbour8_collada.zip");
        yield return www;
        System.IO.File.WriteAllBytes(@"./Temp/new.zip", www.bytes);

        ZipFile zip = ZipFile.Read(@"./Temp/new.zip");


        zip.ExtractAll("./Temp");


        using (var assetLoader = new AssetLoader())
        {
            var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
            assetLoaderOptions.Scale = 0.5f;
            assetLoaderOptions.DontLoadCameras = true;
            assetLoaderOptions.DontLoadLights = true;
            assetLoaderOptions.GenerateMeshColliders = true;
            assetLoader.LoadFromFile("./temp/BoatHarbour8_collada.dae", assetLoaderOptions, this.gameObject);
            Shader shader1 = Shader.Find("Unlit/Texture");
            GetComponentInChildren<Renderer>().material.shader = shader1;
        }


    }
    */
    /*
    public void Start()
    {
        StartCoroutine(Decompress());

    }
    private IEnumerator Decompress()
    {
        
        WWW www = new WWW("http://www.ausgeol.org/data/public/AusGeolSites/BoatHarbour8/BoatHarbour8_collada.zip");
        yield return www;

#if WINDOWS_UWP
        System.IO.File.WriteAllBytes(Path.Combine(ApplicationData.Current.RoamingFolder.Path, "new.zip"), www.bytes);
        int[] progress = new int[1];
        int[] progress2 = new int[1];
        int zres = lzip.decompress_File(Path.Combine(ApplicationData.Current.RoamingFolder.Path, "new.zip"), ApplicationData.Current.RoamingFolder.Path, progress, null, progress2);

        using (var assetLoader = new AssetLoader())
        {
            var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
            assetLoaderOptions.Scale = 0.5f;
            assetLoaderOptions.DontLoadMaterials = true;
            assetLoaderOptions.DontLoadCameras = true;
            assetLoaderOptions.DontLoadLights = true;
            assetLoaderOptions.GenerateMeshColliders = true;
            assetLoader.LoadFromFile(Path.Combine(ApplicationData.Current.RoamingFolder.Path, "BoatHarbour8_collada.dae"), assetLoaderOptions, this.gameObject);
            Shader shader1 = Shader.Find("Unlit/Texture");
            GetComponentInChildren<Renderer>().material.shader = shader1;
            Texture2D tex;
            tex = new Texture2D(4, 4);
            byte[] fileData = File.ReadAllBytes(Path.Combine(ApplicationData.Current.RoamingFolder.Path, "BoatHarbour8_collada.jpg"));
            tex.LoadImage(fileData);
            GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", tex);
        }

#endif

        /*
        System.IO.File.WriteAllBytes(Path.Combine("./Temp/", "new.zip"), www.bytes);
        int[] progress = new int[1];
        int[] progress2 = new int[1];
        int zres = lzip.decompress_File(Path.Combine("./Temp/", "new.zip"), "./Temp/", progress, null, progress2);

        using (var assetLoader = new AssetLoader())
        {
            var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
            assetLoaderOptions.Scale = 0.5f;
            assetLoaderOptions.DontLoadMaterials = true;
            assetLoaderOptions.DontLoadCameras = true;
            assetLoaderOptions.DontLoadLights = true;
            assetLoaderOptions.GenerateMeshColliders = true;
            assetLoader.LoadFromFile(Path.Combine("./Temp/", "BoatHarbour8_collada.dae"), assetLoaderOptions, this.gameObject);
            Shader shader1 = Shader.Find("Unlit/Texture");
            GetComponentInChildren<Renderer>().material.shader = shader1;
            Texture2D tex;
            tex = new Texture2D(4, 4);
            byte[] fileData = File.ReadAllBytes(Path.Combine("./Temp/", "BoatHarbour8_collada.jpg"));
            tex.LoadImage(fileData);
            GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", tex);
            
        }
        */

    
}