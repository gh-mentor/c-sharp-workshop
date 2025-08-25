Fix issues in Average Salary

This code appears to calculate the average salary of employees in a department, but it contains issues that could lead to incorrect results or runtime errors.

- Review the code on your own, then use Copilot to explain the code and identify potential problems.

- Refactor the code to fix the identified issues and improve its overall quality. Ensure the code adheres to SOLID design principles, including proper exception handling and adherence to C# coding conventions.

- Add XML documentation comments to all public methods and classes.

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