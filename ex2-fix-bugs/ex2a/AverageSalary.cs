using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents an employee with a name, department, and salary.
/// </summary>
public class Employee
{
  public string Name { get; set; }
  public string Department { get; set; }
  public double Salary { get; set; }
}

/// <summary>
/// Provides methods to calculate the average salary of employees in a given department.
/// </summary>
public class SalaryCalculator
{
  /// <summary>
  /// Calculates the average salary of employees in a given department.
  /// </summary>
  /// <param name="employees">A list of employees.</param>
  /// <param name="department">The department to filter by.</param>
  /// <returns>The average salary of employees in the department.</returns>
  /// <exception cref="InvalidOperationException">Thrown if no employees are found in the specified department.</exception>
  public static double CalculateAverageSalary(List<Employee> employees, string department)
  {
    var departmentEmployees = employees.Where(e => e.Department == department).ToList();

    if (!departmentEmployees.Any())
    {
        // TODO: Handle the case where there are no employees in the specified department
        
    }

    return departmentEmployees.Average(e => e.Salary);
  }
}

public class Program
{
  public static void Main()
  {
    try
    {
      var employees = new List<Employee>
      {
        new Employee { Name = "Alice", Department = "Engineering", Salary = 75000 },
        new Employee { Name = "Bob", Department = "Engineering", Salary = -50000 }, // Negative salary (logical error)
        new Employee { Name = "Charlie", Department = "HR", Salary = 60000 },
        new Employee { Name = "Diana", Department = "Engineering", Salary = 80000 },
        new Employee { Name = "Eve", Department = "HR", Salary = 0 } // Zero salary (edge case)
      };

      string department = "Engineering";
      double averageSalary = SalaryCalculator.CalculateAverageSalary(employees, department);

      Console.WriteLine($"The average salary in the {department} department is: {averageSalary}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex.Message}");
    }
  }
}