using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWhiteboard.Api.Data;
using MyWhiteboard.Shared;

namespace MyWhiteboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardsController : ControllerBase
{
    private readonly WhiteboardDbContext _context;

    public BoardsController(WhiteboardDbContext context)
    {
        _context = context;
    }

    [HttpGet("{boardId}/events")]
    public async Task<ActionResult<IEnumerable<DrawEventDto>>> GetBoardEvents(Guid boardId)
    {
        var events = await _context.DrawEvents
            .Where(e => e.BoardId == boardId)
            .OrderBy(e => e.Timestamp)
            .ToListAsync();

        return Ok(events);
    }
}
