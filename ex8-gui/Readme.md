## .NET MAUI Application with Full MVVM, Validation, Data Fetching, and Automated Tests

Build a .NET MAUI application (project name: orgchart) that displays organization pay data using modern MVVM practices equivalent to WPF concepts: data binding, INotifyPropertyChanged, ICommand, data templates, control templates, resource dictionaries, and validation. The solution will include:
- A runtime project: orgchart (MAUI)
- A unit/integration test project: orgchart.Tests (NUnit) for models, services, and view models
- A UI automation test project: orgchart.UITests (Appium + NUnit) for end-to-end UI validation

Start with Windows as the initial target platform, then optionally expand to others.

### Recommended Execution Sequence 
Follow the numbered steps below as the authoritative definition of scope; however for faster feedback:
1. Do Steps 1–2 (data + model), then immediately write model validation tests (part of Step 10 early).
2. Do Step 3 (project setup) and add analyzers / EditorConfig now (items listed later in Step 12).
3. In Step 4 implement only MockDataService first; defer HttpDataService until after Step 5 + related ViewModel tests.
4. Implement Step 5 (ViewModel) and write ViewModel tests (subset of Step 10).
5. Build initial UI (Step 6) with AutomationIds early.
6. Add HttpDataService (remaining part of Step 4) + its tests.
7. Integrate fuller error handling (Step 8) as you wire HTTP; you already applied basic try/catch in the ViewModel earlier.
8. Finish remaining tests (Step 10), then proceed with UI automation (Step 11), quality gating (Step 12), optional features (Step 13).

---

### Step 1: Generate Sample JSON
Create a JSON array (10 rows) with:
- employeeID (int)
- departmentName (HR | QA | Engineering)
- firstName (string)
- lastName (string)
- hourlyRate (decimal, 20.00–50.00, normal-like distribution, σ ≈ 3.5)
Ensure:
- Proper UTF-8 encoding
- hourlyRate serialized with two decimals
- Sorted by departmentName then lastName
Use this both as seed mock data and for test fixtures (store under: assets/orgdata.json).

### Step 2: Define the Employee Model
Create an Employee class:
- Properties: EmployeeId, DepartmentName, FirstName, LastName, HourlyRate
- DataAnnotations: [Required], [Range], [StringLength], [RegularExpression] where appropriate
- Serializable via System.Text.Json (use [JsonPropertyName] if naming divergence required)
- Implement a Validate() helper that:
  - Uses Validator.TryValidateObject
  - Returns a collection of ValidationResult
Do not rely on implicit UI auto-validation—explicitly invoke validation in ViewModel.

Early Test Tip: After this step, create initial NUnit tests to verify valid vs invalid employee instances (start Step 10 early).

### Step 3: Create the orgchart MAUI Project
- Use the .NET MAUI App template (e.g., net8.0 target)
- Enable nullable reference types
- Add folders: Models, ViewModels, Services, Validation, Resources/Styles, Assets
- Add assets/orgdata.json (Content, Copy if newer)
- Add EditorConfig and enable analyzers early (TreatWarningsAsErrors where feasible)
- Register services in MauiProgram (initially MockDataService + IValidationService; defer HttpDataService)
- Add logging: builder.Logging.AddDebug()

### Step 4: Implement Services
Implement in two passes:
Pass 1 (immediate – supports ViewModel tests):
1. IDataService
   - Task<IReadOnlyList<Employee>> GetEmployeesAsync(CancellationToken ct)
2. MockDataService (design-time & tests)
   - Reads embedded or file-based orgdata.json
3. IValidationService / DataAnnotationsValidationService
   - Validates a single object or property
   - Returns structured errors (key + messages)

Pass 2 (after Step 5 + ViewModel tests):
4. HttpDataService (production)
   - GET https://localhost:6543/orgdata
   - Uses HttpClient (named client) with timeout
   - Throws detailed exceptions (include classification)
   - Validates JSON payload shape before mapping

### Step 5: ViewModel Layer
EmployeeListViewModel:
- ObservableCollection<Employee> Employees
- ObservableCollection<ValidationResult> ValidationErrors (or custom structure)
- ICommand RefreshCommand
- IsBusy (bool), ErrorMessage (string?)
Load logic:
- Sets IsBusy
- Calls data service
- Validates each Employee; invalid entries collected into ValidationErrors
- Populates Employees only with valid items
- Wrap service call with basic try/catch now (expanded in Step 8)
Implements INotifyPropertyChanged (use a BaseViewModel with SetProperty helper)
Testing: After implementation, write tests for success path, invalid employee filtering, and simulated failure (using a stub service that throws).

