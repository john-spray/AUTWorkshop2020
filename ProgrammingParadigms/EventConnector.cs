﻿using System;
using System.Collections.Generic;

namespace ProgrammingParadigms
{
    /// <summary>
    /// Events or observer pattern (publish/subscribe without data.
    /// Can be asynchronous or synchronous
    /// No data, can be bidirectional.
    /// Analogous to Reactive Extensions without the duality with Iteration - the flow only uses hot observables, and never completes.
    /// </summary>
    public interface IEvent
    {
        void Execute();
    }

    public delegate void CallBackDelegate();

    /// <summary>
    /// A reversed IEvent. The IEvent pushes event to the destination whereas the IEventB pulls data from source.
    /// However, the destination will be notified when event happens at source.
    /// </summary>
    public interface IEventB
    {
        event CallBackDelegate EventHappened;
    }

    /// <summary>
    /// It fans out IEvent by creating a list and assign the event to the element in the list.
    /// Moreover, any IEvent and IEventB can be transferred bidirectionally.
    /// </summary>
    public class EventConnector : IEvent, IEventB
    {
        // Properties
        public string InstanceName;

        // outputs
        private List<IEvent> fanoutList = new List<IEvent>();

        /// <summary>
        /// Fans out an IEvent to multiple IEvents, or connect IEvent and IEventB
        /// </summary>
        public EventConnector() { }

        // IEvent implementation ------------------------------------
        void IEvent.Execute()
        {
            foreach (var fanout in fanoutList) fanout.Execute();
            EventHappened?.Invoke();
        }

        // IEventB implementation --------------------------------------
        public event CallBackDelegate EventHappened;
    }
}
