﻿using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace TriLib
{
	namespace Samples
	{
		/// <summary>
		/// Represents a model downloader sample class.
		/// </summary>
		public class ModelDownloader : MonoBehaviour
		{
			/// <summary>
			/// Replace this constant value with your model URI.
			/// </summary>
			//private const string ModelURI = "https://github.com/assimp/assimp/blob/master/test/models/FBX/spider.fbx?raw=true";
            //private const string ModelURI = "http://epsc.wustl.edu/~martinpratt/AssetBundles/BasaltColumns.fbx";
            private const string ModelURI = "https://langsdorf.blob.core.windows.net/paramaribo/Galena - PbS.dae";
            //private const string TextureURI = "http://epsc.wustl.edu/~martinpratt/AssetBundles/BasaltColumns.png";
            /// <summary>
            /// Replace this constant value with the local path you want your model to be saved.
            /// </summary>
            //private const string ModelLocalPath = "/spider.fbx";
            private const string ModelLocalPath = "/Actinolite.dae";

            /// <summary>
            /// Change this constant value to change your model scale.
            /// </summary>
            private const float ModelScale = 1f;

			/// <summary>
			/// <see cref="UnityEngine.Networking.UnityWebRequest"/> instance used to download the model.
			/// </summary>
			private UnityWebRequest _unityWebRequest;
            //private UnityWebRequest _unityWebRequest2;

            /// <summary>
            /// <see cref="UnityEngine.GUIStyle"/> instance used to center the GUI text.
            /// </summary>
            private GUIStyle _centeredStyle;

			/// <summary>
			/// <see cref="UnityEngine.Rect"/> instance used to display the centered text.
			/// </summary>
			private Rect _centeredRect;

			/// <summary>
			/// Starts downloading the model and setups the centered rect.
			/// </summary>
			protected void Start()
			{
				StartCoroutine(DownloadModel());
				_centeredRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50);
			}

			/// <summary>
			/// Shows the download progress.
			/// </summary>
			protected void OnGUI()
			{
				if (_unityWebRequest == null || _unityWebRequest.isDone)
				{
					return;
				}
				if (_centeredStyle == null)
				{
					_centeredStyle = GUI.skin.GetStyle("Label");
					_centeredStyle.alignment = TextAnchor.UpperCenter;
				}
				GUI.Label(_centeredRect, string.Format("Downloaded {0:P2}", _unityWebRequest.downloadProgress), _centeredStyle);
			}

           
            /// <summary>
            /// Downloads the model.
            /// </summary>
            /// <returns>Coroutine <see cref="System.Collections.IEnumerator"/></returns>
            private IEnumerator DownloadModel()
			{
				_unityWebRequest = UnityWebRequest.Get(ModelURI);
               // _unityWebRequest2 = UnityWebRequest.Get(TextureURI);
				yield return _unityWebRequest.SendWebRequest();
				if (string.IsNullOrEmpty(_unityWebRequest.error))
				{
					using (var assetLoader = new AssetLoader())
					{
						var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
						assetLoaderOptions.Scale = ModelScale;
                        assetLoaderOptions.DontLoadCameras = true;
                        assetLoaderOptions.DontLoadLights = true;
						assetLoader.LoadFromMemory(_unityWebRequest.downloadHandler.data, ModelLocalPath, assetLoaderOptions, gameObject);
					}
				}
				_unityWebRequest.Dispose();
				_unityWebRequest = null;
			}
		}
	}
}