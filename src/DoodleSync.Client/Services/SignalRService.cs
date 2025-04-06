using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace DoodleSync.Client.Services;

public interface ISignalRService
{
    Task StartAsync();
    Task StopAsync();
    Task SendMessageAsync(string message);
    event Action<string> OnMessageReceived;
}

public class SignalRService : ISignalRService, IAsyncDisposable
{
    private HubConnection? _hubConnection;
    private readonly ILogger<SignalRService> _logger;
    private bool _isConnected;

    public event Action<string>? OnMessageReceived;

    public SignalRService(ILogger<SignalRService> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync()
    {
        if (_isConnected) return;

        try
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://your-api-url/hubs/doodle") // Replace with your actual SignalR hub URL
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                OnMessageReceived?.Invoke(message);
            });

            await _hubConnection.StartAsync();
            _isConnected = true;
            _logger.LogInformation("SignalR Connection started");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting SignalR connection");
            throw;
        }
    }

    public async Task StopAsync()
    {
        if (!_isConnected || _hubConnection == null) return;

        try
        {
            await _hubConnection.StopAsync();
            _isConnected = false;
            _logger.LogInformation("SignalR Connection stopped");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error stopping SignalR connection");
            throw;
        }
    }

    public async Task SendMessageAsync(string message)
    {
        if (!_isConnected || _hubConnection == null)
        {
            throw new InvalidOperationException("SignalR connection is not started");
        }

        try
        {
            await _hubConnection.InvokeAsync("SendMessage", message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending message");
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection != null)
        {
            await StopAsync();
            await _hubConnection.DisposeAsync();
        }
    }
} 