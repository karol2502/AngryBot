namespace AngryBot.Domain.Entities;

public class User
{
    public ulong Id { get; set; }
    public string Username { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
