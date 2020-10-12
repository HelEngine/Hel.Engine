namespace Hel.Engine
{
    public delegate void OnChangeEvent<in T>(T obj);
    
    internal interface IEventChange<T>
    {
        /// <summary>
        /// Adds a method to the OnChange delegate.
        /// </summary>
        /// <param name="method">A void returning method with no parameters to be called on change</param>
        void AddChangeEvent(OnChangeEvent<T> method);
        /// <summary>
        /// Removes a method from the OnChange delegate.
        /// </summary>
        /// <param name="method">A void returning method with no parameters to be called on change.</param>
        void RemoveChangeEvent(OnChangeEvent<T> method);
        /// <summary>
        /// Reset the OnChange delegate to null
        /// </summary>
        void ResetEvent();
    }

    public abstract class EventChange<T> : IEventChange<T>
    {
        protected OnChangeEvent<T> OnChange;

        /// <summary>
        /// Calling this will run all OnChange events. Context should generally be the object calling (this)
        /// doing so will get the object type to the objects class type. Methods being called should use the class type for param.
        /// </summary>
        /// <typeparam name="T">Class type of the object. Auto set when using This keyword</typeparam>
        /// <param name="context">Object to be passed along.</param>
        protected void OnChangeEvents(T context) => OnChange?.Invoke(context);
        public virtual void AddChangeEvent(OnChangeEvent<T> method) => OnChange += method;
        public virtual void RemoveChangeEvent(OnChangeEvent<T> method) => OnChange -= method;
        public virtual void ResetEvent() => OnChange = null;
    }
}
