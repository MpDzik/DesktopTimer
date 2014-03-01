// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimersCollection.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Core.Storage
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using DesktopTimer.Domain;

    /// <summary>
    /// Represents a collection of timers.
    /// </summary>
    [CollectionDataContract(Name = "Timers", ItemName = "Timer")]
    public class TimersCollection : List<Timer>
    {
    }
}