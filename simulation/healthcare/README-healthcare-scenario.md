# Healthcare Mini-Normalizer Challenge

This exercise asks you to build (within ~60 minutes) a minimal C# console application that ingests a single CSV file of mixed VISIT and LAB rows, validates each line, maps a small set of codes, masks patient names for an analytics view, and prints a concise metrics summary.

Core goals:
- Parse one CSV (no additional inputs).
- Build in‑memory collections: Patients, Visits, LabResults, Rejections.
- Apply only two required rejection rules (missing MRN, invalid date) plus an optional warning (empty code).
- Perform minimal terminology mapping (R001/R002, GLU/HCT) and count unmapped codes.
- Mask names (first letter + ***). Never expose full names in the analytics list.
- Track and output metrics: totalLines, totalPatients, visitCount, labCount, rejectedCount, unmappedCodesCount (optional warningsCount).
- Provide 3–4 focused unit tests (rejection, mapping, masking, optional invalid date).

Success criteria:
- Summary line printed: `Lines=X Patients=Y Visits=V Labs=L Rejected=R Unmapped=U`.
- At least one demonstrable rejection and one unmapped code scenario possible by editing the CSV.
- Tests pass (green) in Test Explorer.
- No scope creep: only the four models plus a simple metrics container.

Optional (only if time remains): an interactive loop (patients / patients analytics / visits MRN= / observations MRN= / observations test= / metrics / exit).

Stop when acceptance criteria are satisfied—clarity and correctness over extra features.
