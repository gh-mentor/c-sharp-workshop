## Assembly Line Simulation (Advanced Lab)

Implement `FactoryAssemblyLine` (see `IFactoryAssemblyLine`) so all tests pass (`tests/FactoryAssemblyLineTests.cs`). Keep it lean, correct, intention‑revealing.

### Model
Station:
- Id (>0, unique)
- ProcessingTime (>0)
- IsActive (bool)
All state in-memory. No async / persistence.

### API (must implement)
AddStation, RemoveStation, StartAssembly, StopAssembly,
GetProcessingTime, GetTotalProcessingTime (active only),
GetNumStations, GetNumActiveStations, GetNumInactiveStations, IsStationActive.

Complexities: everything O(1) except `GetTotalProcessingTime()` (may aggregate). Optional: maintain cached running total for active stations.

### Data Structure
`Dictionary<int, Station>` where `Station` is a private record/class: `private sealed record Station(int ProcessingTime, bool IsActive);`
You may replace the record on state changes or use a mutable type—consistency + clarity matter most.

### Validation & Errors
Throw:
- `ArgumentException` for stationId <= 0 or processingTime <= 0.
- `InvalidOperationException` for: duplicate add; missing station on remove/start/stop/get.
No silent ignores (except if you choose documented no-op for Start/Stop on same state—be consistent).

### Edge Rules
- Start on active: either no-op OR throw (pick one; tests define expected behavior—assume no-op unless failing tests say otherwise).
- Stop on inactive: same policy.
- Removing an active station must adjust cached total if used.
Invariant: sum(active.processingTime) == GetTotalProcessingTime().

### Acceptance
- All tests pass.
- Guards in place (arguments + operations).
- Public surface matches interface only.
- Clean names, no dead code, XML docs on public members.

### Common Pitfalls
Duplicate add logic leaks; forgetting to update cached total; using KeyNotFoundException; double counting after multiple Start calls; negative totals after mismatched decrement.

### Suggested Order
1. Station model + dictionary.
2. AddStation / RemoveStation.
3. StartAssembly / StopAssembly.
4. Query counts + IsStationActive.
5. GetProcessingTime / GetTotalProcessingTime (simple aggregate first).
6. Optional: introduce cachedActiveTotal.
7. XML docs + polish.
8. Run tests / fix.

### Suggested Copilot Chat Prompts

1. "Summarize the README requirements for FactoryAssemblyLine implementation." 
2. "Create a private Station record/class (ProcessingTime, IsActive) in FactoryAssemblyLine." 
3. "Implement AddStation and RemoveStation with validation and proper exceptions." 
4. "Implement StartAssembly and StopAssembly (treat repeated Start/Stop as no-op) and explain choice." 
5. "Add query methods: GetProcessingTime, IsStationActive, GetNumStations, GetNumActiveStations, GetNumInactiveStations." 
6. "Implement GetTotalProcessingTime using aggregation; verify invariant logic." 
7. "Refactor to add cached active total field and update it on Add/Remove/Start/Stop (optional)." 
8. "(Optional) Show the current FactoryAssemblyLine code so we can review for clarity and consistency." 
9. "Add XML documentation to all public members matching interface semantics." 
10. "List invariants and edge cases; confirm they hold in current code." 
11. "Run NUnit tests; list any failures and suggest fixes." 
12. "Apply cleanup: remove unused usings, simplify expressions, ensure guard clause consistency." 
13. "(Optional) Add UpdateProcessingTime(stationId, newTime) with cache adjustments." 
14. "(Optional) Add a debug-only assertion that recomputes total and checks it equals cached total." 
15. "(Optional) Make class thread-safe using a private lock; describe trade-offs." 
16. "Generate final readiness checklist vs acceptance criteria." 
17. "Confirm code is ready for submission (no pending issues)." 

### Stretch (Optional)
Thread safety; UpdateProcessingTime; expose active IDs enumeration; micro-benchmark.

Good luck—focus on clarity, correctness, and disciplined state changes.



