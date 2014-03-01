// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountdownTimer.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Domain
{
    using System;

    /// <summary>
    /// Represents a countdown timer.
    /// </summary>
    public class CountdownTimer : Timer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountdownTimer"/> class.
        /// </summary>
        public CountdownTimer()
        {
            this.TargetTimePoint = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the date and time of the point which the timer counts down to.
        /// </summary>
        public DateTime TargetTimePoint { get; set; }
    }
}