// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DesktopTimerModule.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer
{
    using System;
    using System.Linq;
    using Autofac;
    using Caliburn.Micro;
    using DesktopTimer.Core;
    using DesktopTimer.Domain;
    using DesktopTimer.ViewModels;

    /// <summary>
    /// Configures the dependency injection container for the <c>DesktopTimer</c> assembly.
    /// </summary>
    public class DesktopTimerModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DesktopTimerCoreModule>();
            builder.RegisterModule<DesktopTimerDomainModule>();

            builder.RegisterType<WindowManager>().As<IWindowManager>().InstancePerLifetimeScope();
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .AssignableTo<ViewModelBase>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(t => t.Name.EndsWith("View", StringComparison.Ordinal))
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Views", StringComparison.Ordinal))
                .AsSelf()
                .InstancePerDependency();
        }
    }
}