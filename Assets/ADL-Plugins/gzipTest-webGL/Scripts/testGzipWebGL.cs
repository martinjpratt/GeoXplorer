using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;




public class testGzipWebGL : MonoBehaviour
{
#if UNITY_WEBGL

	private string myFile = "testLZ4b.png.gz";

	//an output Buffer for the decompressed gz buffer
	private byte[] outbuffer = null;
	private Texture2D tex = null;

	#if UNITY_EDITOR
		byte[] wwb = null;
	#else
		private WWW ww2 = null;
	#endif
	
	string path = "";

	private bool downloadDone2;

	private string log = "";

    //log for output of results
    void plog(string t)
    {
        log += t + "\n"; ;
    }

	void Start(){
		path = Application.streamingAssetsPath;

		tex = new Texture2D(1,1,TextureFormat.RGBA32, false);
		//get a gz file from StreamingAssets
		#if !UNITY_EDITOR
			StartCoroutine( getFromStreamingAssets() );		
		#else
			StartCoroutine( getFromStreamingAssetsEditor() );
		#endif
		
    }


	
	void OnGUI(){
		
		if (downloadDone2 == true) {
			GUI.Label(new Rect(10, 0, 250, 30), "got package, ready to extract");
		
		

			if (GUI.Button(new Rect(10, 90, 230, 50), "start StreamingAssets gz test")) {
				#if !UNITY_EDITOR
					plog("ungzip2: "+lzip.unGzip2(ww2.bytes, outbuffer).ToString()+" bytes");
				#else
					plog("ungzip2: "+lzip.unGzip2(wwb, outbuffer).ToString()+" bytes");
				#endif
					if(outbuffer != null) tex.LoadImage(outbuffer);
					
			}
		}

		if(tex != null) GUI.DrawTexture(new Rect(360, 10, 375, 300), tex);

		GUI.TextArea(new Rect(10, 370, Screen.width - 20, Screen.height - 400), log);
				
	}



	// ============================================================================================================================================================= 

	

	#if !UNITY_EDITOR
    IEnumerator getFromStreamingAssets() {
		plog("getting buffer from StreamingAssets ...");

        ww2 = new WWW(path +"/"+ myFile);

        yield return ww2;

        if (ww2.error != null) plog("Streaming Assets Error: "+ww2.error.ToString());
		else outbuffer = new byte[ lzip.gzipUncompressedSize(ww2.bytes) ];
		plog("Got buffer");
		downloadDone2 = true;
    }
	#else

	IEnumerator getFromStreamingAssetsEditor() {
		plog("getting buffer from StreamingAssets ...");
		yield return true;

		if(System.IO.File.Exists(path+"/"+myFile)) {
			wwb = System.IO.File.ReadAllBytes(path+"/"+myFile);
		}
		if(wwb == null) plog("Could not find file: " + myFile + " in StreamingAssets");
		else outbuffer = new byte[ lzip.gzipUncompressedSize(wwb) ];
		plog("Got buffer");
		downloadDone2 = true;
	}
	#endif
#else
    void Start(){
        Debug.Log("Only for WebGL");
    }
#endif

}

