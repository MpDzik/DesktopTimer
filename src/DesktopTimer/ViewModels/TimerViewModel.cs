// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerViewModel.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.ViewModels
{
    using System;
    using System.Drawing;
    using DesktopTimer.Core;
    using DesktopTimer.Domain;
    using Color = System.Windows.Media.Color;

    /// <summary>
    /// Represents the view model for the timer dialog.
    /// </summary>
    public class TimerViewModel : ViewModelBase
    {
        /// <summary>
        /// The timer manager service.
        /// </summary>
        private readonly ITimerManager timerManager;

        /// <summary>
        /// The timer displayed in the window.
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerViewModel"/> class.
        /// </summary>
        /// <param name="timerManager">The timer manager service.</param>
        public TimerViewModel(ITimerManager timerManager)
        {
            this.timerManager = timerManager;

            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += (sender, args) => this.NotifyOfPropertyChange(() => this.RemainingTime);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Gets or sets the timer displayed in the window.
        /// </summary>
        public Timer Timer
        {
            get
            {
                return this.timer;
            }

            set
            {
                this.timer = value;
                this.NotifyOfPropertyChange(() => this.Timer);
            }
        }

        /// <summary>
        /// Gets the remaining time until the target time point associated with the timer.
        /// </summary>
        public TimeSpan RemainingTime
        {
            get
            {
                var countdownTimer = this.Timer as CountdownTimer;
                if (countdownTimer != null)
                {
                    return countdownTimer.TargetTimePoint - DateTime.Now;
                }

                return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Gets the background color.
        /// </summary>
        public Color BackColor
        {
            get
            {
                var color = this.Timer.Display.BackColor;
                return Color.FromArgb(color.A, color.R, color.G, color.B);
            }
        }

        /// <summary>
        /// Gets the foreground color.
        /// </summary>
        public Color ForeColor
        {
            get
            {
                var color = this.Timer.Display.ForeColor;
                return Color.FromArgb(color.A, color.R, color.G, color.B);
            }
        }

        /// <summary>
        /// Gets or sets the distance of the window from the left edge of the screen.
        /// </summary>
        public int Left
        {
            get
            {
                return this.timer.Position.X;
            }

            set
            {
                this.timer.Position = new Point(value, this.timer.Position.Y);
                this.NotifyOfPropertyChange(() => this.Left);
            }
        }

        /// <summary>
        /// Gets or sets the distance of the window from the top edge of the screen.
        /// </summary>
        public int Top
        {
            get
            {
                return this.timer.Position.Y;
            }

            set
            {
                this.timer.Position = new Point(this.timer.Position.X, value);
                this.NotifyOfPropertyChange(() => this.Left);
            }
        }

        /// <summary>
        /// Called when the timer window is closed.
        /// </summary>
        public void OnWindowClosed()
        {
            this.timerManager.SaveTimer(this.timer);
        }
    }
}