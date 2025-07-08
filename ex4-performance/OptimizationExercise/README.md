# Optimization Exercise

## Overview
This project models a complex problem that requires optimization. The primary focus is on demonstrating an unoptimized solution to provide an exercise for students to understand the importance of optimization in algorithm design.

## Project Structure
The project is organized into the following directories:

- **src**: Contains the main application code.
  - **OptimizationExercise**: The main project folder.
    - `OptimizationExercise.csproj`: Project file defining settings and dependencies.
    - `Program.cs`: Entry point of the application.
    - **Models**: Contains the data models.
      - `ProblemModel.cs`: Represents the complex problem being modeled.
    - **Services**: Contains the logic for solving the problem.
      - `UnoptimizedSolver.cs`: Implements a naive algorithm to solve the problem.
    - **Utils**: Contains utility functions.
      - `HelperFunctions.cs`: Provides helper methods for validation and formatting.

- **tests**: Contains the test suite for the application.
  - **OptimizationExercise.Tests**: The test project folder.
    - `OptimizationExercise.Tests.csproj`: Project file for the test suite.
    - `UnoptimizedSolverTests.cs`: Unit tests for the `UnoptimizedSolver` class.

## Running the Application
To run the application, follow these steps:

1. Open a terminal and navigate to the `src/OptimizationExercise` directory.
2. Build the project using the command:
   ```
   dotnet build
   ```
3. Run the application using the command:
   ```
   dotnet run
   ```

## Running Tests
To run the tests, follow these steps:

1. Open a terminal and navigate to the `tests/OptimizationExercise.Tests` directory.
2. Build the test project using the command:
   ```
   dotnet build
   ```
3. Run the tests using the command:
   ```
   dotnet test
   ```

