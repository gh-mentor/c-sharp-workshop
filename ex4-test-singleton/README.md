Logger.cs contains the implementation of a Logger class that writes log messages to a text file. The Logger class is designed to be a singleton, ensuring that only one instance of the logger exists throughout the application. This is achieved by making the constructor private and providing a static method to access the instance.

TestLogger.cs contains some unit tests for the Logger class. Consider adding at least the following tests:

1) Test that the Logger is a singleton. 

2) Test the logging functionality. Use the following steps to verify the log messages:
-- Get the Logger instance
-- Log a test message
-- Verify that the log message was written to the file

3) Test the Logger's thread safety by logging from multiple threads simultaneously.
-- Create a number of threads that log messages
-- Verify that all messages are logged

4) Test logging multiple messages in sequence