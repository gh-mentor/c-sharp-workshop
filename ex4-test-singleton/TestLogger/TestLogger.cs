using LoggerLib;
using NUnit.Framework;
using System.IO;

namespace TestLogger
{
    /// <summary>
    /// Tests for Logger log file creation.
    /// </summary>
    [TestFixture]
    public class LoggerFileCreationTests
    {
        private const string LogFileName = "log.txt";

        [SetUp]
        public void SetUp()
        {
            // Delete log file before each test to ensure clean state
            if (File.Exists(LogFileName))
            {
                File.Delete(LogFileName);
            }
        }

        /// <summary>
        /// Verifies that the log file is created when logging a message.
        /// </summary>
        [Test]
        public void LogFile_IsCreated_WhenLoggingMessage()
        {
            // Act
            Logger.Instance.Log("Test message");
            Logger.Instance.Dispose();

            // Assert
            Assert.That(File.Exists(LogFileName), Is.True, "Log file should be created after logging a message.");
        }
    }
}
