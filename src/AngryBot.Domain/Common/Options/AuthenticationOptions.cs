namespace AngryBot.Domain.Common.Options;

public class AuthenticationOptions
{
    public const string Authentication = "Authentication";

    public string ApiToken { get; set; } = default!;
}
