using UnityEngine;
using System;
#if !(UNITY_WSA_8_1 ||  UNITY_WP_8_1 || UNITY_WINRT_8_1) || UNITY_EDITOR
using System.Threading;
using System.IO;
#endif
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;

using System.Runtime.InteropServices;

#if (UNITY_WSA_8_1 ||  UNITY_WP_8_1 || UNITY_WINRT_8_1) && !UNITY_EDITOR
 using File = UnityEngine.Windows.File;
 #else
 using File = System.IO.File;
 #endif

#if NETFX_CORE
    #if UNITY_WSA_10_0
    using System.Threading.Tasks;
    using static System.IO.Directory;
    using static System.IO.File;
    using static System.IO.FileStream;
    #endif
#endif

public class testZip : MonoBehaviour
{
#if (!UNITY_WEBPLAYER && !UNITY_WEBGL)  || UNITY_EDITOR

	//we use some integer to get error codes from the lzma library (look at lzma.cs for the meaning of these error codes)
	private int zres=0;
	
	private string myFile;
	private WWW www;

    private string log;
	
	private string ppath;
	
	private bool compressionStarted, pass;
	private bool downloadDone;

	//reusable buffers
    private byte[] reusableBuffer, reusableBuffer2, reusableBuffer3;

	//fixed size buffers, that don't get resized, to perform compression/decompression of buffers in them and avoid memory allocations.
	private byte[] fixedInBuffer = new byte[1024 * 256];
	private byte[] fixedOutBuffer = new byte[1024 * 768];
	private byte[] fixedBuffer = new byte[1024 * 1024];

    //A single item integer array that changes to the current number of file that get uncompressed of a zip archive.
    //When running the decompress_File function, compare this int to the total number of files returned by the getTotalFiles function
    //to get the progress of the extraction if the zip contains multiple files.
    //If you use multiple threads, remember to use other progress integers for the other threads, otherwise there will be a sharing violation.
    //
    private int[] progress = new int[1];

	//individual file progress (in bytes)
	private int[] progress2 = new int[1];

    //log for output of results
    void plog(string t)
    {
        log += t + "\n"; ;
    }

