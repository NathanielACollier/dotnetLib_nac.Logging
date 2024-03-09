using System;

namespace nac.Logging.Appenders;

public class ColoredConsole : LogAppender
{
    private static ColoredConsole me = new ColoredConsole();
    private model.LogLevel minimumLevel;
    private ColoredConsole(){ }

    public static void Setup(model.LogLevel minimumLevel = model.LogLevel.Debug){

        me.minimumLevel = minimumLevel;
        Logger.Register(me);
    }

    public void HandleLog(model.LogMessage entry)
    {
        if (entry.Level < minimumLevel)
        {
            return;
        }

        switch (entry.Level){
            case model.LogLevel.Debug:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case model.LogLevel.Info:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case model.LogLevel.Warn:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case model.LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case model.LogLevel.Fatal:
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
        }

        Console.WriteLine(entry);
        Console.ResetColor();
    }

}