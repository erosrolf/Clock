using System;
using Cysharp.Threading.Tasks;

namespace Clock.Services.TimeProvider
{
    /// <summary>
    /// Class for managing the retrieval of the current time using an <see cref="ITimeProvider"/>.
    /// </summary>
    public class TimeProviderService
    {
        private ITimeProvider _timeProvider;
        
        /// <summary>
        /// Constructor for <see cref="TimeManager"/>.
        /// </summary>
        /// <param name="timeProvider">An instance of <see cref="ITimeProvider"/> for retrieving the time.</param>
        public TimeProviderService(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }
        
        /// <summary>
        /// Asynchronously retrieves the current time.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> representing the current date and time.</returns>
        public async UniTask<DateTime> GetCurrentTimeAsync() 
            => await _timeProvider.GetCurrentTimeAsync();
    }
}
