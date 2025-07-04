using System;

namespace OptimizationExercise.Utils
{
  public static class HelperFunctions
  {
    /// <summary>
    /// Validates the input data for the problem.
    /// </summary>
    /// <param name="inputData">The input data to validate.</param>
    /// <returns>True if the input data is valid, otherwise false.</returns>
    public static bool ValidateInput(string inputData)
    {
      // Simple validation logic (deliberately unoptimized)
      if (string.IsNullOrWhiteSpace(inputData))
      {
        return false;
      }

      // Check for specific conditions (could be more complex)
      return inputData.Length > 5;
    }

    /// <summary>
    /// Formats the output for display.
    /// </summary>
    /// <param name="outputData">The output data to format.</param>
    /// <returns>A formatted string representation of the output data.</returns>
    public static string FormatOutput(string outputData)
    {
      // Simple formatting logic (deliberately unoptimized)
      return $"Output: {outputData}";
    }
  }
}