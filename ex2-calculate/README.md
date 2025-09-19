## Average Salary Refactor Exercise


### Scenario
You are given a naive implementation that computes an average salary per department. It has hidden bugs, weak design, poor validation, and mixes concerns. You will use GitHub Copilot as a partner to: understand, critique, refactor, harden, document, and test the code.

### Objectives
1. Practice prompt patterns (Explain, Critique, Constrain, Iterate, Compare).
2. Extract risks, assumptions, and hidden defects from existing code.
3. Apply SOLID (focus: SRP, ISP, DIP) while refactoring.
4. Introduce validation, domain-centric design, and custom exceptions.
5. Replace naive numeric handling with money-safe choices (e.g., `decimal`).
6. Generate meaningful unit tests (happy path, edge, failure).
7. Author XML documentation via assisted prompting.
8. Evaluate Copilot output quality and iterate.

### Current Issues to Surface (Have Copilot List & Explain)
Have Copilot analyze `AverageSalary.cs` and confirm (do not just accept the first answer):
- Possible runtime exception when averaging an empty sequence.
- No null / empty guard for employee list or department.
- Negative or zero salary values silently included (data integrity issue).
- Uses `double` for monetary value (precision risk).
- Magic string department usage; no normalization.
- No abstraction (calculation logic + I/O in same flow).
- Lack of domain validation (invalid employee instances allowed).
- No testability seams (static method, direct console usage if present).
- Mixed responsibilities (data, calculation, presentation).
- No error classification (only generic exception handling, if any).
- Missing XML docs on public members.
- No defined policy for "no matching employees" (throw? 0? optional result?).

### Refactor Requirements
Refactor toward:
- Domain model (e.g., `Employee`) with validation.
- Service abstraction (e.g., `ISalaryCalculator`) to decouple consumers.
- Implementation (`SalaryCalculator`) using guard clauses and explicit outcomes.
- Custom exception(s) for invalid salary or bad arguments.
- Monetary type: prefer `decimal`.
- Clear, documented behavior for "no matching employees" (pick and justify a policy).
- Separation of concerns: core logic free of console/UI.
- Unit-testable pure logic.
- XML docs on all public types/members.

### Architecture Discovery (Do NOT Skip)
You are NOT given a project/folder structure here intentionally. Use Copilot to propose at least two alternative .NET solution structures (e.g., layering by Domain/Application/UI or a simpler single-project layout) and justify pros/cons. Pick one, then iteratively refine with Copilot before implementing.

Suggested prompt to start: "Propose two possible multi-project solution structures for refactoring an average salary calculator into clean layers; briefly explain trade-offs."

### Refactoring Flow (Use Iterative Prompts)
1. Ask Copilot for a risk/assumption table; manually verify omissions.
2. Request proposed interfaces (review, prune, simplify).
3. Generate initial refactor (reject anything mixing console and domain logic).
4. Add validation & exceptions (no silent failure paths).
5. Introduce XML docs (have Copilot draft, you refine wording & accuracy).
6. Generate tests (success, invalid, boundary, policy behavior).
7. Ask for alternate implementation strategies (compare clarity & extensibility).
8. Final pass: ask Copilot to critique its own refactor for further improvements.

### Testing Focus
Cover:
- Valid average with multiple employees.
- Negative salary rejected (exception asserted).
- Empty department argument (argument exception expected).
- No matching employees (assert your chosen policy).
- Large salary values (precision and no overflow/rounding surprises).
- Single employee case (average equals that salary).
- Filtering correctness across multiple departments.

### Quality & SOLID Mapping
- SRP: Split calculation, validation, presentation concerns.
- OCP: Calculator can be extended (e.g., weighted averages) without modification.
- LSP: Alternate calculators can replace default via interface.
- ISP: Keep interfaces minimal (`ISalaryCalculator` focused on a single responsibility).
- DIP: High-level orchestration depends on abstractions, not concretes.

### Documentation Guidance
For each public class/method:
- Summary: Describe intent (what, not how).
- Parameters: Constraints & expectations (e.g., non-null, non-empty, >= 0).
- Returns: Defined contract (include policy for no matches).
- Exceptions: List custom and argument exceptions.

Prompt Copilot: "Generate XML docs for this class; emphasize input validation and exception behavior."

### Evaluation Rubric (Self-Check)
- Design separation achieved (Y/N)
- Monetary correctness (Y/N)
- Deterministic, documented edge behaviors (Y/N)
- Breadth of test coverage (qualitative assessment)
- Prompt iteration cycles (≥ 3) evidenced
- Critical review of Copilot output documented

---

### Suggested Copilot Prompts

Understanding & Critique:
- "Explain line by line what `AverageSalary.cs` does; list hidden failure modes."
- "List all assumptions the current salary logic makes about input data."

Design & Structure Discovery:
- "Propose two alternative .NET solution structures for separating domain, application logic, and UI for this salary calculator; include pros/cons."
- "Refine the chosen structure to minimize unnecessary layers while retaining testability."

Refactor & SOLID:
- "Propose an interface for a salary calculation service applying SRP and DIP."
- "Refactor the salary logic to use decimal and guard clauses; exclude negative salaries with a custom exception."

Validation & Exceptions:
- "Introduce a custom exception for invalid salary values; integrate into the calculation flow and update method docs."

Documentation:
- "Generate XML documentation comments for all public members; include exception conditions and edge case policy."

Testing:
- "Create unit tests covering normal, empty, negative, and no-match scenarios for the salary calculator using xUnit (or NUnit)."
- "Add a test ensuring averaging an empty matching set follows the documented no-match policy."

Iteration & Quality:
- "Critique this refactored SalaryCalculator for any remaining SOLID violations or over-engineering."
- "Suggest further decomposition or patterns to enable alternate averaging strategies (e.g., weighted, rolling)."

Prompt Engineering:
- "Rewrite this earlier prompt to be more constrained and produce higher quality output—reduce ambiguity."
- "Compare two alternative rewrites of my prompt and explain which will yield more precise code."

Exploration & Extensibility:
- "Suggest how this design would adapt if salary data came from an async repository or external API."

Final Audit:
- "List any potential precision, globalization, or localization issues remaining in this solution."

Use these prompts as starting points; iterate, constrain, and compare results for best learning impact.




