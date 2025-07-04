using NUnit.Framework;
using OptimizationExercise.Services;
using OptimizationExercise.Models;

namespace OptimizationExercise.Tests
{
  [TestFixture]
  public class UnoptimizedSolverTests
  {
    private UnoptimizedSolver m_solver;

    [SetUp]
    public void Setup()
    {
      m_solver = new UnoptimizedSolver();
    }

    [Test]
    public void Solve_ValidInput_ReturnsExpectedResult()
    {
      // Arrange
      var problem = new ProblemModel
      {
        InputData = new[] { 1, 2, 3, 4, 5 },
        Constraints = new[] { 10 }
      };

      // Act
      var result = m_solver.Solve(problem);

      // Assert
      Assert.AreEqual(15, result); // Assuming the expected result is the sum of the input data
    }

    [Test]
    public void Solve_EmptyInput_ReturnsZero()
    {
      // Arrange
      var problem = new ProblemModel
      {
        InputData = new int[0],
        Constraints = new[] { 10 }
      };

      // Act
      var result = m_solver.Solve(problem);

      // Assert
      Assert.AreEqual(0, result);
    }

    [Test]
    public void Solve_InputExceedsConstraints_ThrowsException()
    {
      // Arrange
      var problem = new ProblemModel
      {
        InputData = new[] { 1, 2, 3, 4, 5 },
        Constraints = new[] { 5 }
      };

      // Act & Assert
      Assert.Throws<System.Exception>(() => m_solver.Solve(problem));
    }

    [Test]
    public void Solve_NegativeInput_ReturnsExpectedResult()
    {
      // Arrange
      var problem = new ProblemModel
      {
        InputData = new[] { -1, -2, -3 },
        Constraints = new[] { -10 }
      };

      // Act
      var result = m_solver.Solve(problem);

      // Assert
      Assert.AreEqual(-6, result); // Assuming the expected result is the sum of the input data
    }
  }
}