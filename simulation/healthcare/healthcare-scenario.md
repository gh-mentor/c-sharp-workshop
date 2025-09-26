# Micro Scenario: Healthcare Mini-Normalizer (60-Minute Build)

## Objective
Create a single C# console app that ingests one CSV, builds simple canonical objects, applies minimal validation and terminology mapping, masks patient names for an analytics view, reports a concise summary, and includes 3–4 focused unit tests.

## Start Here (Quick Checklist)
1. Create a new Solution.
2. Add Console App (.NET 8) project: `HealthcareMiniNormalizer`.
3. Add Test Project (NUnit, .NET 8): `HealthcareMiniNormalizer.Tests`.
4. Add `data.csv` at solution root (or copy to project output) using the specified format.
5. Implement ONLY the four models (Patient, Visit, LabResult, Rejection) + Metrics container.
6. Run once with sample lines; confirm summary prints.
7. Add tests (minimum 3) and ensure green.
8. Stop when Acceptance Criteria are met—do not expand scope unless time remains.

## Recommended Visual Studio Project Types
- Main: Console App (.NET 8)
- Tests: NUnit Test Project (.NET 8)
(Optional later: a class library for domain separation—NOT needed for the 60?minute build.)

## Input (Single File: data.csv)
Format (one line per record):  
`RecordType,MRN,PatientName,DOB,Date,Code,Value`

RecordType values:  
- `VISIT` (Date = VisitDate, Code = ReasonCode, Value ignored)  
- `LAB` (Date = CollectedDate, Code = TestCode, Value = lab value)

Example lines:  
```
VISIT,1001,Jane Smith,1975-02-10,2025-09-01,R001,
LAB,1001,Jane Smith,1975-02-10,2025-09-01,GLU,105
```

## Canonical Models (DO NOT ADD MORE)
- Patient: MRN, Name, MaskedName, DOB  
- Visit: VisitId, MRN, VisitDate, ReasonCodeOriginal, ReasonCodeMapped  
- LabResult: ResultId, MRN, TestCodeOriginal, TestCodeMapped, Value, CollectedDate  
- Rejection: Line, Reason  
(Adding extra models wastes time and adds no value here.)

## Validation
Required rules:  
1. Missing MRN ? reject (`MissingMRN`)  
2. Invalid date (DOB or Date) ? reject (`InvalidDate`)  

Optional warning:  
- Empty Code ? keep record, count warning (`WarnEmptyCode`)

Decision outcomes:
- Valid ? continue ? map & create canonical object.
- Reject ? add Rejection entry (Line, Reason); do NOT create objects; increment `rejectedCount`.
- Warn ? continue processing; increment `warningsCount` (if implemented).

## Terminology Mapping
- ReasonCode: R001 ? Annual, R002 ? FollowUp, else ? Unknown (Unknown counts toward unmapped)  
- TestCode: GLU ? LOINC-GLU, HCT ? LOINC-HCT, else unmapped (retain original)  

Mapping decision notes:
- Unmapped Reason ? set mapped = "Unknown" + increment unmapped.
- Unmapped Test ? mapped stays empty or original left in `TestCodeOriginal`; increment unmapped.

## De-identification
Name masking: first character + `***`. If null/empty ? `*`.

## Metrics (Required)
- totalLines  
- totalPatients  
- visitCount  
- labCount  
- rejectedCount  
- unmappedCodesCount (reason + test combined)  

Optional: warningsCount

Metric integrity guideline: Each counter increments exactly once per triggering event (one pass). Do NOT recalc from lists at the end.

## Console Output (End of Run)
Single summary line:  
`Lines=X Patients=Y Visits=V Labs=L Rejected=R Unmapped=U`

Then:  
- Patients list: `MRN | Name | MaskedName`  
- Rejections grouped by reason (if any)

## Optional Interactive Queries (If Time)
If you finish core requirements early, you may add a very small interactive command loop AFTER printing the summary. This is OPTIONAL and not required to meet acceptance criteria. Keep it simple (a while loop reading a line, basic string matching). Skip entirely if time is tight.

Supported commands (suggested):
- `patients` ? list MRN | Name | MaskedName.
- `patients analytics` ? list MRN | MaskedName only (never the clear name).
- `visits MRN=1234` ? list visits for that MRN.
- `observations MRN=1234` ? list lab results for that MRN.
- `observations test=LOINC-GLU` ? list lab results whose mapped OR original code matches value after `test=`.
- `metrics` ? reprint the summary line.
- `exit` ? terminate the loop.

Implementation guidance:
- Do NOT build a full parser; use simple `StartsWith` / `Contains` checks.
- Ignore unknown commands; print a short help hint.
- Keep this below ~30 lines of code.
- Do not let this feature delay finishing tests or core metrics.

If omitted, you still pass as long as the summary + lists print correctly.

## Unit Tests (Minimum 3)
1. Missing MRN line ? rejected  
2. Mapping: GLU ? LOINC-GLU; unknown code increments unmapped  
3. Name masking edge cases (“A”, empty)  
Optional 4th: Invalid date rejects  

## Constraints
- Single pass over file  
- In-memory lists only  
- No async, no external packages (besides test framework)  
- Single CSV input  
- Keep functions small and explicit  

## Suggested Workflow (Guidance)
1. Models + metrics container  
2. Parsing + validation  
3. Canonical creation + mapping + masking  
4. Metrics accumulation + summary output  
5. Tests (rejection, mapping, masking, optional date)  
6. Unmapped + warning handling refinement  

## Copilot Prompt Starter Sequence
1. "List minimal classes (no methods) for Patient, Visit, LabResult, Rejection, MetricsTracker."  
2. "Outline parse ? validate ? dispatch steps (6 bullets)."  
3. "Provide method signatures only for parser, validator, mapper, metrics update, summary formatter."  
4. "List test case names for missing MRN, invalid date, unknown code, name masking."  
5. "Suggest summary line format with metrics order."  

## Structured Prompt Set
1. “List the minimal classes and their fields for this micro normalizer (no methods).”  
2. “Outline a simple parse ? validate ? dispatch flow returning either canonical raw data or a rejection.”  
3. “Describe a tiny terminology mapping approach with a way to increment an unmapped counter.”  
4. “Specify a deterministic name masking rule and edge handling.”  
5. “List each point in the flow where metrics should increment (line read, rejection, visit created, lab created, unmapped).”  
6. “Provide test case descriptions for missing MRN, unknown code, date invalid, name masking.”  
7. “Suggest a concise summary output format using collected metrics.”  
8. “Identify a likely over-abstraction to avoid in this limited scope.”  

## Acceptance Criteria
- Summary line printed with all required metrics  
- At least one demonstrable rejection  
- Unmapped code increments `unmappedCodesCount`  
- Masked names never expose full originals  
- All tests pass  
- Single CSV only input  

## Common Mistakes to Avoid
- Not trimming MRN (duplicate patients)  
- Forgetting to increment unmapped on “Unknown” reason code  
- Mixing parsing and validation logic  
- Omitting rejection reason text  
- Overusing interfaces for one-off functions  

## README Sections (Suggested)
Overview | Input Format | Validation Rules | Terminology Mapping | Metrics | How to Run | Tests | Possible Extensions

## Optional Quick Extensions
- Separate warningsCount  
- Age calculation from DOB  
- Export metrics to JSON  
- Basic command loop (if skipped earlier)

## Reflection Prompts
- “Which decision saved the most time?”  
- “First change if adding a second file type?”  
- “What metric would you add next for quality insight?”
