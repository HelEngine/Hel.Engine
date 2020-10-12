using System;

namespace Hel.Engine.Jobs.ExceptionExtensions
{
    /// <summary>
    /// The job you are trying to run has not been queued
    /// </summary>
    public class JobNotQueuedException : Exception
    {
        public JobNotQueuedException() { }

        public JobNotQueuedException(string message) : base(message) { }
    }
}
