using System;
using System.IO;
using System.Text;
using System.Threading;

namespace LoggerLib
{
  /// <summary>
  /// Thread-safe singleton logger that writes messages to a log file.
  /// </summary>
  public sealed class Logger : IDisposable
  {
    private static readonly Lazy<Logger> _instance = new(() => new Logger());
    private static readonly object _lock = new();
    private readonly StreamWriter _writer;
    private bool _disposed;
    private const string LogFileName = "log.txt";

    /// <summary>
    /// Gets the singleton instance of the Logger.
    /// </summary>
    public static Logger Instance => _instance.Value;

    private Logger()
    {
      _writer = new StreamWriter(LogFileName, append: true, Encoding.UTF8)
      {
        AutoFlush = true
      };
    }

    /// <summary>
    /// Logs a message to the log file in a thread-safe manner.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Log(string message)
    {
      if (_disposed) throw new ObjectDisposedException(nameof(Logger));
      lock (_lock)
      {
        _writer.WriteLine(message);
      }
    }

    /// <summary>
    /// Disposes the logger and closes the log file.
    /// </summary>
    void IDisposable.Dispose()
    {
      Dispose();
    }

    public void Dispose()
    {
      if (_disposed) return;
      lock (_lock)
      {
        _writer.Dispose();
        _disposed = true;
      }
    }
  }
}
