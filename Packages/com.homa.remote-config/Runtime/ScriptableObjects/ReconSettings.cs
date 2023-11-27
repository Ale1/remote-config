using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Homa.RemoteConfig
{
    [DataContract]
    public class ReconSettings : ScriptableObject
    {
        [DataMember] [JsonProperty("token")] public string token;

        [DataMember] [JsonProperty("s_manifest_name")]
        public string manifestName;

        [DataMember] [JsonProperty("s_android_bundle_id")]
        public string androidBundleId;

        [DataMember] [JsonProperty("s_ios_bundle_id")]
        public string iosBundleId;

        [DataMember] [JsonProperty("ao_packages")]
        public Package[] packages;

        public string[] GetPackageVersions(string packageName) => packages.Where(p => p.packageKey == packageName)
            .Select(p => p.versionNumber).Distinct().ToArray();

        public Package GetPackageClone(string key, string version) => packages.FirstOrDefault(p => p.packageKey == key && p.versionNumber == version)?.Clone();

        public Dictionary<string, string> GetPackageParams(string key, string version)
        {
            //!important. end-user only gets copy of dictionary, not real serializable one!
            return GetPackage(key, version).ParamsDictionary.CopyDictionary;
        }
        
        public string[] GetPackageNames() => packages.Select(p => p.packageKey).Distinct().ToArray();


        public string ObjectToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        internal Package GetPackage(string key, string version) => packages.FirstOrDefault(p => p.packageKey == key && p.versionNumber == version);

        public void ImportJObject(JObject jsonData)
        {
            var token = jsonData.Value<string>("token");
            this.token = token;
            var freshData = jsonData["res"];
            this.manifestName = freshData!.Value<string>("s_manifest_name"); //todo: get jsonProperty instead of harcoded string
            this.androidBundleId = freshData.Value<string>("s_android_bundle_id");
            this.iosBundleId = freshData.Value<string>("s_ios_bundle_id");

            var packagesToken = freshData["ao_packages"];
            if (packagesToken != null) 
                this.packages = packagesToken.ToObject<Package[]>();
            else
            {
                Debug.LogWarning("RemoteConfig :: packages token was null");
                this.packages = new Package[]{};
            }
        }


    }
}