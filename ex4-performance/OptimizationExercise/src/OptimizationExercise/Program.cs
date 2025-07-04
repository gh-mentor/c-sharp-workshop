using System;

namespace OptimizationExercise
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Starting the optimization exercise...");

      // Initialize the problem model and solver
      var problemModel = new Models.ProblemModel();
      var solver = new Services.UnoptimizedSolver();

      // Here you would typically set up the problem model with input data and constraints
      // For demonstration purposes, we will use placeholder values
      problemModel.InputData = new int[] { 1, 2, 3, 4, 5 };
      problemModel.Constraints = "Some constraints";

      // Solve the problem using the unoptimized solver
      var result = solver.Solve(problemModel);

      // Output the result
      Console.WriteLine("Result of the unoptimized solution: " + result);
    }
  }
}