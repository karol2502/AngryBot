using AngryBot.Domain.Common.Enums;

namespace AngryBot.Domain.Entities;

public class MessageResponse
{
    public int Id { get; set; }
    public string Message { get; set; } = default!;
    public string Response { get; set; } = default!;
    public ResponseType ResponseType { get; set; }
    public Guild Guild { get; set; } = default!;
    public ulong GuildId { get; set; }
    public DateTime AddedAt { get; } = DateTime.UtcNow;
    public User AddedBy { get; set; } = default!;
    public ulong AddedById { get; set; }

    public MessageResponse()
    {        
    }

    public MessageResponse(string message, string response, ResponseType responseType, ulong guildId, ulong addedById)
    {
        Message = message;
        Response = response;
        ResponseType = responseType;
        GuildId = guildId;
        AddedById = addedById;
    }
}
