// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Unity.InputModule;

namespace HoloToolkit.Sharing.Tests
{
    /// <summary>
    /// Class that handles spawning sync objects on keyboard presses, for the SpawningTest scene.
    /// </summary>
    public class SyncObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private PrefabSpawnManager spawnManager = null;

        [SerializeField]
        [Tooltip("Optional transform target, for when you want to spawn the object on a specific parent.  If this value is not set, then the spawned objects will be spawned on this game object.")]
        private Transform spawnParentTransform;

        private void Awake()
        {
            if (spawnManager == null)
            {
                Debug.LogError("You need to reference the spawn manager on SyncObjectSpawner.");
            }

            // If we don't have a spawn parent transform, then spawn the object on this transform.
            if (spawnParentTransform == null)
            {
                spawnParentTransform = transform;
            }
        }

        public void SpawnBasicSyncObject()
        {
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            var spawnedObject = new SyncSpawnedObject();

            spawnManager.Spawn(spawnedObject, position, rotation, spawnParentTransform.gameObject, "SpawnedObject", false);
        }

        public void SpawnCustomSyncObject()
        {
            Vector3 position = Random.onUnitSphere * 2;
            Quaternion rotation = Random.rotation;

            var spawnedObject = new SyncSpawnTestSphere();
            spawnedObject.TestFloat.Value = Random.Range(0f, 100f);

            spawnManager.Spawn(spawnedObject, position, rotation, spawnParentTransform.gameObject, "SpawnTestSphere", false);
        }

        public void SpawnAssetBundle(string bundleName, string prefabName, string locationURL)
        {
            Vector3 position = new Vector3(0, 0, 0);
            Quaternion rotation = Quaternion.identity;

            var spawnedObject = new SyncAssetBundle();
            spawnedObject.bundleName.Value = bundleName;
            spawnedObject.prefabName.Value = prefabName;
            spawnedObject.locationURL.Value = locationURL;

            spawnManager.Spawn(spawnedObject, position, rotation, spawnParentTransform.gameObject, "SpawnedAssetBundle", false);
        }

        /// <summary>
        /// Deletes any sync object that inherits from SyncSpawnObject.
        /// </summary>
        public void DeleteSyncObject()
        {
            GameObject hitObject = GazeManager.Instance.HitObject;
            if (hitObject != null)
            {
                var syncModelAccessor = hitObject.GetComponent<DefaultSyncModelAccessor>();
                //Check the parent
                if (syncModelAccessor == null)
                {
                    syncModelAccessor = hitObject.transform.parent.gameObject.GetComponent<DefaultSyncModelAccessor>();

                    if (syncModelAccessor == null)
                    {
                        syncModelAccessor = hitObject.transform.parent.transform.parent.gameObject.GetComponent<DefaultSyncModelAccessor>();
                    }
                }

                if (syncModelAccessor != null)
                {
                    var syncSpawnObject = (SyncSpawnedObject)syncModelAccessor.SyncModel;
                    spawnManager.Delete(syncSpawnObject);
                }
            }
        }
    }
}
