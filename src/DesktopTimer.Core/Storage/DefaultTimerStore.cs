// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultTimerStore.cs" company="Marek Dzikiewicz">
//   Copyright (c) Marek Dzikiewicz
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DesktopTimer.Core.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using DesktopTimer.Domain;

    /// <summary>
    /// Stores timers in an XML file in the current user's data folder.
    /// </summary>
    public class DefaultTimerStore : ITimerStore
    {
        /// <summary>
        /// Retrieves all timers from the store.
        /// </summary>
        /// <returns>A sequence of timers.</returns>
        public IEnumerable<Timer> RetrieveTimers()
        {
            return DeserializeTimers();
        }

        /// <summary>
        /// Persists the specified timers to the store.
        /// </summary>
        /// <param name="timers">The timers to persist.</param>
        public void PersistTimers(IEnumerable<Timer> timers)
        {
            if (timers == null)
            {
                throw new ArgumentNullException("timers");
            }

            var timersCollection = new TimersCollection();
            timersCollection.AddRange(timers);
            
            SerializeTimers(timersCollection);
        }

        /// <summary>
        /// Creates a <see cref="DataContractSerializer"/> for serializing the timers to an XML file.
        /// </summary>
        /// <returns>The created serializer.</returns>
        private static DataContractSerializer CreateSerializer()
        {
            return new DataContractSerializer(typeof(TimersCollection), new[] { typeof(CountdownTimer) });
        }

        /// <summary>
        /// Deserializes the timers from the <c>Timers.xml</c> file.
        /// </summary>
        /// <returns>The deserialized timers.</returns>
        private static TimersCollection DeserializeTimers()
        {
            using (Stream stream = OpenTimersXmlFile())
            {
                DataContractSerializer serializer = CreateSerializer();
                return (TimersCollection)serializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Serializes the specified timers to the <c>Timers.xml</c> file.
        /// </summary>
        /// <param name="timers">The collection of timers which will be serialized.</param>
        private static void SerializeTimers(TimersCollection timers)
        {
            using (Stream stream = OpenTimersXmlFile())
            {
                stream.SetLength(0);

                DataContractSerializer serializer = CreateSerializer();
                serializer.WriteObject(stream, timers);
            }
        }

        /// <summary>
        /// Opens an existing or creates a new timers XML file.
        /// </summary>
        /// <returns>The stream which contains the timers XML file.</returns>
        private static Stream OpenTimersXmlFile()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string folderPath = Path.Combine(appData, "DesktopTimer");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, "Timers.xml");
            if (!File.Exists(filePath))
            {
                Stream stream = File.Create(filePath);

                // Serialize empty timers collection to new file.
                DataContractSerializer serializer = CreateSerializer();
                serializer.WriteObject(stream, new TimersCollection());
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                return stream;
            }

            return File.Open(filePath, FileMode.Open);
        }
    }
}