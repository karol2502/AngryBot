using Discord;
using Microsoft.Extensions.Logging;

namespace AngryBot.Shared.Logging;
public static class LogHelper
{
    public async static Task OnLogAsync(ILogger logger, LogMessage message)
    {
        switch (message.Severity)
        {
            case LogSeverity.Debug:
            case LogSeverity.Verbose:
            case LogSeverity.Info:
                logger.LogInformation(message.ToString());
                break;

            case LogSeverity.Warning:
                logger.LogWarning(message.ToString());
                break;

            case LogSeverity.Error:
                logger.LogError(message.ToString());
                break;

            case LogSeverity.Critical:
                logger.LogCritical(message.ToString());
                break;
        }
        await Task.CompletedTask;
    }
}
