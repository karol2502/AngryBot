namespace AngryBot.Domain.Entities;

public class Disappointment
{
    public int Id { get; set; }
    public DateTime AddedAt { get; } = DateTime.UtcNow;
    public string Description { get; set; } = default!;
    public UserDisappointment UserDisappointment { get; set; } = default!;
    public User AddedBy { get; set; } = default!;
    public ulong AddedById { get; set; }

    public Disappointment()
    {        
    }

    public Disappointment(string description)
    {
        Description = description;
    }

    public Disappointment(string description, UserDisappointment userDisappointment, ulong addedById)
    {
        Description = description;
        UserDisappointment = userDisappointment;
        AddedById = addedById;
    }
}
