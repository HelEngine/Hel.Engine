using System;

namespace Hel.Engine.Jobs.Model
{
    /// <summary>
    /// Currently unused
    /// </summary>
    [Obsolete]
    public interface IJob<T>
    {
        /// <summary>
        /// The key that represents the job. Should properly represent the functionality 
        /// of the job to ensure multiple jobs with the same function dont run. 
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Queues the job to be run whenever a thread is available.
        /// </summary>
        void QueueJobThread();

        /// <summary>
        /// Returns the data that the job utilizes.
        /// </summary>
        /// <returns></returns>
        T GetData();

        /// <summary>
        /// Runs the job logic specified on creation. 
        /// </summary>
        /// <param name="entityList">The data to pass to the job.</param>
        void Run(T data);
    }
}
