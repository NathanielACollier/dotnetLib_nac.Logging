using System;
using System.IO;
using nac.Logging.model;

namespace nac.Logging.Appenders;

public class RollingFile : LogAppender
{
    private static RollingFile me = new RollingFile();
    private model.LogLevel minimumLevel;
    private System.IO.FileInfo logFileInfo;
    private RollingFile(){ }

    public static void Setup(model.LogLevel minimumLevel=model.LogLevel.Debug, 
                        string filePath = @"logs/log.txt"
    ){
        me.minimumLevel = minimumLevel;
        me.logFileInfo = new System.IO.FileInfo(filePath);

        if( !System.IO.Directory.Exists(me.logFileInfo.DirectoryName)){
            System.IO.Directory.CreateDirectory(me.logFileInfo.DirectoryName);
        }
        Logger.Register(me);
    }


    public void HandleLog(LogMessage entry)
    {
        if( entry.Level < minimumLevel){
            return;
        }
        handleMovingContentsToBackupLocationIfNeeded(logFileInfo.FullName);

        using (FileStream fs = System.IO.File.Open(logFileInfo.FullName,
                                    mode: System.IO.FileMode.Append,
                                    access: System.IO.FileAccess.Write,
                                    share: System.IO.FileShare.ReadWrite | System.IO.FileShare.Delete)) {
            using( var writer = new StreamWriter(fs)){
                writer.WriteLine(entry);
            }
        }
    }

    private static void handleMovingContentsToBackupLocationIfNeeded(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            var modificationDate = System.IO.File.GetLastWriteTime(filePath);
            if (modificationDate.Date != DateTime.Now.Date)
            {
                // last modified on a different day.  Move old entries to another file
                // preserve that file
                var logFileInfo = new System.IO.FileInfo(filePath);
                string namewithoutExt = System.IO.Path.GetFileNameWithoutExtension(logFileInfo.Name);
                string ext = System.IO.Path.GetExtension(logFileInfo.Name);
                string newFileName = $"{namewithoutExt}{modificationDate:yyyyMMdd}{ext}";
                string newFilePath = System.IO.Path.Combine(
                    logFileInfo.DirectoryName,
                    newFileName
                );

                if( System.IO.File.Exists(newFilePath)){
                    // if the new file path exists, then append whatever we have onto that file?
                    var logContents = System.IO.File.ReadAllText(filePath);
                    System.IO.File.AppendAllText(newFilePath, logContents);
                    System.IO.File.Delete(filePath);
                }else {
                    // perform a move of the log.txt or whatever to the backup location
                    System.IO.File.Move(logFileInfo.FullName, newFilePath);
                }
            }
        }
    }

}