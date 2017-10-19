using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Singleton class using static functions.
/// </summary>
public class DebugLog
{
    private static StreamWriter All;
    private static StreamWriter Information;
    private static StreamWriter Warning;
    private static StreamWriter Critical;
    private static StreamWriter Debug;

    private static DebugLog Logger = null;

    public static bool LogInformationConsole = true;
    public static bool LogWarningConsole = true;
    public static bool LogCriticalConsole = true;
    public static bool LogDebugConsole = false;

    private DebugLog()
    {
        string directory = Directory.GetCurrentDirectory() + @"\Log";

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        All = new StreamWriter(directory + @"\all.log");
        Information = new StreamWriter(directory + @"\information.log");
        Warning = new StreamWriter(directory + @"\warning.log");
        Critical = new StreamWriter(directory + @"\critical.log");
        Debug = new StreamWriter(directory + @"\debug.log");

        All.AutoFlush = true;
        Information.AutoFlush = true;
        Warning.AutoFlush = true;
        Critical.AutoFlush = true;
        Debug.AutoFlush = true;

        All.WriteLine("'all.log' contains Information, Warning and Critical information but no Debug information.");
    }

    public static void Log(string log, LogLevel level)
    {
        if (Logger == null)
            Logger = new DebugLog();

        if (level == LogLevel.LOG_INFORMATION)
        {
            string output = string.Format("[{0}] Info: {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(), log);
            All.WriteLine(output);
            Information.WriteLine(output);
            if (LogInformationConsole) Console.WriteLine(output);
        }
        else if (level == LogLevel.LOG_WARNING)
        {
            string output = string.Format("[{0}] Warning: {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(), log);
            All.WriteLine(output);
            Warning.WriteLine(output);
            if (LogWarningConsole) Console.WriteLine(output);
        }
        else if (level == LogLevel.LOG_CRITICAL)
        {
            string output = string.Format("[{0}] Critical: {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(), log);
            All.WriteLine(output);
            Critical.WriteLine(output);
            if (LogCriticalConsole) Console.WriteLine(output);
        }
        else if (level == LogLevel.LOG_DEBUG)
        {
            string output = string.Format("[{0}] Debug: {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(), log);
            Debug.WriteLine(output);
            if (LogDebugConsole) Console.WriteLine(output);
        }
    }

    public static void LogInfo(string log)
    {
        Log(log, LogLevel.LOG_INFORMATION);
    }
    public static void LogWarning(string log)
    {
        Log(log, LogLevel.LOG_WARNING);
    }
    public static void LogCritical(string log)
    {
        Log(log, LogLevel.LOG_CRITICAL);
    }
    public static void LogDebug(string log)
    {
        Log(log, LogLevel.LOG_DEBUG);
    }
}
public enum LogLevel
{
    LOG_INFORMATION = 0,
    LOG_WARNING = 1,
    LOG_CRITICAL = 2,
    LOG_DEBUG = 3
};

