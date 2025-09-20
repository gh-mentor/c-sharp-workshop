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

Tip: Stop after each step, run tests, and self-assess before moving forward. Avoid combining multiple steps into one broad prompt early on.

---
### Testing Strategy
Primary framework for new tests in this workshop: `NUnit` (aligns with global guidelines; adapt easily to xUnit/MSTest if desired). Focus on small, isolated, deterministic tests. Favor clear Arrange / Act / Assert sections and explicit test names that read like specifications (e.g., `Transform_WithMask55_OnSingleByte99_ReturnsCC`).

Core categories and representative cases:
1. Construction & Validation
  - Null input throws `ArgumentNullException`.
  - Valid input within limit sets `IsValid == true`.
  - Over limit: `IsValid == false`; stored count == 1000 (Option A truncation) and no element beyond index 999 present.
  - Empty sequence remains valid; count == 0.
2. Counting & Internal State
  - `GetByteCount` equals actual stored list count across: empty, boundary (1000), over-limit input.
3. XOR Transform Behavior
  - Sample vector: `[0x99]` with mask `0x55` => `[0xCC]`.
  - Identity mask `0x00` returns deep-equal sequence (reference should differ to ensure non-mutation).
  - Inversion mask `0xFF` equals per-byte bitwise NOT of original.
  - Mixed sequence with multiple masks via parameterized test (e.g., `(input, mask, expected)` tuples).
4. Delegate Transform
  - Inversion lambda `b => (byte)~b` matches XOR 0xFF results for a randomized sample.
  - Increment lambda `b => (byte)(b + 1)` wraps correctly (e.g., 0xFF -> 0x00).
  - Null delegate throws `ArgumentNullException` (assert parameter name if desired).
5. Empty Input Invariance
  - Both transform overloads return empty list instances (not null) when factory constructed with empty input.
6. Non-Mutation & Idempotence Guarantees
  - Two consecutive `Transform(mask)` calls with same mask yield equal sequences; original internal data remains unchanged (can reflect by calling a different mask afterwards and verifying independence).
7. Edge & Boundary Conditions
  - Exactly 1000 bytes: valid.
  - 1001 bytes: invalid, count reported as 1000, transformation still operates over stored subset.
8. Determinism & Purity
  - Repeated calls with same delegate produce identical outputs (value equality, not same reference).

Additional quality techniques:
- Parameterized tests: Use `TestCase` / `TestCaseSource` to cover multiple (input, mask) combinations compactly.
- Randomized smoke test (bounded, reproducible with fixed seed) to cross‑verify delegate inversion vs. XOR 0xFF for e.g. 128 random bytes.
- Negative tests: ensure exceptions are thrown early and do not partially mutate state.

Test naming guidelines:
- Pattern: `[MethodOrScenario]_[Condition]_[ExpectedOutcome]`.
- Keep hex values uppercase for readability (e.g., `ReturnsCC`).

Code coverage focus:
- Aim for 100% of `ByteFactory` methods.
- Assert both value correctness and structural guarantees (non-mutation, truncation rule).

Performance note (optional):
- For large (near‑limit) inputs compare allocation between `Transform(mask)` and delegate version—they should be equivalent; streaming variant (if added) reduces allocation by deferring materialization.

---
### Suggested Testing Prompts for Copilot
Use these targeted prompts while developing tests. Paste (or paraphrase) them incrementally—avoid over-broad multi‑feature prompts early on.

Foundational Skeleton
Prompt: "Generate an NUnit test class ByteFactoryTests with setup showing valid, empty, over-limit construction. Only method stubs, no assertions." 

Constructor Edge Cases
Prompt: "Add NUnit tests verifying ByteFactory throws ArgumentNullException on null source and truncates over-limit input to 1000 while setting IsValid false." 

Counting & State
Prompt: "Write NUnit tests for GetByteCount covering empty, exactly 1000, and over-limit (1001) inputs. Include assertions for IsValid flag." 

XOR Transform Cases
Prompt: "Add tests verifying Transform(mask) with inputs [0x99] mask 0x55 => 0xCC, identity mask 0x00 returns original bytes, and mask 0xFF equals bitwise NOT of each byte." 

Delegate Overload
Prompt: "Create tests for Transform(Func<byte, byte>) including inversion lambda, increment with wrap, and null delegate throwing ArgumentNullException." 

Parameterized Masks
Prompt: "Introduce NUnit TestCase attributes to cover multiple (inputSequence, mask, expectedHexSequence) scenarios for Transform(byte mask)." 

Non-Mutation Guarantee
Prompt: "Add a test proving that repeated Transform calls return new list instances and original internal bytes are unchanged." 

Randomized Cross-Check
Prompt: "Generate a reproducible test (fixed Random seed) comparing results of Transform(0xFF) and Transform(b => (byte)~b) for 128 random bytes." 

Boundary Conditions
Prompt: "Add tests for exactly 1000 bytes (valid) and 1001 bytes (invalid) ensuring stored count is 1000 and transforms operate on truncated list." 

Documentation Enforcement
Prompt: "Add XML doc comments to ByteFactory summarizing size limit, truncation behavior, and non-mutation; do not change implementation." 

Refactor for Testability (if design drifts)
Prompt: "Suggest minimal refactors to ByteFactory to make internal byte list observable only via safe methods while keeping encapsulation." 

Coverage Gap Audit
Prompt: "List any untested branches or exception paths in ByteFactory and propose new NUnit tests to cover them." 

Performance Exploration (Optional Extension Present)
Prompt: "Generate a benchmark-style test (not strict perf) comparing Transform(byte) vs. TransformEnumerable(byte) over 1000 bytes counting allocations conceptually in comments." 

Final Review
Prompt: "Review all ByteFactory tests for redundancy and propose one consolidation using parameterized cases without reducing clarity." 

How to iterate:
1. Start with skeleton tests.
2. Add concrete assertions gradually (fail fast mindset).
3. Introduce parameterization to reduce duplication.
4. Add randomized (seeded) parity checks last to avoid early noise.
5. Refactor test names for clarity before final polish.

Use these prompts selectively—each should produce a small, reviewable diff.

---
### Example Usage (Once Implemented)
```csharp
var factory = new ByteFactory(new byte[] { 0x10, 0x20, 0x30 });
if (!factory.IsValid()) throw new InvalidOperationException();

List<byte> masked = factory.Transform(0x55);           // XOR each
List<byte> inverted = factory.Transform(b => (byte)~b); // Custom delegate
```






