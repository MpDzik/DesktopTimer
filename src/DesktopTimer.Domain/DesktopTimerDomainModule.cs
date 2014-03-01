// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DesktopTimerDomainModule.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Domain
{
    using Autofac;

    /// <summary>
    /// Configures the dependency injection container for the <c>DesktopTimer.Domain</c> assembly.
    /// </summary>
    public class DesktopTimerDomainModule : Module
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