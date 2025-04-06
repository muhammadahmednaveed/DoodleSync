using Microsoft.AspNetCore.SignalR;
using MyWhiteboard.Shared;

namespace MyWhiteboard.Hubs;

public class WhiteboardHub : Hub
{
    private readonly IEventStore _eventStore;

    public WhiteboardHub(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task JoinBoard(Guid boardId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, boardId.ToString());
    }

    public async Task LeaveBoard(Guid boardId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, boardId.ToString());
    }

    public async Task DrawEvent(DrawEventDto drawEvent)
    {
        await _eventStore.SaveEventAsync(drawEvent);
        await Clients.Group(drawEvent.BoardId.ToString()).SendAsync("ReceiveDrawEvent", drawEvent);
    }
}

public interface IEventStore
{
    Task SaveEventAsync(DrawEventDto drawEvent);
}
