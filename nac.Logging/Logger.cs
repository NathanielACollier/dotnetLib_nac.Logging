using nac.Logging.model;
using System;
using System.Runtime.CompilerServices;

namespace nac.Logging;

public class Logger
{
    public static event EventHandler<LogMessage> OnNewMessage;

    private Type Caller_classType;

    public Logger()
    {
        // get calling assembly
        var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
        var _class_to_log = mth.ReflectedType;

        this.Caller_classType = _class_to_log;
    }

    public void Info(string message, [CallerMemberName] string callerMemberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "INFO",
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }

    public void Debug(string message, [CallerMemberName] string callerMemberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "DEBUG",
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }

    public void Warn(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "WARN",
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }

    public void Error(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "ERROR",
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }

    public void Fatal(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "FATAL",
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }

}
