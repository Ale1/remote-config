using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json.Linq;
using Unity.EditorCoroutines.Editor;


namespace Homa.RemoteConfig.Editor
{
    public static class DataManager
    {
        private const string k_DirectoryPath = "Assets/Resources/";
        private static string remoteReconAssetPath => Path.Combine(k_DirectoryPath,"RemoteConfigSettings.asset");
        private static string localReconAssetPath =>  Path.Combine(k_DirectoryPath, "LocalConfigSettings.asset");


        private static ReconSettings _remoteRecon;

        public static ReconSettings RemoteRecon //Lazy Inititialization
        {
            get
            {
                if (_remoteRecon == null)
                {
                    _remoteRecon = FindOrCreateConfigSettings(remoteReconAssetPath);
                }

                return _remoteRecon;
            }
        }

        private static ReconSettings _localRecon;

        public static ReconSettings LocalRecon //Lazy Inititialization
        {
            get
            {
                if (_localRecon == null)
                {
                    _localRecon = FindOrCreateConfigSettings(localReconAssetPath);
                }

                return _localRecon;
            }
        }

        private static ReconSettings FindOrCreateConfigSettings(string reconPath)
        {
            ReconSettings recon = AssetDatabase.LoadAssetAtPath<ReconSettings>(reconPath);
            if (recon == null)
            {
                recon = ScriptableObject.CreateInstance<ReconSettings>();

                // Extract the filename without extension to use as the object's name
                string objectName = System.IO.Path.GetFileNameWithoutExtension(reconPath);
                recon.name = objectName;

                if (!Directory.Exists(k_DirectoryPath))
                {
                    Debug.LogWarning("RemoteConfig :: Had to create a Resources folder");
                    Directory.CreateDirectory(k_DirectoryPath);
                }
                
                AssetDatabase.CreateAsset(recon, reconPath);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            return recon;
        }

        
        //todo: Allow sync with Server during runtime as well.
        
        public static void SyncRemoteReconWithServer(Action OnFinished = null)
        {
            var recon = RemoteRecon;
            EditorCoroutineUtility.StartCoroutineOwnerless(ApiClient.FetchJsonCoroutine(OnSuccess, OnFail));

            void OnSuccess(string jsonString)
            {
                JObject jsonData;
                try
                {
                    jsonData = JObject.Parse(jsonString);
                }
                catch
                {
                    Debug.LogError("could not parse api data");
                    return;
                }

                try
                {
                    recon.ImportJObject(jsonData);
                }
                catch (JsonException e)
                {
                    Debug.LogError("RemoteConfig :: could not write JObject into SO " + e.Message);
                    return;
                }
                
                if(!recon.SaveToCache())
                    Debug.LogError("RemoteConfig :: problem saving to cache");
                else
                {
                    OnFinished?.Invoke();
                }
            }
            

            void OnFail(string e)
            {
                Debug.LogError(e);
            }
        }

        
/// <summary>
/// todo: xml comments
/// </summary>
        public static void ApplyRemoteUntoLocal()
        {
            if (LocalRecon.CachedJson() != LocalRecon.ObjectToJson())
            {
                EditorUtility.DisplayDialog("warning",
                    "You have unsaved changes in your local cache. Save them or clear them and try again", "ok");
            }
            else
            {
                CopySettings(RemoteRecon, LocalRecon);
            }
         
        }

/// <summary>
/// todo: xml comments
/// </summary>
        public static void ApplyLocalUntoRemote()
        {
            if (LocalRecon.CachedJson() != LocalRecon.ObjectToJson())
            {
                EditorUtility.DisplayDialog("warning",
                    "You have unsaved changes in your local cache. Save them or clear them and try again", "ok");
            }
            else
            {
                CopySettings(LocalRecon, RemoteRecon);
            }
        }

/// <summary>
/// todo: xml comments
/// </summary>
/// <param name="source"></param>
/// <param name="target"></param>
        private static void CopySettings(ReconSettings source, ReconSettings target)
        {
            if (source == null || target == null)
                Debug.LogError("RemoteConfig :: source or target cannot be null");

            // Deserialize the JSON into the target object
            JsonConvert.PopulateObject(source.CachedJson(), target);
            
            if(!target.SaveToCache())
                Debug.LogWarning("RemoteConfig :: cache not updated properly");
            
            EditorUtility.SetDirty(target);

        }
    }
}