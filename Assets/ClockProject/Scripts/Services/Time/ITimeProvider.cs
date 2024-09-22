using System;
using Cysharp.Threading.Tasks;

namespace Services.Time
{
    public interface ITimeProvider
    {
        /// <summary>
        /// Asynchronously retrieves the current time from a third party API.
        /// </summary>
        /// <returns>A <see cref="DateTime"/>> representing the current time. </returns>
        UniTask<DateTime> GetCurrentTimeAsync();
    }
}