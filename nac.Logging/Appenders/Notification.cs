using System;
using System.Collections.Generic;
using System.Linq;

namespace nac.Logging.Appenders;

public class Notification : LogAppender
{
    private static Notification me = new Notification();
    private static List<Action<model.LogMessage>> notificationMessageReceiversList = new();
    private model.LogLevel minimumLevel;

    private Notification(){}
    
    public static void Setup( Action<model.LogMessage> onNewMessage,
        model.LogLevel minimumLevel = model.LogLevel.Debug){

        if (!notificationMessageReceiversList.Contains(onNewMessage))
        {
            notificationMessageReceiversList.Add(onNewMessage);
        }
        
        me.minimumLevel = minimumLevel;
        Logger.Register(me);
    }

    public void HandleLog(model.LogMessage entry)
    {
        if (entry.Level < minimumLevel)
        {
            return;
        }

        // create a copy so we don't deal with modifications in foreach
        var receiversAsOfNow = notificationMessageReceiversList.ToList();

        foreach (var receiver in receiversAsOfNow)
        {
            receiver.Invoke(entry);
        }
    }
}