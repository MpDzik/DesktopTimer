// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerDisplay.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Domain
{
    using System.Drawing;

    /// <summary>
    /// Represents the display parameters of a timer.
    /// </summary>
    public class TimerDisplay
    {
        /// <summary>
        /// Gets or sets the timer's background color.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the timer's foreground color.
        /// </summary>
        public Color ForeColor { get; set; }
    }
}