	void Start(){

		#if (UNITY_WSA_8_1 ||  UNITY_WP_8_1 || UNITY_WINRT_8_1) && !UNITY_EDITOR
			ppath = UnityEngine.Windows.Directory.localFolder;
		#else
			ppath = Application.persistentDataPath;
		#endif

        #if UNITY_STANDALONE_OSX && !UNITY_EDITOR
		     ppath=".";
        #endif

        Debug.Log(ppath);

        //various byte buffers for testing
        reusableBuffer = new byte[4096];
        reusableBuffer2 = new byte[0];
        reusableBuffer3 = new byte[0];
		
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //call the download coroutine to download a test file
        StartCoroutine(DownloadZipFile());

    }
	

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}
	
	
	void OnGUI(){
		
		
		if (downloadDone == true) {
			GUI.Label(new Rect(10, 0, 250, 30), "package downloaded, ready to extract");
			GUI.Label(new Rect(10, 50, 650, 100), ppath);
		}
		
		
		if (compressionStarted){
            GUI.TextArea(new Rect(10, 160, Screen.width - 20, Screen.height - 170), log);
            GUI.Label(new Rect(Screen.width - 30, 0, 80,40), progress[0].ToString());
			GUI.Label(new Rect(Screen.width - 140, 0, 80,40), progress2[0].ToString());
		}

        if (downloadDone) {
		    if (GUI.Button(new Rect(10, 90, 230, 50), "start zip test")) {
                log = "";
			    compressionStarted = true;
			    DoDecompression();
		    }
			#if (UNITY_IPHONE || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_ANDROID || UNITY_STANDALONE_LINUX || UNITY_EDITOR  || UNITY_STANDALONE_WIN || UNITY_WSA)
				if (GUI.Button(new Rect(260, 90, 230, 50), "start File Buffer test")) {
					log = "";
					compressionStarted = true;
					DoDecompression_FileBuffer();
				}
			#endif
        }
		
	}



    //Test all the plugin functions.
    //
    void DoDecompression(){

		//----------------------------------------------------------------------------------------------------------------
		//commented out example on how to set the permissions of a MacOSX executbale that has been unzipped so it can run.
		//
		//lzip.setFilePermissions(ppath + "/Untitled.app", "rwx","rx","rx");
		//lzip.setFilePermissions(ppath + "/Untitled.app/Contents/MacOS/Untitled", "rwx","rx","rx");
		//
		//----------------------------------------------------------------------------------------------------------------
		
		//Windows & WSA10 only (see lzip.cs for more info)
		#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WSA)
		lzip.setEncoding(65001);//CP_UTF8  // CP_OEMCP/UNICODE = 1
		#endif

		//validate sanity of a zip archive
		plog("Validate: "+ lzip.validateFile(ppath + "/testZip.zip").ToString());


		//decompress the downloaded file
        zres = lzip.decompress_File(ppath + "/testZip.zip", ppath+"/", progress,null, progress2);
		plog("decompress: "+zres.ToString());
		plog("");

		//get the true total files of the zip
		plog("true total files: "+lzip.getTotalFiles(ppath + "/testZip.zip"));

		
		//get the total entries of the zip
		plog("true total entries: "+lzip.getTotalEntries(ppath + "/testZip.zip"));


		//entry exists
		bool eres = lzip.entryExists(ppath + "/testZip.zip", "dir1/dir2/test2.bmp");
		plog("entry exists: " + eres.ToString());


		//get entry dateTime
		plog("");
		plog("DateTime: " + lzip.entryDateTime(ppath + "/testZip.zip", "dir1/dir2/test2.bmp").ToString());


        //extract an entry
        zres = lzip.extract_entry(ppath + "/testZip.zip", "dir1/dir2/test2.bmp", ppath + "/test22P.bmp", null, progress2);
        plog("extract entry: " + zres.ToString());

        plog("");



        //compress a file and add it to a new zip
        zres = lzip.compress_File(9, ppath + "/test2Zip.zip", ppath + "/dir1/dir2/test2.bmp",false, "dir1/dir2/test2.bmp");
        plog("compress: " + zres.ToString());


        //append a file to it
        zres = lzip.compress_File(9, ppath + "/test2Zip.zip", ppath + "/dir1/dir2/dir3/Unity_1.jpg",true, "dir1/dir2/dir3/Unity_1.jpg");
        plog("append: " + zres.ToString());

		

		 plog("");
		//compress multiple files added in some lists, and protect them with a password (disabled for WSA due to certification reasons)
		//
		#if !UNITY_WSA || UNITY_EDITOR
			//create a list of files to get compressed
			List<string> myFiles = new List<string>();
			myFiles.Add(ppath + "/test22P.bmp");
			myFiles.Add(ppath + "/dir1/dir2/test2.bmp");
			//create an optional list with new names for the above listings
			List<string> myNames = new List<string>();
			myNames.Add("NEW_test22P.bmp");
			myNames.Add("dir13/dir23/New_test2.bmp");
			//use password and bz2 method
			zres = lzip.compress_File_List(9, ppath+"/newTestZip.zip", myFiles.ToArray(), progress, false, myNames.ToArray(),"password");
			plog("MultiFile Compress password: " + zres.ToString());
			myFiles.Clear(); myFiles = null; myNames.Clear(); myNames = null;
		
			//decompress a password protected archive
			zres = lzip.decompress_File(ppath + "/newTestZip.zip", ppath+"/", progress,null, progress2,"password");
			plog("decompress password: " + zres.ToString());

			plog("");
		#endif
		

        //compress a buffer to a file and name it.
        plog( "Buffer2File: "+ lzip.buffer2File(9, ppath + "/test3Zip.zip", "buffer.bin", reusableBuffer).ToString());



        //compress a buffer, name it and append it to an existing zip archive
        plog("Buffer2File append: " + lzip.buffer2File(9, ppath + "/test3Zip.zip", "dir4/buffer.bin", reusableBuffer, true).ToString());
       // Debug.Log(reusableBuffer.Length);
        plog("");


        //get the uncompressed size of a specific file in the zip archive
        plog("get entry size: " + lzip.getEntrySize(ppath + "/testZip.zip", "dir1/dir2/test2.bmp").ToString());
        plog("");


        //extract a file in a zip archive to a byte buffer (referenced buffer method)
        plog("entry2Buffer1: "+ lzip.entry2Buffer(ppath + "/testZip.zip","dir1/dir2/test2.bmp",ref reusableBuffer2).ToString() );
       // File.WriteAllBytes(ppath + "/out.bmp", reusableBuffer2);
	   plog("");

	   //extract an entry in a zip archive to a fixed size buffer
	   plog("entry2FixedBuffer: "+ lzip.entry2FixedBuffer(ppath + "/testZip.zip","dir1/dir2/test2.bmp",ref fixedBuffer).ToString() );
	   plog("");


	   //GZIP TESTS---------------------------------------------------------------------------------------------------------------

	   //create a buffer that will store the compressed gzip data.
	   //it should be at least the size of the input buffer +18 bytes.
	   var btt = new byte[reusableBuffer2.Length+18];

	   //compress a buffer to gzip format and add gzip header and footer
	   //returns total size of compressed buffer.
	   int rr = lzip.gzip(reusableBuffer2,btt,9, true, true);
	   plog("gzip compressed size: "+ rr);


	   //create a buffer to store the compressed data (optional, if you want to write out a gzip file)
	   var bt2 = new byte[rr];
	   //copy the data to the new buffer
	   Buffer.BlockCopy(btt, 0, bt2, 0, rr);

	   //write a .gz file
	   File.WriteAllBytes(ppath+"/test2.bmp.gz", bt2);

	   //create a buffer to decompress a gzip buffer
	   var bt3 = new byte[lzip.gzipUncompressedSize(bt2)];

	   //decompress the gzip compressed buffer
	   int gzres = lzip.unGzip(bt2, bt3);
	   
	   if(gzres > 0) { File.WriteAllBytes(ppath+"/test2GZIP.bmp", bt3); plog("gzip decompression: success "+gzres.ToString());}
	   else {plog("gzip decompression error: "+gzres.ToString());}

	   btt=null; bt2=null; bt3=null;
	   plog("");
	   //END GZIP TESTS-----------------------------------------------------------------------------------------------------------


        //extract a file in a zip archive to a byte buffer (new buffer method)
        var newBuffer = lzip.entry2Buffer(ppath + "/testZip.zip", "dir1/dir2/test2.bmp");
        zres = 0;
        if (newBuffer != null) zres = 1;
        plog("entry2Buffer2: "+ zres.ToString());
        // write a file out to confirm all was ok
        //File.WriteAllBytes(ppath + "/out.bmp", newBuffer);
        plog("");


		//FIXED BUFFER FUNCTIONS:
		int compressedSize = lzip.compressBufferFixed(newBuffer, ref fixedInBuffer, 9);
		plog(" # Compress Fixed size Buffer: " + compressedSize.ToString());

		if(compressedSize>0){
			int decommpressedSize = lzip.decompressBufferFixed(fixedInBuffer, ref fixedOutBuffer);
			if(decommpressedSize > 0) plog(" # Decompress Fixed size Buffer: " + decommpressedSize.ToString());
		}
		plog("");


        //compress a buffer into a referenced buffer
        pass = lzip.compressBuffer(reusableBuffer2, ref reusableBuffer3, 9);
        plog("compressBuffer1: " + pass.ToString());
        // write a file out to confirm all was ok
        //File.WriteAllBytes(ppath + "/out.bin", reusableBuffer3);


        //compress a buffer and return a new buffer with the compresed data.
        newBuffer = lzip.compressBuffer(reusableBuffer2,9);
        zres = 0;
        if (newBuffer != null) zres = 1;
        plog("compressBuffer2: " + zres.ToString());
        plog("");


        //decompress a previously compressed buffer into a referenced buffer
        pass = lzip.decompressBuffer(reusableBuffer3, ref reusableBuffer2);
        plog("decompressBuffer1: " + pass.ToString());
        //Debug.Log(reusableBuffer2.Length);
        // write a file out to confirm all was ok
        //File.WriteAllBytes(ppath + "/out.bmp", reusableBuffer2);
        zres = 0;
        if (newBuffer != null) zres = 1;


        //decompress a previously compressed buffer into a new returned buffer
        newBuffer = lzip.decompressBuffer(reusableBuffer3);
        plog("decompressBuffer2: " + zres.ToString());
        plog("");

		
        //get file info of the zip file (names, uncompressed and compressed sizes)
		plog( "total bytes: " + lzip.getFileInfo(ppath + "/testZip.zip").ToString());

        
		
        //Look through the ninfo, uinfo and cinfo Lists where the file names and sizes are stored.
        if (lzip.ninfo != null) {
            for (int i = 0; i < lzip.ninfo.Count; i++) {
                log += lzip.ninfo[i] + " - " + lzip.uinfo[i] + " / " + lzip.cinfo[i] + "\n";
            }
        }
        plog("");
		
		
		#if !(UNITY_WSA_8_1 ||  UNITY_WP_8_1 || UNITY_WINRT_8_1) || UNITY_EDITOR
			//Until a replacement function is made for 'Directory.GetFiles' on WSA8.1 this function is disabled for it.
			//Recursively compress a directory
			lzip.compressDir(ppath + "/dir1", 9, ppath + "/recursive.zip", false, null);
			plog("recursive - no. of files: "+lzip.cProgress.ToString());


			//decompress the above compressed zip to make sure all was ok.
			zres = lzip.decompress_File(ppath + "/recursive.zip", ppath+"/recursive/", progress, null, progress2);
			plog("decompress recursive: "+zres.ToString());
		#endif

        //multithreading example to show progress of extraction, using the ref progress int
        //in this example it happens to fast, because I didn't want the user to download a big file with many entrie.
		#if !NETFX_CORE
			Thread th = new Thread(decompressFunc); th.Start();
		#endif
		#if NETFX_CORE && UNITY_WSA_10_0
			Task task = new Task(new Action(decompressFunc)); task.Start();
		#endif

		//delete/replace entry example
		if(File.Exists(ppath+"/test-Zip.zip")) File.Delete(ppath+"/test-Zip.zip");
		#if UNITY_WSA && !UNITY_EDITOR
			if(File.Exists(ppath+"/testZip.zip")) lzip.fileCopy(ppath+"/testZip.zip", ppath+"/test-Zip.zip");
		#else
			if(File.Exists(ppath+"/testZip.zip")) File.Copy(ppath+"/testZip.zip", ppath+"/test-Zip.zip");
		#endif


		//replace an entry with a byte buffer
		var newBuffer3 = lzip.entry2Buffer(ppath + "/testZip.zip", "dir1/dir2/test2.bmp");
		plog("replace entry: "+lzip.replace_entry(ppath+"/test-Zip.zip", "dir1/dir2/test2.bmp", newBuffer3, 9, null).ToString() );


		//replace an entry with a file in the disk (ability to asign a password or bz2 compression)
		plog("replace entry 2: "+lzip.replace_entry(ppath+"/test-Zip.zip", "dir1/dir2/test2.bmp",ppath+"/dir1/dir2/test2.bmp", 9, null, null).ToString() );


		//delete an entry in the zip
		plog("delete entry: "+lzip.delete_entry(ppath+"/test-Zip.zip", "dir1/dir2/test2.bmp").ToString() );
		
    }

    void decompressFunc()
    {
        int res = lzip.decompress_File(ppath + "/recursive.zip", ppath + "/recursive/", progress,null, progress2);
        if (res == 1) plog("multithreaded ok"); else plog("multithreaded error: "+res.ToString());
    }






	//these functions demonstrate how to extract data from a zip file in a byte buffer.
	//
	void DoDecompression_FileBuffer() {
		#if (UNITY_IPHONE || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_ANDROID || UNITY_STANDALONE_LINUX || UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WSA)
			//we read a downloaded zip from the Persistent data path. It could be also a file in a www.bytes buffer.
			var fileBuffer = File.ReadAllBytes(ppath + "/testZip.zip");

			plog("Validate: "+ lzip.validateFile(null, fileBuffer).ToString());


			//decompress the downloaded file
			zres = lzip.decompress_File(null, ppath+"/", progress, fileBuffer, progress2);
			plog("decompress: " + zres.ToString());


			plog("true total files: " + lzip.getTotalFiles(null, fileBuffer) );
			plog("total entries: " + lzip.getTotalEntries(null, fileBuffer) );


			//entry exists
			bool eres = lzip.entryExists(null, "dir1/dir2/test2.bmp", fileBuffer);
			plog("entry exists: " + eres.ToString());


			//extract an entry
			zres = lzip.extract_entry(null, "dir1/dir2/test2.bmp", ppath + "/test22B.bmp", fileBuffer, progress2);
			plog("extract entry: " + zres.ToString());
			plog("");


			//get the uncompressed size of a specific file in the zip archive
			plog("get entry size: " + lzip.getEntrySize(null, "dir1/dir2/test2.bmp", fileBuffer).ToString());
			plog("");


			//extract a file in a zip archive to a byte buffer (referenced buffer method)
			plog("entry2Buffer1: "+ lzip.entry2Buffer(null,"dir1/dir2/test2.bmp",ref reusableBuffer2, fileBuffer).ToString() );
		   // File.WriteAllBytes(ppath + "/out.bmp", reusableBuffer2);


			//extract a file in a zip archive to a byte buffer (new buffer method)
			var newBuffer = lzip.entry2Buffer(null, "dir1/dir2/test2.bmp", fileBuffer);
			zres = 0;
			if (newBuffer != null) zres = 1;
			plog("entry2Buffer2: "+ zres.ToString());
			// write a file out to confirm all was ok
			// File.WriteAllBytes(ppath + "/out.bmp", reusableBuffer2);
			plog("");

			
			//get file info of the zip file (names, uncompressed and compressed sizes)
			plog( "total bytes: " + lzip.getFileInfo(null, null, fileBuffer).ToString());

			//Look through the ninfo, uinfo and cinfo Lists where the file names and sizes are stored.
			if (lzip.ninfo != null) {
				for (int i = 0; i < lzip.ninfo.Count; i++) {
					log += lzip.ninfo[i] + " - " + lzip.uinfo[i] + " / " + lzip.cinfo[i] + "\n";
				}
			}
			plog("");

		#endif
	}


	// ============================================================================================================================================================= 


    IEnumerator DownloadZipFile()
    {

        Debug.Log("starting download");

		myFile = "testZip.zip";

        //make sure a previous zip file having the same name with the one we want to download does not exist in the ppath folder
        if (File.Exists(ppath + "/" + myFile)) { downloadDone = true; yield break; }//File.Delete(ppath + "/" + myFile);

        //replace the link to the zip file with your own (although this will work also)
        www = new WWW("https://dl.dropboxusercontent.com/s/xve34ldz3pqvmh1/" + myFile);

        yield return www;

        if (www.error != null) Debug.Log(www.error);

        downloadDone = true;

        //write the downloaded zip file to the ppath directory so we can have access to it
        //depending on the Install Location you have set for your app, set the Write Access accordingly!
		File.WriteAllBytes(ppath + "/" + myFile, www.bytes);

        www.Dispose();
        www = null;


    }

#else
    void Start(){
        Debug.Log("Does not work on WebPlayer!");
    }
#endif

}

