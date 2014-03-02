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
    using System.Windows;
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
        /// The view models of the displayed timers.
        /// </summary>
        private readonly IList<TimerViewModel> timerViews;

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

            this.timerViews = new List<TimerViewModel>();
            this.ReloadTimers();
        }

        /// <summary>
        /// Gets the main window title.
        /// </summary>
        public string WindowTitle
        {
            get
            {
                return Resources.MainViewModel_windowTitle;
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
        /// Reloads all timers.
        /// </summary>
        public void ReloadTimers()
        {
            foreach (TimerViewModel view in this.timerViews)
            {
                ((Window)view.GetView()).Close();
            }

            this.timerViews.Clear();
            
            this.Timers = this.timerManager.GetAllTimers().ToList();
            foreach (Timer timer in this.timers)
            {
                var view = new TimerViewModel { Timer = timer };
                this.windowManager.ShowWindow(view);
                this.timerViews.Add(view);
            }
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

            this.ReloadTimers();
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

            this.ReloadTimers();
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

            this.ReloadTimers();
        }
    }
}