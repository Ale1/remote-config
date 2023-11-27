using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Homa.RemoteConfig
{
    
    /// <summary>
    /// Represents the main entry point for accessing remote configuration services.
    /// </summary>
    public static class RemoteConfigService
    {
        public enum Source
        {
            LOCAL,
            REMOTE
        }
        
        #region Public API
        /// <summary>
        /// Retrieves a dictionary of parameters for a specified package of given version.
        /// </summary>
        /// <param name="packageName">The name of the package to retrieve parameters for.</param>
        /// <param name="version">The version of the package.</param>
        /// <param name="source">The cache from which to retrieve the package parameters (Local or Remote).</param>
        /// <returns>A dictionary(string, string) containing the parameters of the specified package and version.</returns>
        public static Dictionary<string, string> GetPackageParams(string packageName, string version, Source source)
        {
            return GetRecon(source)?.GetPackageParams(packageName, version); 
        }

        /// <summary>
        /// Retrieves a package object for a specified package name and version from the given source (Local or Remote).
        /// </summary>
        /// <param name="packageName">The name of the package to be retrieved.</param>
        /// <param name="version">The version of the package.</param>
        /// <param name="source">The source from which to retrieve the package (Local or Remote).</param>
        /// <returns>A Package object</returns>
        /// <returns></returns>
        public static Package GetPackage(string packageName, string version, Source source)
        {
            return GetRecon(source)?.GetPackageClone(packageName, version); 
        }
        
        
        /// <summary>
        /// Fetches remote config data without any serialization into Package objects. Essentially return the raw JObject from the Json representation of ao_packages.
        /// </summary>
        /// <param name="packageName">The name of the package for which to retrieve data</param>
        /// <param name="versionNumber">The version of the package desired</param>
        /// <param name="jObject">The output JObject containing the contents of ao_packages json token.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        private static bool GetRemote(string packageName, string versionNumber, out JObject jObject)
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
    





