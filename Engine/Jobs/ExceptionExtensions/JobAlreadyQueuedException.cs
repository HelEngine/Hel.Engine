using System;

namespace Hel.Engine.Jobs.ExceptionExtensions
{
    /// <summary>
    /// You have tried to queue a job that is already in the queue. A job can only be queued once.
    /// </summary>
    public class JobAlreadyQueuedException : Exception
    {
        public JobAlreadyQueuedException() { }

        public JobAlreadyQueuedException(string message) : base(message) { }
    }
}
