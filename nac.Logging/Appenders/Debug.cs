namespace nac.Logging.Appenders;

public class Debug : LogAppender
{
    private static Debug me = new Debug();
    private model.LogLevel minimumLevel;

    private Debug(){ }

    public static void Setup(model.LogLevel minimumLevel = model.LogLevel.Debug){
        me.minimumLevel = minimumLevel;
        Logger.Register(me);
    }

    public void HandleLog(model.LogMessage entry)
    {
        if( entry.Level < minimumLevel){
            return;
        }

        System.Diagnostics.Debug.WriteLine(entry);
    }
}