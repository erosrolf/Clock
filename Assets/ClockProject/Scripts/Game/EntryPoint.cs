using Clock.Controller;
using Clock.Model;
using Clock.View;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ClockView _clockView;
        private ClockController _clockController;
        private ClockModel _clockModel;

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
        }
    }
}