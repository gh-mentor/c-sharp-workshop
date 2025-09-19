The code is a simple producer-consumer example using `Task` and `TaskCompletionSource`. 

The goal is to gain experience using Copilot Chat features effectively. Use either Ask or Agent mode if desired.

- Use Copilot to generate a .gitignore file for a C# project.

- Ask Copilot to explain the existing code and the purpose of using `Task` and `TaskCompletionSource`.

- Use Copilot to separate implementation into appropriate classes and interfaces to follow SOLID principles. The classes and interfaces should be in namespace 'ProduceConsumer'.Each type will be in a separate file.

- Use Copilot Chat to suggest unit tests for the code using a testing framework such as xUnit, NUnit, or MSTest.

- Build and run the code to ensure it works as expected.

## Suggested Copilot Prompts

Use these prompts (copy/paste or adapt)  using Copilot Chat (Ask or Agent modes). They are grouped by objective. Iterate: first ask for an explanation, then refine or request alternatives.

### 1. Git & Project Setup
- "Generate a .gitignore suitable for a .NET 9 C# console project with bin/ obj/ coverage output ignored." 
- "Review this .gitignore and tell me if anything is missing for typical C# tooling (Rider, VS, VS Code)." 

### 2. Understanding the Existing Code
- "Explain the purpose of `TaskCompletionSource<string>` in this Program.cs producer-consumer example." 
- "What problem does `TaskCompletionSource` solve compared to returning a `Task.Delay(...).ContinueWith(...)` chain?" 
- "Walk through the execution order of `producer.ProduceAsync()` and `consumer.ConsumeAsync()` and where the await suspension happens." 
- "List potential race conditions or misuse scenarios with TaskCompletionSource in this code." 

### 3. Applying SOLID & Refactoring
- "Refactor this producer-consumer code into separate files with namespace `ProduceConsumer` following SOLID. Propose interfaces first (no implementation yet)." 
- "Given the proposed interfaces, generate clean implementations without changing behavior." 
- "Ensure Dependency Inversion: the consumer should depend on an abstraction for receiving messages. Suggest modifications." 
- "Add XML documentation comments to each public interface and class in the refactored version." 
- "Suggest how to make the producer cancellable via `CancellationToken`." 

### 4. Interface & Class Design Deep Dives
- "Propose an `IMessageProducer` and `IMessageConsumer` interface with async methods and describe their responsibilities in one sentence each." 
- "Suggest how to extend the design to support multiple consumers waiting on the same message.” 
- "Show two alternative designs: (a) TaskCompletionSource-based, (b) Channel-based using System.Threading.Channels." 

### 5. Unit Testing Guidance
- "Suggest xUnit tests for the refactored producer-consumer pattern: one for successful message flow, one for timeout, one for cancellation." 
- "Write an xUnit test that asserts the consumer prints the producer's message (capture console output)." 
- "Generate NUnit equivalents of the earlier xUnit tests." 
- "Show how to mock a producer to simulate delayed completion without real Task.Delay." 
- "Provide a test that verifies no deadlock when awaiting both tasks with Task.WhenAll." 

### 6. Build, Run & Validation
- "Give the exact dotnet CLI commands to build and run this console app in Release mode." 
- "Suggest a watch mode command to rebuild rapidly while refactoring." 
- "How can I add simple logging (Microsoft.Extensions.Logging) with minimal setup in a console app?" 

### 7. Enhancements & Extensions
- "Enhance the design to support producing multiple messages; outline options (queue, Channel, IAsyncEnumerable)." 
- "Refactor to use `Channel<string>` instead of `TaskCompletionSource`. Provide code." 
- "Demonstrate an async iterator (`IAsyncEnumerable<string>`) version of the producer with a consumer that enumerates." 
- "Add cancellation support to both producer and consumer and show usage in Main." 
- "Introduce a timeout on the consumer side and show how to surface a friendly error message." 

### 8. Troubleshooting & Edge Cases
- "What happens if `SetResult` is called twice on the same TaskCompletionSource? Show how to guard against it." 
- "Show how to handle producer exceptions so the consumer displays an error path." 
- "Explain why `Task.Run` is not required here and when it would be appropriate." 

### 9. Performance & Diagnostics
- "Suggest lightweight instrumentation (timing) for the produce path without adding heavy dependencies." 
- "How to add EventSource or Activity tracing for this pattern?" 

### 10. Teaching / Meta Prompts
- "Generate a short quiz (5 questions) to test understanding of Task vs TaskCompletionSource using this example." 
- "Provide a bullet summary comparing TaskCompletionSource, Channel, and BlockingCollection for producer-consumer scenarios." 

### 11. Prompt Refinement Examples
- Initial: "Refactor code for SOLID." → Refined: "Refactor into `IMessageProducer` / `IMessageConsumer` interfaces in namespace `ProduceConsumer`, each type in its own file, no behavior changes, keep async methods, return Task." 
- Initial: "Add tests." → Refined: "Create 3 xUnit tests: success message flow (assert output), cancellation before completion (assert TaskCanceledException), and timeout using CancellationTokenSource with 1s timeout on a 5s delay." 

### 12. Stretch / Advanced Topics
- "Show how to adapt this to a background service in a generic host (IHostedService)." 
- "Convert this into a minimal API endpoint that triggers the producer and streams status." 
- "Demonstrate Polly retry logic if the producer sometimes fails before setting the result." 

### 13. Using Agent Mode (Optional)
- "Act as an agent: create interfaces and move each type to a new file under namespace ProduceConsumer, then show a diff." 
- "Act as an agent: generate xUnit test project named ex1-enhance.Tests and add tests for success and cancellation." 

---
Tip: Iterate after any answer, ask: "Suggest improvements" or "Provide an alternative approach." This builds prompt refinement habits.






