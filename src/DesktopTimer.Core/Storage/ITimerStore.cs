// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITimerStore.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Core.Storage
{
    using System.Collections.Generic;
    using DesktopTimer.Domain;

    /// <summary>
    /// Defines the API for classes which implement timer storage.
    /// </summary>
    public interface ITimerStore
    {
        /// <summary>
        /// Retrieves all timers from the store.
        /// </summary>
        /// <returns>A sequence of timers.</returns>
        IEnumerable<Timer> RetrieveTimers();

        /// <summary>
        /// Persists the specified timers to the store.
        /// </summary>
        /// <param name="timers">The timers to persist.</param>
        void PersistTimers(IEnumerable<Timer> timers);
    }
}