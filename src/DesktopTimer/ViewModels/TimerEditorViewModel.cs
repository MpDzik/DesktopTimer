// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerEditorViewModel.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.ViewModels
{
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Media;
    using DesktopTimer.Domain;

    /// <summary>
    /// Represents the view model for the timer dialog.
    /// </summary>
    public class TimerEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// The timer object.
        /// </summary>
        private CountdownTimer timer;

        /// <summary>
        /// Gets or sets the timer object.
        /// </summary>
        public CountdownTimer Timer
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
        /// Gets or sets the timer's background color.
        /// </summary>
        public Color BackColor
        {
            get
            {
                if ((this.timer != null) && (this.timer.Display != null))
                {
                    System.Drawing.Color color = this.timer.Display.BackColor;
                    return Color.FromArgb(color.A, color.R, color.G, color.B);
                }

                return default(Color);
            }

            set
            {
                if (this.timer.Display == null)
                {
                    this.timer.Display = new TimerDisplay();
                }

                this.timer.Display.BackColor = System.Drawing.Color.FromArgb(value.R, value.G, value.B);
                this.NotifyOfPropertyChange(() => this.BackColor);
            }
        }

        /// <summary>
        /// Gets or sets the timer's foreground color.
        /// </summary>
        public Color ForeColor
        {
            get
            {
                if ((this.timer != null) && (this.timer.Display != null))
                {
                    System.Drawing.Color color = this.timer.Display.ForeColor;
                    return Color.FromArgb(color.A, color.R, color.G, color.B);
                }

                return default(Color);
            }

            set
            {
                if (this.timer.Display == null)
                {
                    this.timer.Display = new TimerDisplay();
                }

                this.timer.Display.ForeColor = System.Drawing.Color.FromArgb(value.R, value.G, value.B);
                this.NotifyOfPropertyChange(() => this.ForeColor);
            }
        }

        /// <summary>
        /// Changes the timer's background color.
        /// </summary>
        public void ChangeBackColor()
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = this.timer.Display.BackColor;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.BackColor = Color.FromRgb(dialog.Color.R, dialog.Color.G, dialog.Color.B);
                }
            }
        }

        /// <summary>
        /// Changes the timer's foreground color.
        /// </summary>
        public void ChangeForeColor()
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = this.timer.Display.BackColor;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.ForeColor = Color.FromRgb(dialog.Color.R, dialog.Color.G, dialog.Color.B);
                }
            }
        }

        /// <summary>
        /// Handles the OK button.
        /// </summary>
        public void Ok()
        {
            var window = (Window)this.GetView();
            window.DialogResult = true;
            window.Close();
        }
    }
}