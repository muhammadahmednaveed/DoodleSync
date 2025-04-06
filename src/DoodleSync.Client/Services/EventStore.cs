using System;
using System.Collections.Generic;
using DoodleSync.Client.Models;

namespace DoodleSync.Client.Services
{
    public class EventStore
    {
        private readonly List<DrawingEvent> _events = new();
        public event Action<DrawingEvent>? OnEventAdded;

        public IReadOnlyList<DrawingEvent> Events => _events;

        public void AddEvent(DrawingEvent evt)
        {
            _events.Add(evt);
            OnEventAdded?.Invoke(evt);
        }

        public void Clear()
        {
            _events.Clear();
            OnEventAdded?.Invoke(new ClearCanvasEvent());
        }
    }
}
