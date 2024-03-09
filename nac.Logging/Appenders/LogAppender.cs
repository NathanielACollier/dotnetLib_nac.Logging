namespace nac.Logging.Appenders;

public interface LogAppender
{
    void HandleLog(model.LogMessage entry);
}
