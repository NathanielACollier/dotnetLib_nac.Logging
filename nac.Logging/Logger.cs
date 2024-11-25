using nac.Logging.model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace nac.Logging;

public class Logger
{
    private static List<Appenders.LogAppender> appenders = new List<Appenders.LogAppender>();
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
        CreateLogEntry(new LogMessage
        {
            Level = model.LogLevel.Info,
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
        CreateLogEntry(new LogMessage
        {
            Level = model.LogLevel.Debug,
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }

    public void Warn(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        CreateLogEntry(new LogMessage
        {
            Level = model.LogLevel.Warn,
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }

    public void Error(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        CreateLogEntry(new LogMessage
        {
            Level = model.LogLevel.Error,
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }

    public void Fatal(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        CreateLogEntry( new LogMessage
        {
            Level = model.LogLevel.Fatal,
            Message = message,
            CallingClassType = Caller_classType,
            CallingMemberName = callerMemberName,
            CallingSourceFilePath = sourceFilePath,
            CallingSourceLineNumber = sourceLineNumber
        });
    }
    
    internal static model.LogLevel getLogLevelFromText(string logLevelText)
    {
        return logLevelText.Trim().ToLower() switch
        {
            "info" => model.LogLevel.Info,
            "debug" => model.LogLevel.Debug,
            "warn" => model.LogLevel.Warn,
            "error" => model.LogLevel.Error,
            "fatal" => model.LogLevel.Fatal,
            _ => throw new Exception($"Unknown log level [{logLevelText}]")
        };
    }

    public static void Register(Appenders.LogAppender __appender){
        if( appenders.Contains(__appender)){
            return;
        }

        appenders.Add(__appender);
    }
    
    public static void CreateLogEntry(model.LogMessage logMessage){


        var appendersAtCallTime = new List<Appenders.LogAppender>(appenders); // make a copy to prevent collection was modified in foreach loop errors
        foreach( var _a in appendersAtCallTime ){
            _a.HandleLog(logMessage);
        }
    }


}
