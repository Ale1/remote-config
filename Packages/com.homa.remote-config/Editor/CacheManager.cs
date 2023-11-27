using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Homa.RemoteConfig.Editor
{
    public static class CacheManager
    {
        private static string CachePath(ReconSettings recon) => $"ProjectSettings/{recon.name}";
        
        public static string CachedJson(this ReconSettings recon)
        {
            var path = CachePath(recon);
            
            if (File.Exists(path))
            {
                    return File.ReadAllText(path);
            }
            else
            {
                    Debug.LogWarning($"Cached JSON not found at path: {path}, creating new");
                    var json = recon.ObjectToJson();
                    File.WriteAllText(path, json);
                    return json;
            }
        }

        public static bool RestoreFromCache(this ReconSettings recon)
        {
                try
                {
                    string jsonData = CachedJson(recon);
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        JsonConvert.PopulateObject(jsonData, recon);
                        return true;
                    }

                    return false;
                }
                catch (JsonException e)
                {
                    Debug.LogError($"Error applying cache: {e.Message}");
                    return false;
                }
            
        }

        public static bool SaveToCache(this ReconSettings recon)
        {
            var path = CachePath(recon);
            
                try
                {
                    string jsonData = recon.ObjectToJson();
                    File.WriteAllText(path, jsonData);
                    return true;
                }
                catch (IOException e)
                {
                    Debug.LogError($"Error saving to cache: {e.Message}");
                    return false;
                }
        }
    }
}
