using Microsoft.EntityFrameworkCore;
using DoodleSync.Shared;

namespace DoodleSync.Api.Data;

public class WhiteboardDbContext : DbContext
{
    public WhiteboardDbContext(DbContextOptions<WhiteboardDbContext> options)
        : base(options)
    {
    }

    public DbSet<DrawEventDto> DrawEvents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DrawEventDto>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<DrawEventDto>()
            .Property(e => e.Type)
            .IsRequired();

        modelBuilder.Entity<DrawEventDto>()
            .Property(e => e.UserId)
            .IsRequired();
    }
}
