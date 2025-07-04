Advanced Byte Manipulation with Modern C#

In this exercise, you will design and implement a C# class that performs advanced byte manipulation using modern .NET features. The goal is to explore advanced C# practices, including LINQ, async/await, and dependency injection, while adhering to SOLID design principles.

### Requirements

- Create a class `AdvancedByteProcessor` with the following features:
  - A private `List<byte>` to store byte data, initialized via the constructor.

- Implement the following methods:
  1. `FilterBytesAsync`: 
     - Accepts a `Func<byte, bool>` predicate.
     - Uses LINQ to filter the bytes asynchronously based on the predicate and returns a new `List<byte>`.

  2. `TransformBytesAsync`:
     - Accepts a `Func<byte, Task<byte>>` delegate.
     - Applies the transformation asynchronously to each byte in the list and returns a new `List<byte>`.

  3. `AggregateBytesAsync`:
     - Accepts a `Func<byte, byte, Task<byte>>` delegate and an initial seed value.
     - Aggregates the bytes asynchronously using the provided delegate and returns the final result.

- Use dependency injection to inject custom transformation or filtering strategies where applicable.

- Ensure the class adheres to the single-responsibility principle and is open for extension but closed for modification.

### Testing

- Implement a comprehensive test suite using NUnit to validate the functionality of the `AdvancedByteProcessor` class.
- Write unit tests to cover various scenarios, including edge cases and error handling:
  - Initialization with valid and invalid byte collections.
  - Filtering with different predicates.
  - Transformation with various asynchronous functions.
  - Aggregation with different strategies.
  - Edge cases such as empty collections and maximum size.

### Modern Practices

- Use LINQ for concise and readable data transformations.
- Use `async`/`await` for asynchronous programming.
- Use dependency injection to decouple logic from the `AdvancedByteProcessor` class.
- Document the code with XML comments and provide a README file explaining the design choices, usage, and testing instructions.

