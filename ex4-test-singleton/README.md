## Singleton Logger: Advanced Copilot Course

Logger.cs implements a singleton Logger that writes log messages to a text file. Only one instance exists in the application, enforced by a private constructor and a static accessor.

TestLogger.cs contains unit tests for the Logger. You should add and improve tests as described below.

Tests to implement:

1. Test that the Logger is a singleton.
2. Test logging functionality:
	- Get the Logger instance
	- Log a test message
	- Verify the message was written to the file
3. Test Logger thread safety:
	- Log from multiple threads
	- Verify all messages are logged
4. Test logging multiple messages in sequence

Suggested Copilot prompts (in order):

1. How do I verify that Logger.Instance always returns the same object?
2. How do I check that a log message is written to the file?
3. How can I test logger thread safety with multiple threads?
4. How do I test logging several messages in sequence?
5. How can I add log levels (Info, Warning, Error) to the logger?
6. How do I make the logger configurable (file name, format)?
7. How can I ensure proper disposal of resources and handle exceptions?
8. How can I extend the logger for dependency injection?
9. What are best practices for handling cross-cutting concerns in .NET?
10. How can I add support for asynchronous logging?

Enhancements for cross-cutting concerns:

- Ensure proper resource management (e.g., implement IDisposable)
- Add error handling for file operations
- Allow configuration of log file path and format
- Support log levels and extensibility
- Maintain thread safety for all methods
- Refactor for testability and dependency injection
- Consider asynchronous logging for performance