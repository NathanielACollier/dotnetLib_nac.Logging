using nac.Logging.model;
using System;
using System.Runtime.CompilerServices;

namespace nac.Logging;

public class Logger
{
    public static event EventHandler<LogMessage> OnNewMessage;

    private string Caller_className;

    public Logger()
    {
        // get calling assembly
        var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
        var _class_to_log = mth.ReflectedType;

        this.Caller_className = _class_to_log.FullName;
    }

    public void Info(string message, [CallerMemberName] string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "INFO",
            Message = message,
            CallingClassName = Caller_className,
            CallingMemberName = callerMemberName
        });
    }

    public void Debug(string message, [CallerMemberName] string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "DEBUG",
            Message = message,
            CallingClassName = Caller_className,
            CallingMemberName = callerMemberName
        });
    }

    public void Warn(string message, [CallerMemberName] string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "WARN",
            Message = message,
            CallingClassName = Caller_className,
            CallingMemberName = callerMemberName
        });
    }

    public void Error(string message, [CallerMemberName] string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "ERROR",
            Message = message,
            CallingClassName = Caller_className,
            CallingMemberName = callerMemberName
        });
    }
}
