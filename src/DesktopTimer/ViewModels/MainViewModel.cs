// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.ViewModels
{
    using Caliburn.Micro;
    using DesktopTimer.Properties;

    /// <summary>
    /// The main view model.
    /// </summary>
    public class MainViewModel : PropertyChangedBase
    {
        /// <summary>
        /// The main window title.
        /// </summary>
        private string windowTitle = Resources.MainViewModel_windowTitle;

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
    }
}