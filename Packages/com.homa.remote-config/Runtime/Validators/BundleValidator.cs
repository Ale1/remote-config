using System.Text.RegularExpressions;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Homa.RemoteConfig
{
    internal static class BundleValidator
    {
        public static bool Validate(string targetAndroidId, string targetIosId)
        {
            if (Application.isEditor)
            {
                return (ValidAndroidBundleId(targetAndroidId) && ValidIosBundleId(targetIosId));
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                return ValidAndroidBundleId(targetAndroidId);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return ValidIosBundleId(targetIosId);
            }

            Debug.LogError("unknown platform");
            return false;
        }

        private static bool ValidAndroidBundleId(string targetId)
        {

            if (!CanBeUsedAsBundleID(targetId))
            {
                Debug.LogWarning("Remote config sent an unusable android target bundle id with special characters!. skipping validation");
                return true;
            }
            
#if UNITY_EDITOR
            string androidBundleId = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
#else
            string androidBundleId = Application.identifier;
#endif
            
            return (targetId != null && androidBundleId == targetId);
        }

        private static bool ValidIosBundleId(string targetId)
        {
            
            if (!CanBeUsedAsBundleID(targetId))
            {
                Debug.LogWarning("Remote config sent an unusable target Ios bundle id with special characters!. skipping validation");
                return true;
            }
            
            
#if UNITY_EDITOR
            string iosBundleId = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
#else
            string iosBundleId = Application.identifier;
#endif

            return (targetId != null && iosBundleId == targetId);
        }

        private static bool CanBeUsedAsBundleID(string id)
        {
                // Regular expression for validating bundle ID
                // This allows: letters, digits, periods, and underscores. It also checks for starting with a letter.
                string pattern = @"^[a-zA-Z][a-zA-Z0-9_\.]*$";

                return Regex.IsMatch(id, pattern);
        }
    }
}