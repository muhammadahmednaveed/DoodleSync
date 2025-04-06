using System;

namespace DoodleSync.Client.Models
{
    public abstract record DrawingEvent
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
        public double X { get; init; }
        public double Y { get; init; }
    }

    public record StartDrawingEvent : DrawingEvent
    {
    }

    public record DrawToEvent : DrawingEvent
    {
    }

    public record EndDrawingEvent : DrawingEvent
    {
    }

    public record ClearCanvasEvent : DrawingEvent { }
}
