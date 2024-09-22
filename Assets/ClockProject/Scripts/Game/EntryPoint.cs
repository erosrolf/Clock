using Clock.Controller;
using Clock.Model;
using Clock.Services.TimerService;
using Clock.View;
using R3;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ClockView _clockView;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        
        private async void Awake()
        {
            ClockModel clockModel = new ClockModel();
            ClockController clockController = new ClockController(clockModel);
            _clockView.Construct(clockController);
            await clockController.SynchronizeTime();
            clockController.StartTimer();
        }
    }
}