namespace AngryBot.Domain.Entities;

public class UserDisappointment
{
    public int Id { get; set; }
    public User User { get; set; } = default!;
    public ulong UserId { get; set; }
    public int Counter { get; set; } = 0;
    public List<Disappointment> Disappointments { get; set; } = [];

    public UserDisappointment()
    {        
    }

    public UserDisappointment(ulong userId)
    {
        UserId = userId;
    }
}
