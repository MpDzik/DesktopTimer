// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITimerManager.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Core
{
    using System.Collections.Generic;
    using DesktopTimer.Domain;

    /// <summary>
    /// Defines the API of the timer manager.
    /// </summary>
    public interface ITimerManager
    {
        /// <summary>
        /// Gets the currently loaded timers.
        /// </summary>
        /// <returns>A sequence of timers.</returns>
        IEnumerable<Timer> GetTimers();

        /// <summary>
        /// Reloads all timers from the store.
        /// </summary>
        /// <returns>A sequence of timers.</returns>
        IEnumerable<Timer> ReloadTimers();

        /// <summary>
        /// Saves the specified timer.
        /// </summary>
        /// <param name="timer">The timer to save.</param>
        void SaveTimer(Timer timer);

        /// <summary>
        /// Removes the specified timer.
        /// </summary>
        /// <param name="timer">The timer to remove.</param>
        /// <returns>
        /// <c>true</c> if the specified timer was successfully removed or <c>false</c> if the timer was not found.
        /// </returns>
        bool RemoveTimer(Timer timer);
    }
}