using System;
using System.IO;

namespace LoggerLib
{
  // NOTE: Intentionally naive implementation for learning exercise.
  // Students will iteratively improve this based on README prompts.
  // Current intentional limitations / TODOs:
  // - Not thread safe (race conditions possible when called from multiple threads)
  // - No IDisposable / resource management (file handle reopened each call)
  // - No log levels (Info/Warning/Error)
  // - No timestamp or formatting
  // - No configuration of file name or path
  // - No validation (null/empty message)
  // - No asynchronous logging
  // - No dependency injection friendliness
  // - No XML documentation (left as an exercise)
  public sealed class Logger
  {
    private static Logger _instance;
    private const string LogFileName = "log.txt";

    public static Logger Instance => _instance ??= new Logger();

    private Logger() { }

    public void Log(string message)
    {
      // Naive append: opens and closes file every call; no error handling.
      File.AppendAllText(LogFileName, message + Environment.NewLine);
    }
  }
}