### Step 6: UI (XAML)
MainPage.xaml:
- Resource dictionaries for colors, spacing, text styles
- DataTemplate for Employee item row
- CollectionView bound to Employees
- Refresh UI (RefreshView or Button) bound to RefreshCommand / IsBusy
- Validation summary area (if ValidationErrors.Count > 0)
- ControlTemplate (optional) for a consistent page frame
Accessibility:
- AutomationProperties.Name on interactive elements
Automation IDs (add now for UI tests):
- CollectionView: AutomationId="EmployeesList"
- Refresh control/button: AutomationId="RefreshButton"
- Error banner: AutomationId="ErrorBanner"
- Validation summary: AutomationId="ValidationSummary"

### Step 7: Localhost HTTPS Support
Ensure the dev certificate is trusted before enabling HttpDataService:
- Run: dotnet dev-certs https --trust
If using a mock test server for integration tests, expose the same endpoint signature: /orgdata

### Step 8: Error Handling & Resilience
Enhance earlier basic try/catch with:
- Distinct handling for HttpRequestException, TaskCanceledException, JsonException, Validation issues
- User-friendly ErrorMessage mapping
- CancellationToken support in RefreshCommand
- Avoid blocking UI thread; all network + parsing async

### Step 9: Performance Considerations
- Use ObservableCollection only for UI-bound mutable lists (avoid wholesale reassignment)
- Stream-based deserialization (await JsonSerializer.DeserializeAsync)
- Avoid synchronous .Result / .Wait()
- Validate items as they’re added (avoid large post-processing pass)
- Consider caching (optional future enhancement) only after baseline complete

### Step 10: Testing (NUnit)
Project: orgchart.Tests
Add tests progressively (some created early in Steps 2, 4, 5):
1. Model Validation
   - Valid employee passes
   - Out-of-range HourlyRate fails
   - Missing DepartmentName fails
2. Data Service
   - MockDataService returns expected count
   - Corrupted JSON throws JsonException
   - HttpDataService handles:
     - 200 OK valid payload
     - 500 server error (assert exception or classification)
     - Malformed JSON
     - Cancellation
3. ViewModel
   - RefreshCommand populates Employees
   - Invalid entries excluded & surfaced in ValidationErrors
   - Network exception sets ErrorMessage
Testing Notes:
- Use mocked HttpMessageHandler (no real network)
- Embed JSON fixtures (Copy to Output or EmbeddedResource)

### Step 11: UI Automation (Appium)
Project: orgchart.UITests
- Appium + WinAppDriver (Windows)
Scenarios:
- Initial load shows expected row count
- Simulated server error shows error banner (ErrorBanner)
- Malformed data scenario shows ValidationSummary
Tag tests with [Category("UI")]

### Step 12: Continuous Validation of Code Quality
- Analyzers (TreatWarningsAsErrors) – ideally already enabled (Step 3)
- EditorConfig (indent size 2, naming rules)
- Verify only required usings included
- All public methods have XML documentation
- Private fields prefixed with _
- dotnet format as part of CI
- Code coverage target (e.g., ≥85% non-UI assemblies)

### Step 13: Extensibility (Optional Enhancements)
- Filtering by Department (Picker bound to distinct Department list)
- Average hourly rate summary (computed property)
- Theming (light/dark resource dictionaries)
- Cancellation improvements for long-running tasks
- Offline cache of last known data

### Prompting Guidance (Copilot Usage)
Examples:
- "Generate Employee model with DataAnnotations and Validate method returning IList<ValidationResult>."
- "Create IDataService and MockDataService reading local JSON file asynchronously."
- "Implement HttpDataService with HttpClient and JSON error handling."
- "Scaffold an EmployeeListViewModel with RefreshCommand and validation filtering."
- "Write NUnit tests for HttpDataService error scenarios."
- "Generate Appium UI test locating EmployeesList by AutomationId."

### Completion Criteria Checklist
- Employee model validated & serializable
- Mock + HTTP data services implemented & tested
- ViewModel supports async loading, validation filtering, error reporting
- UI binds correctly with AutomationIds and accessibility metadata
- Unit tests (model, services, view model) passing
- UI tests (basic load + error + validation scenarios) passing
- No blocking calls (no .Result / .Wait())
- Localhost HTTPS works (dev cert trusted)
- Coding standards enforced (naming, XML docs, private field underscores)
- Analyzer warnings resolved
- ValidationErrors populated correctly for invalid data
- Logging present for network and validation issues

### Optional Future Considerations
- Introduce INotifyDataErrorInfo for per-field validation feedback
- Add localization via resource files
- Add retry policies (Polly) around transient HTTP failures
- Add telemetry (Application Insights) if scaling to production

---

Execution Notes:
- Keep commits small and feature-scoped.
- Run dotnet test frequently (fast feedback).
- Fail fast: if validation fails, log and exclude item; do not silently continue without traceability.
