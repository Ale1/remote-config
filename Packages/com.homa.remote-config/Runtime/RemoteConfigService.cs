using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Homa.RemoteConfig
{
    public static class RemoteConfigService
    {
        public enum Source
        {
            LOCAL,
            REMOTE
        }
        
        #region Public API
        /// <summary>
        /// todo: add xml comments
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="version"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPackageParams(string packageName, string version, Source source)
        {
            return GetRecon(source)?.GetPackageParams(packageName, version); 
        }

        /// <summary>
        /// todo: add xml comments
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="version"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Package GetPackage(string packageName, string version, Source source)
        {
            return GetRecon(source)?.GetPackageClone(packageName, version); 
        }
        
        
        /// <summary>
        /// todo: add xml comments
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="versionNumber"></param>
        /// <param name="data"></param>
        /// <param name="jObject"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static bool GetRemote(string packageName, string versionNumber, JObject data, out JObject jObject)
        {
            throw new NotImplementedException();
        }    
        
        
        /// <summary>
        /// todo: add xml comments
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="versionNumber"></param>
        /// <param name="data"></param>
        /// <param name="deserializedRecon"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static bool GetRemote<T>(string packageName, string versionNumber, JObject data, out T deserializedRecon) where T : class
        {
            throw new NotImplementedException();
        }
        #endregion
        
        
        #region private
        private static ReconSettings _remoteRecon;
        private static ReconSettings RemoteRecon
        {
            get
            {
                if (_remoteRecon == null)
                    _remoteRecon = Resources.Load<ReconSettings>("RemoteConfigSettings"); // todo: fetch from constants, not hardcoded string!
                return _remoteRecon;
            }
        }

        private static ReconSettings LocalRecon
        {
            get
            {
                if (_remoteRecon == null)
                    _remoteRecon = Resources.Load<ReconSettings>("RemoteConfigSettings"); // todo: fetch from constants, not hardcoded string!
                return _remoteRecon;
            }
        }

        private static ReconSettings GetRecon(Source source)
        {
            switch (source)
            {
                case(Source.LOCAL):
                    return LocalRecon;
                case(Source.REMOTE):
                    return RemoteRecon;
                default:
                    Debug.LogError($"RemoteConfigService :: unknown source: {source} ");
                    return null;
            }
        }
        #endregion
    }
}
    





