# Exercise: Average Salary Refactor (Prompt-Driven)

## Goal
Refactor the naive average salary logic into a clean, testable, validated design using decimal, SOLID principles, custom exceptions, and NUnit tests.

## What Is Wrong (Confirm Before Refactoring)
Purpose: Quickly surface concrete flaws so you do not refactor blindly. Use a prompt to have the tool list these, then validate each in code yourself.
Problems to verify:
- Empty average: Averaging an empty filtered set throws (InvalidOperationException).
- Missing guards: No null or empty checks for the employee list or department.
- Invalid salary data: Negative (and questionable zero) salaries pass through unvalidated.
- Wrong numeric type: Uses double; risk of rounding errors for money.
- Mixed responsibilities: Data model, calculation, and presentation concerns are blended.
- No abstraction: Logic tied to static method; hard to swap or mock.
- No validation layer: Invalid Employee instances can exist silently.
- Undefined "no matches" policy: Behavior unclear if department has no employees.
- Error handling: Only generic exceptions; no domain-specific classification.
- Missing documentation: Public members lack XML docs explaining contracts.
Your first action: Prompt for an explanation + hidden issues list, then tick each off by reading the code.

## Early Design Decisions (Choose Once, Avoid Rework)
Purpose: Establish non-negotiable rules up front so later prompts stay consistent and tests reflect a stable contract.
Decide now:
1. Money type: Use decimal (fixed-point, safer for currency).
2. No-match policy: Pick one:
   a. Throw a specific exception
   b. Return null / nullable decimal
   c. Return a result object with HasValue flag
   Recommended: nullable decimal (simple, explicit).
3. Invalid salary rule: Salary < 0 triggers InvalidSalaryException.
4. Zero salary: Accept or reject? (Declare explicitly; e.g., allow zero for interns.)
5. Department input: Null/empty/whitespace -> ArgumentException.
6. Case sensitivity: Decide whether department comparison is case-insensitive (recommend ordinal ignore case).
7. Overflow / large values: Decide whether to checked/unchecked (state policy).
Action: Write these decisions verbatim at the top of your refactor (or a DESIGN.md). All subsequent prompts must align with them.

---

## Workflow (Follow Steps In Order)

### Step 1: Baseline Understanding
Prompt:
"Explain line by line what AverageSalary.cs does and list every hidden failure mode or assumption."

### Step 2: Risk & Assumption Table
Prompt:
"Produce a table of risks and assumptions for AverageSalary.cs. Include cause, impact, and mitigation. Do not skip monetary precision."

### Step 3: Architecture Options
Prompt:
"Propose two .NET solution structures (single-project vs multi-project: Domain + Application + Tests). List pros/cons for this small exercise. Recommend one."
Then:
"Refine the recommended structure to the minimal viable layers keeping testability."

### Step 4: Interface & Domain Sketch
Prompt:
"Propose an Employee model with validation and an ISalaryCalculator interface (single responsibility, minimal members). Include chosen no-match policy."
Review, adjust manually, then:
"Revise the interface to remove any premature abstractions."

### Step 5: Implement Core (No I/O)
Prompt:
"Implement Employee (with guards), InvalidSalaryException, ISalaryCalculator, and SalaryCalculator using decimal, guard clauses, and nullable/optional policy for no matches. No console usage."

### Step 6: XML Documentation
Prompt:
"Generate XML docs for all public members emphasizing: parameter constraints, exceptions thrown, and no-match policy."
Manually refine wording.

### Step 7: Unit Tests (NUnit)
Prompt:
"Generate NUnit tests for SalaryCalculator covering: valid average, single employee, no matches (policy), negative salary (exception), empty department (exception), zero salary allowed?, large values precision."
Then:
"Add an edge case test for all employees filtered out."

### Step 8: Alternative Implementation
Prompt:
"Suggest an alternative calculator implementation (e.g., streaming aggregation) and compare clarity, extensibility, and performance trade-offs."

### Step 9: Self-Critique
Prompt:
"Critique the refactored design for remaining SOLID issues, over-engineering risks, and precision/localization concerns."

### Step 10: Final Audit
Prompt:
"List unresolved risks or TODOs and recommend final minor improvements."

---

## Stretch (Optional)
Prompt:
"Show how to adapt SalaryCalculator if data source becomes async (repository returning Task<IEnumerable<Employee>>)."




