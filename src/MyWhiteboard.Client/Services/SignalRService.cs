using Microsoft.AspNetCore.SignalR.Client;
using MyWhiteboard.Shared;

namespace MyWhiteboard.Client.Services;

public class SignalRService : IAsyncDisposable
{
    private HubConnection? _hubConnection;
    private readonly string _hubUrl;
    private bool _isConnected;

    public event Action<DrawEventDto>? OnDrawEvent;

    public SignalRService(IConfiguration configuration)
    {
        _hubUrl = configuration["SignalR:HubUrl"] ?? "https://localhost:7001/whiteboardhub";
    }

    public async Task StartAsync()
    {
        if (_hubConnection == null)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<DrawEventDto>("ReceiveDrawEvent", (drawEvent) =>
            {
                OnDrawEvent?.Invoke(drawEvent);
            });

            await _hubConnection.StartAsync();
            _isConnected = true;
        }
    }

    public async Task JoinBoardAsync(Guid boardId)
    {
        if (_isConnected && _hubConnection != null)
        {
            await _hubConnection.InvokeAsync("JoinBoard", boardId);
        }
    }

    public async Task LeaveBoardAsync(Guid boardId)
    {
        if (_isConnected && _hubConnection != null)
        {
            await _hubConnection.InvokeAsync("LeaveBoard", boardId);
        }
    }

    public async Task SendDrawEventAsync(DrawEventDto drawEvent)
    {
        if (_isConnected && _hubConnection != null)
        {
            await _hubConnection.InvokeAsync("DrawEvent", drawEvent);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection != null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
