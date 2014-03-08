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
        /// The currently loaded timers.
        /// </summary>
        private IList<Timer> timers;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerManager"/> class.
        /// </summary>
        /// <param name="store">The timer storage component.</param>
        public TimerManager(ITimerStore store)
        {
            this.store = store;
        }

        /// <summary>
        /// Gets the currently loaded timers.
        /// </summary>
        /// <returns>A sequence of timers.</returns>
        public IEnumerable<Timer> GetTimers()
        {
            if (this.timers == null)
            {
                this.timers = this.store.RetrieveTimers().ToList();
            }

            return this.timers;
        }

        /// <summary>
        /// Reloads all timers from the store.
        /// </summary>
        /// <returns>A sequence of timers.</returns>
        public IEnumerable<Timer> ReloadTimers()
        {
            this.timers = this.store.RetrieveTimers().ToList();
            return this.timers;
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

            Timer existingTimer = this.timers.FirstOrDefault(t => t.Id == timer.Id);
            if (existingTimer != null)
            {
                int index = this.timers.IndexOf(existingTimer);
                this.timers.Remove(existingTimer);
                this.timers.Insert(index, timer);
            }
            else
            {
                this.timers.Add(timer);
            }

            this.store.PersistTimers(this.timers);
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

            Timer existingTimer = this.timers.FirstOrDefault(t => t.Id == timer.Id);
            if (existingTimer == null)
            {
                return false;
            }

            this.timers.Remove(existingTimer);
            this.store.PersistTimers(this.timers);
            
            return true;
        }
    }
}