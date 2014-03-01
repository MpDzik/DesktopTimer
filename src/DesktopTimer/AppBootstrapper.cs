// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Autofac;
    using Caliburn.Micro;
    using DesktopTimer.ViewModels;

    /// <summary>
    /// The application bootstrapper.
    /// </summary>
    public class AppBootstrapper : Bootstrapper<MainViewModel>
    {
        /// <summary>
        /// The dependency injection container.
        /// </summary>
        private IContainer container;

        /// <summary>
        /// Configures the bootstrapper.
        /// </summary>
        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DesktopTimerModule>();
            this.container = builder.Build();
        }

        /// <summary>
        /// Gets the service instance of the specified type and key.
        /// </summary>
        /// <param name="service">The type of service to get.</param>
        /// <param name="key">The key of the service to get.</param>
        /// <returns>The service instance.</returns>
        protected override object GetInstance(Type service, string key)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                if (this.container.IsRegistered(service))
                {
                    return this.container.Resolve(service);
                }
            }
            else
            {
                if (this.container.IsRegisteredWithName(key, service))
                {
                    this.container.ResolveNamed(key, service);
                }
            }

            throw new ArgumentException(string.Format(
                CultureInfo.CurrentCulture,
                "Could not locate any instances of service {0}.",
                key ?? service.Name));
        }

        /// <summary>
        /// Gets all instances of the specified service type.
        /// </summary>
        /// <param name="service">The type of service to get.</param>
        /// <returns>All instances of the specified service type.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            Type type = typeof(IEnumerable<>).MakeGenericType(service);
            return this.container.Resolve(type) as IEnumerable<object>;
        }
    }
}