// UnoptimizedSolver.cs
using System;
using System.Collections.Generic;
using OptimizationExercise.Models;

namespace OptimizationExercise.Services
{
  /// <summary>
  /// Represents an unoptimized solver for the complex problem.
  /// </summary>
  public class UnoptimizedSolver
  {
    /// <summary>
    /// Solves the given problem using a naive approach.
    /// This method is intentionally unoptimized for educational purposes.
    /// </summary>
    /// <param name="problem">The problem to solve.</param>
    /// <returns>A list of solutions.</returns>
    public List<string> Solve(ProblemModel problem)
    {
      List<string> solutions = new List<string>();

      // Simulate a naive approach by iterating through all possible combinations
      for (int i = 0; i < problem.InputData.Count; i++)
      {
        for (int j = 0; j < problem.InputData.Count; j++)
        {
          // Check constraints (inefficiently)
          if (i != j && ValidateCombination(problem.InputData[i], problem.InputData[j]))
          {
            solutions.Add($"Solution found: {problem.InputData[i]} and {problem.InputData[j]}");
          }
        }
      }

      return solutions;
    }

    /// <summary>
    /// Validates a combination of inputs based on the problem's constraints.
    /// </summary>
    /// <param name="input1">The first input.</param>
    /// <param name="input2">The second input.</param>
    /// <returns>True if the combination is valid; otherwise, false.</returns>
    private bool ValidateCombination(string input1, string input2)
    {
      // Simulate a complex validation logic
      return input1.Length + input2.Length < 10; // Arbitrary condition for demonstration
    }
  }
}