namespace MyWhiteboard.Shared;

public record DrawEventDto
{
    public Guid Id { get; init; }
    public string Type { get; init; } = string.Empty;
    public double X { get; init; }
    public double Y { get; init; }
    public string Color { get; init; } = "#000000";
    public double LineWidth { get; init; } = 2;
    public DateTime Timestamp { get; init; }
    public string UserId { get; init; } = string.Empty;
    public Guid BoardId { get; init; }
}
