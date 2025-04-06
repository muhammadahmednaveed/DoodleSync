using System;

namespace DoodleSync.Models
{
    public abstract record DrawingEvent
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }

    public record StartDrawingEvent : DrawingEvent
    {
        public double X { get; init; }
        public double Y { get; init; }
    }

    public record DrawToEvent : DrawingEvent
    {
        public double X { get; init; }
        public double Y { get; init; }
    }

    public record EndDrawingEvent : DrawingEvent
    {
        public double X { get; init; }
        public double Y { get; init; }
    }

    public record ClearCanvasEvent : DrawingEvent { }
}
