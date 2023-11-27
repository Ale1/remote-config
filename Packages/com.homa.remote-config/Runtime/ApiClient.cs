using UnityEngine.Networking;
using System;
using System.Collections;

namespace Homa.RemoteConfig
{
    public class ApiClient
    {
        private const string jsonUrl = "https://gist.githubusercontent.com/ErnSur/84861de2f1bb0bf88bdc6e280f870fb2/raw/79c7404e6264145f7f082d3a73ef2eac4ae96bf4/RemoteConfigResponse.json";

        public static IEnumerator FetchJsonCoroutine(Action<string> onSuccess, Action<string> onFail)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(jsonUrl))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    onFail?.Invoke(webRequest.error);
                }
                else
                {
                    onSuccess?.Invoke(webRequest.downloadHandler.text);
                }
            }
        }

    }
}