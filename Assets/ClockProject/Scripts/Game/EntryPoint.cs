using Clock.Controller;
using Clock.Model;
using Clock.Services.TimerService;
using Clock.View;
using Cysharp.Threading.Tasks;
using R3;
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
            ClockController clockController = new ClockController(clockModel);
            _clockView.Construct(clockController);
            await clockController.SynchronizeTime();
            await UniTask.WaitForSeconds(0.5f);
            _loadingScreen.SetActive(false);
            clockController.StartTimer();
            clockController.StartUpdateView();
        }
    }
}