using AngryBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngryBot.Persistence;

internal sealed class AngryBotDbContext : DbContext
{
    public DbSet<Guild> Guilds { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Disappointment> Disappointments { get; set; }
    public DbSet<UserDisappointment> UserDisappointments { get; set; }
    public DbSet<MessageResponse> MessageResponses { get; set; }

    public AngryBotDbContext(DbContextOptions<AngryBotDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AngryBotDbContext).Assembly);
    }
}
