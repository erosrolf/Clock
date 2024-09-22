using System;
using Clock.Services;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        private async void Start()
        {
            var timeService = new TimeService(new YandexTimeProvider());
            DateTime currentTime = await timeService.GetCurrentTimeAsync();
            Debug.Log($"Current time: {currentTime}");
        }
    }
}