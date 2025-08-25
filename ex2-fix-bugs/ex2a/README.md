Fix Bugs in Average Salary

This code appears to calculate the average salary of employees in a department, but it contains multiple issues that could lead to incorrect results or runtime errors.

- Review the code on your own and try to identify the issues.
- Convert the code to a .NET project.
- Ensure the code follows .NET best practices, including proper exception handling and adherence to C# coding conventions.

- Optional: Use Copilot to help you create unit tests for the code using MSTest or xUnit, then run the tests to see if they pass or fail.

Here’s a recommended folder structure for your .NET project with a separate project for NUnit tests:

```
ex2a
├── AverageSalaryProject
│   ├── SalaryCalculator.cs
│   ├── Employee.cs
│   └── Program.cs
├── AverageSalaryProject.Tests
│   ├── AverageSalaryTests.cs

```