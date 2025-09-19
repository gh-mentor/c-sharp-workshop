## Byte Transformation with C#

This exercise guides learners through designing and implementing a small, focused, test‑driven C# component that performs byte transformations using both fixed (bitwise XOR mask) and pluggable (delegate / lambda) strategies. The activity is also structured to practice progressive, outcome‑oriented prompting with GitHub Copilot.

---
### Objectives
1. Apply clean class design and encapsulation for low‑level data handling.
2. Practice safe handling of collection constraints (size limit & validity flag).
3. Implement method overloading and delegate / lambda based extensibility.
4. Use TDD / incremental tests to drive implementation.
5. Practice increasingly specific Copilot prompting patterns (“prompt ladder”).
6. Reflect on code quality, readability, and extensibility.

---
### Functional Specification
Class: `ByteFactory`
Requirements:
- Internal storage: private `List<byte> m_bytes` initialized from constructor input (any `IEnumerable<byte>` acceptable). Copy the data (do not retain external reference).
- Capacity rule: maximum allowed count = 1000. If exceeded, set an internal validity flag `m_isValid = false`; otherwise `true`.
- Provide a public method `IsValid()` returning the flag.
- Provide `GetByteCount()` returning the current count (even if invalid, still report actual stored size truncated if you choose OR reject extra bytes—define behavior and document it; recommended: reject extras and keep original subset while flagging invalid).
- Provide `Transform(byte mask)` returning a new `List<byte>` where each element = `originalByte ^ mask`.
- Provide overloaded `Transform(Func<byte, byte> transformFunc)` returning a new `List<byte>` with the function applied to each stored byte. If `transformFunc` is null, throw `ArgumentNullException`.
- Ensure both transform methods do not mutate internal state; they return new collections.
- All methods should be deterministic and side‑effect free except for constructor validation logic.

Edge / Behavior Notes (decide & document in code XML comments):
- Construction with `null` source should throw `ArgumentNullException`.
- If input length > 1000: either (A) store first 1000 and mark invalid, or (B) store nothing and mark invalid. (Default expectation: Option A.)
- Empty input is valid; transforms on empty list return empty list.

---
### Suggested Extension Ideas (Optional After Core)
- Add `TransformInPlace` variant (mutating) and compare performance.
- Add streaming enumeration via `IEnumerable<byte> TransformEnumerable(byte mask)` using `yield return`.
- Add combined pipeline: accept `IEnumerable<Func<byte, byte>>` and apply sequentially.
- Add simple metrics (e.g., count of transforms performed) with thread‑safe increment.

---
### Copilot Prompt Ladder (Progressive Prompting Strategy)
Use these in order; each step narrows scope, encourages iteration, and demonstrates prompt engineering best practices for Copilot.

1. High‑Level Intent
  Prompt: "Create a C# class for byte transformations with XOR and custom delegate based strategies plus unit tests. Outline only."
  Goal: Get a skeletal design (class name, method signatures, test class outline) WITHOUT full implementation.

2. Refine Data Constraints
  Prompt: "Add a size limit (1000 bytes) with a validity flag to the previous outline. Show constructor and IsValid method only." 
  Goal: Isolate constructor & validation logic.

3. Implement Core Methods
  Prompt: "Implement GetByteCount and Transform(mask) using XOR; keep fields prefixed with m_. No tests yet." 
  Goal: Focus on single responsibility implementation.

4. Add Delegate Overload
  Prompt: "Add an overload Transform(Func<byte, byte> f) returning a new List<byte>; throw ArgumentNullException if f is null; do not mutate internal list." 
  Goal: Introduce extensibility.

5. Generate Unit Test Skeleton
  Prompt: "Create xUnit tests covering construction validity, empty list, over-limit list, Transform with mask examples, and delegate inversion example. Skeleton only, no assertions yet." 
  Goal: Establish test surface.

6. Flesh Out Assertions
  Prompt: "Fill in assertions for the test skeleton: verify counts, validity flag, XOR results (e.g., 0x99 ^ 0x55 == 0xCC), delegate inversion, and that passing null delegate throws ArgumentNullException." 
  Goal: Complete test coverage for core behaviors.

7. Edge Case Clarification
  Prompt: "Add tests for mask 0x00 (identity) and 0xFF (bit inversion relative to XOR)." 
  Goal: Reinforce understanding of bit operations.

8. Add XML Documentation
  Prompt: "Add XML doc comments summarizing behaviors, especially handling of >1000 inputs and non-mutating transforms." 
  Goal: Improve code discoverability.

9. Introduce Optional Extensions
  Prompt: "Propose an additional streaming transformation method returning IEnumerable<byte> without allocating a new list; provide implementation and one test." 
  Goal: Show performance/alloc considerations.

10. Refactor & Review
   Prompt: "Review the ByteFactory implementation for clarity, immutability of returned results, and suggest any micro refactors without changing behavior." 
   Goal: Encourage iterative refinement.

11. Performance Consideration (Optional)
   Prompt: "Estimate allocation differences between List<byte> returning Transform and an iterator-based TransformEnumerable; add comments with big-O complexity." 
   Goal: Integrate complexity reasoning.

12. Final README Polish
   Prompt: "Summarize design choices (size guarding strategy, immutability, delegate flexibility) in 4 concise bullets for the README." 
   Goal: Consolidate learning.

Instructor Tip: Encourage students to stop after each step, run tests, and self-assess before moving forward. Discourage combining multiple steps into one broad prompt early on.

---
### Testing Strategy
Recommended framework: `xUnit` (but NUnit or MSTest acceptable). Core test categories:
1. Construction
  - Null input throws.
  - Valid list sets `IsValid == true`.
  - Over limit sets `IsValid == false` and (if Option A) truncates to 1000.
2. Counting & State
  - `GetByteCount` matches stored size.
3. XOR Transform
  - Sample vector: input `[0x99]`, mask `0x55` => `[0xCC]`.
  - Identity: mask `0x00` returns original sequence.
  - Full invert (XOR 0xFF) equals bitwise NOT of original.
4. Delegate Transform
  - Inversion lambda `b => (byte)~b` matches XOR 0xFF result.
  - Custom mapping (e.g., `b => (byte)(b + 1)`).
  - Null delegate throws.
5. Empty Input
  - All transform methods return empty list.
6. Non‑Mutation Guarantee
  - Call `Transform` twice; original outputs equal; internal collection unchanged.

Optional: Parameterized tests for multiple masks & inputs.

---
### Example Usage (Once Implemented)
```csharp
var factory = new ByteFactory(new byte[] { 0x10, 0x20, 0x30 });
if (!factory.IsValid()) throw new InvalidOperationException();

List<byte> masked = factory.Transform(0x55);           // XOR each
List<byte> inverted = factory.Transform(b => (byte)~b); // Custom delegate
```

---
### Evaluation Criteria (Rubric)
- Correctness: All required methods present; tests pass.
- Robustness: Edge cases (empty, over-limit, null delegate) handled.
- Readability: Clear naming, `m_` prefix, concise XML docs.
- Immutability: No unintended mutation of internal list.
- Extensibility: Delegate design allows varied strategies.
- Prompt Discipline: Student used progressive refinement (can be evidenced by commit messages or saved prompts).

Stretch Credit: Added streaming or pipeline extensions with tests & documentation.

---
### Next Steps for Learners
1. Follow the prompt ladder through step 6 to reach a stable MVP.
2. Run tests after each implementation change.
3. Add at least one optional extension.
4. Reflect: Which prompt produced the least useful suggestion? How would you refine it?




