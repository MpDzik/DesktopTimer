// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using DesktopTimer.Core;
    using DesktopTimer.Domain;
    using DesktopTimer.Properties;

    /// <summary>
    /// The main view model.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The window manager.
        /// </summary>
        private readonly IWindowManager windowManager;

        /// <summary>
        /// The timer manager.
        /// </summary>
        private readonly ITimerManager timerManager;

        /// <summary>
        /// The main window title.
        /// </summary>
        private string windowTitle = Resources.MainViewModel_windowTitle;

        /// <summary>
        /// The registered timers.
        /// </summary>
        private IList<Timer> timers;

        /// <summary>
        /// The timer which is currently selected in the timers list.
        /// </summary>
        private Timer selectedTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="windowManager">The window manager.</param>
        /// <param name="timerManager">The timer manager.</param>
        public MainViewModel(IWindowManager windowManager, ITimerManager timerManager)
        {
            if (windowManager == null)
            {
                throw new ArgumentNullException("windowManager");
            }

            if (timerManager == null)
            {
                throw new ArgumentNullException("timerManager");
            }

            this.windowManager = windowManager;
            this.timerManager = timerManager;

            this.Timers = timerManager.GetAllTimers().ToList();
        }

        /// <summary>
        /// Gets or sets the main window title.
        /// </summary>
        public string WindowTitle
        {
            get
            {
                return this.windowTitle;
            }

            set
            {
                this.windowTitle = value;
                this.NotifyOfPropertyChange(() => this.WindowTitle);
            }
        }

        /// <summary>
        /// Gets or sets the registered timers.
        /// </summary>
        public IList<Timer> Timers
        {
            get
            {
                return this.timers;
            }

            set
            {
                this.timers = value;
                this.NotifyOfPropertyChange(() => this.Timers);
            }
        }

        /// <summary>
        /// Gets or sets timer which is currently selected in the timers list.
        /// </summary>
        public Timer SelectedTimer
        {
            get
            {
                return this.selectedTimer;
            }

            set
            {
                this.selectedTimer = value;
                this.NotifyOfPropertyChange(() => this.SelectedTimer);
                this.NotifyOfPropertyChange(() => this.CanEditTimer);
                this.NotifyOfPropertyChange(() => this.CanRemoveTimer);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="EditTimer"/> method can be called.
        /// </summary>
        public bool CanEditTimer
        {
            get { return this.SelectedTimer != null; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="RemoveTimer"/> method can be called.
        /// </summary>
        public bool CanRemoveTimer
        {
            get { return this.SelectedTimer != null; }
        }

        /// <summary>
        /// Creates a new timer.
        /// </summary>
        public void AddTimer()
        {
            var timerViewModel = new TimerEditorViewModel { Timer = new CountdownTimer() };
            if (this.windowManager.ShowDialog(timerViewModel) == true)
            {
                this.timerManager.SaveTimer(timerViewModel.Timer);
            }

            this.Timers = this.timerManager.GetAllTimers().ToList();
        }

        /// <summary>
        /// Edits the currently selected timer.
        /// </summary>
        public void EditTimer()
        {
            var timer = this.SelectedTimer as CountdownTimer;
            if (timer != null)
            {
                var timerViewModel = new TimerEditorViewModel { Timer = timer };
                if (this.windowManager.ShowDialog(timerViewModel) == true)
                {
                    this.timerManager.SaveTimer(timerViewModel.Timer);
                }    
            }

            this.Timers = this.timerManager.GetAllTimers().ToList();
        }

        /// <summary>
        /// Removes the currently selected timer.
        /// </summary>
        public void RemoveTimer()
        {
            var timer = this.SelectedTimer as CountdownTimer;
            if (timer != null)
            {
                this.timerManager.RemoveTimer(timer);
            }

            this.Timers = this.timerManager.GetAllTimers().ToList();
        }
    }
}