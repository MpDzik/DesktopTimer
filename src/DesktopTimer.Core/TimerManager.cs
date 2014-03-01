// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerManager.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DesktopTimer.Core.Storage;
    using DesktopTimer.Domain;

    /// <summary>
    /// Implements the timer manager.
    /// </summary>
    public class TimerManager : ITimerManager
    {
        /// <summary>
        /// The timer storage component.
        /// </summary>
        private readonly ITimerStore store;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerManager"/> class.
        /// </summary>
        /// <param name="store">The timer storage component.</param>
        public TimerManager(ITimerStore store)
        {
            this.store = store;
        }

        /// <summary>
        /// Gets all timers.
        /// </summary>
        /// <returns>A sequence of timers.</returns>
        public IEnumerable<Timer> GetAllTimers()
        {
            return this.store.RetrieveTimers();
        }

        /// <summary>
        /// Saves the specified timer.
        /// </summary>
        /// <param name="timer">The timer to save.</param>
        public void SaveTimer(Timer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException("timer");
            }

            List<Timer> timers = this.store.RetrieveTimers().ToList();

            Timer existingTimer = timers.FirstOrDefault(t => t.Id == timer.Id);
            if (existingTimer != null)
            {
                int index = timers.IndexOf(existingTimer);
                timers.Remove(existingTimer);
                timers.Insert(index, timer);
            }
            else
            {
                timers.Add(timer);
            }

            this.store.PersistTimers(timers);
        }

        /// <summary>
        /// Removes the specified timer.
        /// </summary>
        /// <param name="timer">The timer to remove.</param>
        /// <returns>
        /// <c>true</c> if the specified timer was successfully removed or <c>false</c> if the timer was not found.
        /// </returns>
        public bool RemoveTimer(Timer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException("timer");
            }

            List<Timer> timers = this.store.RetrieveTimers().ToList();

            Timer existingTimer = timers.FirstOrDefault(t => t.Id == timer.Id);
            if (existingTimer == null)
            {
                return false;
            }

            timers.Remove(existingTimer);
            this.store.PersistTimers(timers);
            
            return true;
        }
    }
}