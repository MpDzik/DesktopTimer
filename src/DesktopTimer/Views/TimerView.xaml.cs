// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerView.xaml.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Views
{
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for <c>TimerView.xaml</c>.
    /// </summary>
    public partial class TimerView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerView"/> class.
        /// </summary>
        public TimerView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Called when a mouse button is pressed down on the window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void OnTimerViewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}