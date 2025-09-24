# Exercise 9 – WPF Address Book Refactor & Enhancement

Goal: Use GitHub Copilot (chat + inline) to 1) understand the existing WPF MVVM sample, 2) enumerate risks / code smells / missing features, 3) iteratively refactor for robustness, testability, clarity, and maintainability, and 4) add selected improvements.

You should practice structured prompting (ask, review, refine) rather than accepting the first suggestion. Keep commits small and purposeful.

---
## 1. Baseline Understanding
Open these files:
- Window1.xaml / Window1.xaml.cs
- ViewModels/*.cs
- Model/*.cs
- JulMar/*.cs (command + base VM helpers)
- addressbook.xml (data file)

Copilot Chat Prompts (run sequentially):
1. "Summarize the overall architecture of this WPF app (patterns, layers, responsibilities)." 
2. "Explain MainViewModel.cs line by line; list assumptions and potential failure modes." 
3. "Explain how ContactCardManager.Load works and what happens if the XML file is missing or malformed." 
4. "List all public properties that lack validation and potential user-facing issues." 
5. "Identify all uses of magic strings and propose safer alternatives." 
6. "List threading or async concerns in current load/save implementation." 
7. "Explain the lifetime/Dispose pattern used and whether it is idiomatic for this scenario." 

Take notes (create a local NOTES.md if useful).

---
## 2. Initial Issues / Risks (Expect Copilot to Help You Expand)
Seed list (do not treat as exhaustive):
- No nullability (nullable disabled) ? possible NullReferenceExceptions.
- No error handling around file I/O (missing file, locked file, malformed XML).
- Load is synchronous on UI thread (potential UI freeze on large files / slow I/O).
- Save only occurs in Dispose after Yes prompt; user cannot explicitly save mid-session.
- No model or input validation (Name, Email, Phones, ZipCode may be empty / invalid).
- Magic strings in OnPropertyChanged ("SelectedCard", etc.) instead of nameof.
- No CanExecute requery for RemoveCommand when selection changes (command might remain enabled/disabled incorrectly in larger refactors).
- Tight coupling: ViewModel directly references static ContactCardManager & file path constant.
- ContactCardManager assumes full XML schema presence (no defensive querying or defaults).
- No tests (cannot safely refactor persistence logic).
- No XML documentation / inconsistent coding standards vs. repo guidelines.
- Data classes are mutable bags with no invariants or validation.
- No dirty tracking (can't warn about unsaved changes per contact edits; only a broad save prompt at close).
- Email / phone not normalized (formatting, trimming) – potential UX issue.
- No separation of concerns for persistence (no IRepository or abstraction for future JSON/DB switch).

Prompt: "Expand this risk list with at least 5 additional concerns focusing on maintainability and user experience."

---
## 3. Define Target Improvements (Design First)
Before changing code, ask Copilot:
1. "Propose a refactor plan grouped into: Safety (tests + guards), Reliability (error handling), UX (validation + feedback), Maintainability (abstractions + naming)." 
2. "Suggest an interface IContactRepository with async LoadAsync/SaveAsync and typical exception contract." 
3. "Design a minimal validation strategy (DataAnnotations or custom) for ContactCard + ContactAddress including required fields and simple regex for Email/Zip." 
4. "Propose a dirty tracking approach for contacts (e.g., ITrackDirty interface or property change flag)." 
5. "Suggest how to integrate INotifyDataErrorInfo for per-field validation feedback in WPF." 

Review suggestions, adjust manually, THEN implement.

---
## 4. Incremental Refactor Steps (Recommended Order)
Perform each step; after each, build & (if you create tests) run tests.

1. Add nullable enable (Directory.Build.props or project file) and fix warnings minimally.
2. Replace property change magic strings with nameof.
3. Introduce IContactRepository + XmlContactRepository (wrap existing logic). Add async: Task<IReadOnlyList<ContactCard>> LoadAsync(string path), Task SaveAsync(...). Use XmlReader/Writer safely.
4. Add error handling: surface load/save failures to UI (MessageBox or Status property) + graceful defaults when file missing (start empty list).
5. Add a SaveCommand + explicit Save button; decouple saving from Dispose. Keep close prompt but use dirty tracking to decide whether to prompt.
6. Implement dirty tracking (set IsDirty when any contact property changes; reset after successful save). Prompt only if IsDirty.
7. Introduce validation (DataAnnotations or custom). Add ValidationErrors collection in ContactViewModel or adopt INotifyDataErrorInfo. Show basic validation state in UI.
8. Add CanExecute logic: RemoveCommand disabled when nothing selected; ensure RaiseCanExecuteChanged fires on SelectedCard changes.
9. Add NewContact initialization defaults (e.g., placeholder name) + immediate selection focus (optional decide in XAML).
10. Make load/save async (await repository). Consider showing a simple IsBusy flag (spinner or disabling UI).
11. Add unit tests (NUnit) for: repository load/save roundtrip (use temp file), invalid XML handling, validation logic, dirty tracking, Add/Remove behaviors.
12. Add XML docs to all public members (follow repo coding standards).
13. Polish: trim strings on set, normalize phone formatting (optional helper), centralize constants, review exception messages.

---
## 5. Sample Copilot Implementation Prompts
Use precise, scoped prompts:
- "Refactor MainViewModel to use an IContactRepository injected via constructor; create an XmlContactRepository implementing async load/save." 
- "Generate IContactRepository interface with async methods and summary XML docs." 
- "Add dirty tracking to ContactViewModel: add IsDirty boolean set on any property change and a ResetDirty method." 
- "Implement DataAnnotations on ContactCard (Required Name, Email with regex, ZipCode pattern, optional phones) and helper to validate and return errors." 
- "Add INotifyDataErrorInfo implementation to ContactViewModel using DataAnnotations each time a property changes." 
- "Modify RemoveCommand to re-evaluate CanExecute when SelectedCard changes." 
- "Create NUnit tests for XmlContactRepository: load missing file returns empty list, malformed XML throws custom RepositoryException." 
- "Add SaveCommand to MainViewModel (disabled when not dirty) and wire to repository SaveAsync." 
- "Refactor synchronous file I/O in XmlContactRepository to async using XmlWriter + Task.Run pattern if necessary (explain trade-offs)." 
- "Generate XML documentation for MainViewModel public members." 

---
## 6. UI Enhancements (Optional)
Prompts:
- "Suggest XAML DataTemplate for displaying a contact with validation error highlighting." 
- "Add a toolbar with Add / Remove / Save buttons and bind commands (WPF)." 
- "Implement a simple search TextBox filtering the Contacts collection (case-insensitive)." 

---
## 7. Stretch Goals
Pick any (time-box):
- JSON repository (System.Text.Json) behind same interface.
- Recent files MRU (different XML paths).
- Undo/Redo stack (Memento pattern or change journal).
- Sorting & filtering (CollectionViewSource).
- Theming (ResourceDictionaries + light/dark).
- Unit test for validation edge cases (empty + whitespace, invalid email, zip lengths).
- Async file watcher: auto-detect external file changes and prompt reload.

Prompts:
- "Implement a JsonContactRepository parallel to XmlContactRepository reusing model classes." 
- "Show how to integrate CollectionViewSource for sorting Name ascending with dynamic filtering by search text." 

---
## 8. Quality Checklist (Completion Criteria)
Ensure before finishing:
- All magic strings replaced with nameof.
- Nullable enabled; warnings resolved or consciously suppressed with justification.
- Repository abstraction in place; UI has no direct file path references besides injection.
- Load/save wrapped in try/catch with user feedback.
- Validation active; invalid fields visibly indicated or at least collected.
- Commands reflect current state (CanExecute accurate) and SaveCommand present.
- Dirty tracking prevents accidental data loss; prompt only when necessary.
- Async operations do not block UI; IsBusy (or similar) used if implemented.
- Unit tests (if added) pass; no flaky tests.
- XML docs on public members.
- No unused usings; consistent naming and formatting.
- No direct UI blocking operations (.Result / .Wait()).

Prompt: "Generate a final audit report versus the quality checklist; list any gaps." Then close remaining gaps.

---
## 9. Suggested Wrap-Up Prompt
"Summarize all refactors applied, their rationale, and remaining potential improvements if the project continued." Capture this in a FINAL_NOTES.md.

---
## 10. Submission / Review
Provide:
- Updated source
- (Optional) Tests added
- FINAL_NOTES.md with summary
- NOTE of any deliberate omissions (e.g., skipped INotifyDataErrorInfo) & why

---
## Prompt Hygiene Tips
- Be specific: reference exact file and method names.
- Ask for explanation before code when unsure: "Explain then implement." 
- If output is too large or off-target, narrow scope: "Only modify MainViewModel constructor for DI injection, no other changes." 
- Always review generated code for correctness, naming, error handling, and standards.

Good luck—focus on clarity, safe evolution, and disciplined iterative improvement.
