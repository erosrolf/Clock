using System;
using Clock.Controller;
using Clock.Model;
using Clock.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// The <c>EntryPoint</c> class serves as the main entry point for the game.
    /// It initializes the clock system, manages loading screens, 
    /// and ensures the game runs at the specified frame rate.
    /// </summary>
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ClockView _clockView;
        [SerializeField] private GameObject _loadingScreen;

        /// <summary>
        /// Automatically starts the game settings before the scene loads.
        /// Sets the target frame rate and modifies screen sleep settings.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        
        private async void Awake()
        {
            _loadingScreen.SetActive(true); // Show the loading screen
            ClockModel clockModel = new ClockModel(); // Create a new ClockModel instance
            clockModel.UtcOffset = TimeZoneInfo.Local.BaseUtcOffset; // Set the local UTC offset
            ClockController clockController = new ClockController(clockModel); // Instantiate ClockController with the model
            _clockView.Construct(clockController); // Construct the ClockView with the controller
            await TryGetCurrentTime(clockController); // Attempt to fetch the current time
            await UniTask.WaitForSeconds(0.5f); // Wait for view updated
            _loadingScreen.SetActive(false); // Hide the loading screen
            clockController.StartClock(); // Start the clock
        }

        /// <summary>
        /// Tries to synchronize the clock with the current time asynchronously.
        /// Logs an error if the synchronization fails and into SynchronizeTime sets local time.
        /// </summary>
        /// <param name="clockController">The ClockController responsible for time management.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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