using System;
using System.Collections.Generic;
using System.Text;

namespace nac.Logging.model;

public class LogMessage
{
    public model.LogLevel Level { get; set; }
    public string Message { get; set; }
    public string CallingMemberName { get; set; }
    public DateTime EventDate { get; }
    public Type CallingClassType { get; internal set; }
    public string CallingSourceFilePath { get; internal set; }
    public int CallingSourceLineNumber { get; internal set; }

    public LogMessage()
    {
        this.EventDate = DateTime.Now;
    }

    public override string ToString()
    {
        string levelText = this.Level.ToString().ToUpper();
        return $"[{EventDate:yyyy-MM-dd hh:mm:ss tt}] - {levelText} - {CallingClassType.FullName}.{CallingMemberName} - {Message}";
    }
}
