Byte Transformation with C#

In this exercise you will design and implement a C# class that performs byte transformation. The class should allow for both fixed and custom transformation strategies on a collection of bytes. The goal is to create a flexible and reusable component that can be easily extended for future requirements.

1) Class Design Instructions
- Create a class `ByteFactory` with the following features:
  - A private `List<byte>` to store byte data, initialized via the constructor.
  - Ensure the list size does not exceed 100 bytes. Store a status flag and provide a method to check validity.
  - Use 'm_' prefix for private and protected member variables.

- Implement a method `GetByteCount` that returns the current number of bytes stored in the list.

- Implement a method `Transform` that takes a `byte` mask as an argument  
  - For each byte in the list, apply a bitwise EXCLUSIVE OR (`^`) with the mask, and return a new `List<byte>` containing the resulting bytes.  
  - For example, if the mask is `0x55` and a byte is `0x99`, the result should be `0xCC`.

- Overload `Transform` to accept a second argument: a delegate, function, or lambda expression that defines a custom transformation strategy.  
  - The function should take a single `byte` as input and return the transformed `byte`.  
  - Apply this function to each byte in the list, and return a new `List<byte>` containing the transformed bytes.
  - For example, you could pass a lambda that inverts each byte: `b => (byte)~b`.
  - Ensure the function is flexible enough to allow for different transformation strategies.

- Implement a unit test suite to validate the functionality of the `ByteFactory` class.

2) Testing Instructions
- Write unit tests to cover various scenarios, including edge cases and error handling:
  - Initialization with valid and invalid byte collections.
  - Transformation with different masks and custom functions.
  - Edge cases such as empty collections and maximum size.

3) Documentation Instructions
- Document the code with comments and provide a README file explaining the design choices, usage, and testing instructions.

Note: The folder 'sample' contains a bit swap implementation in C. It is provided as an example of using Copilot inline to perform complex bit manipulation tasks.