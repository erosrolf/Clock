using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Clock.Services
{
    [Serializable]
    public class YandexTimeResponse
    {
        public long time;
    }
    
    public class YandexTimeProvider : ITimeProvider
    {
        private const string Url = "https://yandex.com/time/sync.json";

        /// <summary>
        /// Asynchronously retrieves the current time from the Yandex API.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> representing the current date and time.</returns>
        /// <exception cref="Exception">Thrown when there is an error during the request.</exception>
        public async UniTask<DateTime> GetCurrentTimeAsync()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(Url))
            {
                webRequest.timeout = 10;
                var asyncOperation = webRequest.SendWebRequest();
                await asyncOperation;

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(webRequest.error);
                    throw new Exception("TimeService GetCurrentTimeAsync Error");
                }
                
                string jsonResponse = webRequest.downloadHandler.text;
                YandexTimeResponse result = JsonUtility.FromJson<YandexTimeResponse>(jsonResponse);
                
                DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(result.time).DateTime;
                return dateTime;
            }
        }
        
    }
}