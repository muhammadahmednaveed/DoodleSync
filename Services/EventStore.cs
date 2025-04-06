using System;
using System.Collections.Generic;
using DoodleSync.Models;

namespace DoodleSync.Services
{
    public class EventStore
    {
        private readonly List<DrawingEvent> _events = new();
        public event Action<DrawingEvent>? OnEventAdded;

        public IReadOnlyList<DrawingEvent> Events => _events.AsReadOnly();

        public void AddEvent(DrawingEvent drawingEvent)
        {
            _events.Add(drawingEvent);
            OnEventAdded?.Invoke(drawingEvent);
        }

        public void Clear()
        {
            _events.Clear();
            AddEvent(new ClearCanvasEvent());
        }
    }
}
