// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DesktopTimerCoreModule.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Core
{
    using Autofac;

    /// <summary>
    /// Configures the dependency injection container for the <c>DesktopTimer.Core</c> assembly.
    /// </summary>
    public class DesktopTimerCoreModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
        }
    }
}