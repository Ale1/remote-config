 using System;
 using System.Runtime.Serialization;
 using Newtonsoft.Json;

 namespace Homa.RemoteConfig
 {
     [Serializable]
     [DataContract]
     public class Package
     {
         [JsonProperty("s_package_key")] public string packageKey;

         [JsonProperty("s_version_number")] public string versionNumber;

         [JsonProperty("o_parameters")] public ParamsDictionary ParamsDictionary;


         public Package Clone()
         {
             var copiedDict = new ParamsDictionary();
             foreach (var keyValuePair in ParamsDictionary)
             {
                 copiedDict.Add(keyValuePair.Key, keyValuePair.Value);
             }

             return new Package
             {
                 packageKey = this.packageKey,
                 versionNumber = this.versionNumber,
                 ParamsDictionary = copiedDict
             };
         }

         public bool UpdateParam(string paramKey, string paramVal)
         {
             if (ParamsDictionary.ContainsKey(paramKey) && ParamsDictionary[paramKey] != paramVal)
             {
                 ParamsDictionary[paramKey] = paramVal;
                 return true;
             }

             return false;
         }

         public bool IsSyncedWith(Package otherPackage)
         {
             if (otherPackage == null) return false;

             foreach (var key in ParamsDictionary.Keys)
             {
                 if (!otherPackage.ParamsDictionary.ContainsKey(key) ||
                     otherPackage.ParamsDictionary[key] != ParamsDictionary[key])
                 {
                     return false; // Parameter is different or doesn't exist in other package
                 }
             }

             foreach (var key in otherPackage.ParamsDictionary.Keys)
             {
                 if (!ParamsDictionary.ContainsKey(key))
                 {
                     return false; // Parameter exists in other but not in this package
                 }
             }

             return true; // All parameters are the same
         }

         public bool IsParamSynced(string key, Package otherPackage)
         {
             if (otherPackage == null || !otherPackage.ParamsDictionary.ContainsKey(key) ||
                 !ParamsDictionary.ContainsKey(key))
             {
                 return false; // If either package doesn't contain the key, they are not synced
             }

             return ParamsDictionary[key] == otherPackage.ParamsDictionary[key];
         }
     }
 }