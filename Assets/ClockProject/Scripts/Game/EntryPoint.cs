using System;
using Clock.Controller;
using Clock.Model;
using Clock.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ClockView _clockView;
        [SerializeField] private GameObject _loadingScreen;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        
        private async void Awake()
        {
            _loadingScreen.SetActive(true);
            ClockModel clockModel = new ClockModel();
            clockModel.UtcOffset = TimeZoneInfo.Local.BaseUtcOffset;
            ClockController clockController = new ClockController(clockModel);
            _clockView.Construct(clockController);
            await TryGetCurrentTime(clockController);
            await UniTask.WaitForSeconds(0.5f);
            _loadingScreen.SetActive(false);
            clockController.StartClock();
        }

        private async UniTask TryGetCurrentTime(ClockController clockController)
        {
            try
            {
                await clockController.SynchronizeTime();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}