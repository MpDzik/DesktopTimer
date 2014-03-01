// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Timer.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Domain
{
    using System;
    using System.Drawing;

    /// <summary>
    /// The base class for all timers.
    /// </summary>
    public abstract class Timer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        protected Timer()
        {
            this.Id = Guid.NewGuid();
            this.Name = "NewTimer";
            this.Display = new TimerDisplay { BackColor = Color.DodgerBlue, ForeColor = Color.Black };
        }

        /// <summary>
        /// Gets or sets the identifier of the timer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the timer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the timer.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display parameters of the timer.
        /// </summary>
        public TimerDisplay Display { get; set; }
    }
}