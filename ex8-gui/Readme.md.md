## .NET MAUI Application with Full MVVM, Validation, Data Fetching, and Automated Tests

Build a .NET MAUI application (project name: orgchart) that displays organization pay data using modern MVVM practices equivalent to WPF concepts: data binding, INotifyPropertyChanged, ICommand, data templates, control templates, resource dictionaries, and validation. The solution will include:
- A runtime project: orgchart (MAUI)
- A unit/integration test project: orgchart.Tests (NUnit) for models, services, and view models
- A UI automation test project: orgchart.UITests (Appium + NUnit) for end-to-end UI validation

Start with Windows as the initial target platform, then optionally expand to others.

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

### Step 3: Create the orgchart MAUI Project
- Use the .NET MAUI App template
- Enable nullable reference types
- Add folders: Models, ViewModels, Services, Validation, Resources/Styles, Assets
- Add assets/orgdata.json (Content, Copy if newer)
- Register services in MauiProgram (HttpClient, IDataService, IValidationService)

### Step 4: Implement Services
1. IDataService
   - Task<IReadOnlyList<Employee>> GetEmployeesAsync(CancellationToken ct)
2. HttpDataService (production)
   - GET https://localhost:6543/orgdata
   - Uses HttpClient factory, throws detailed exceptions
   - Validates JSON payload shape before mapping
3. MockDataService (design-time & tests)
   - Reads embedded or file-based orgdata.json
4. IValidationService / DataAnnotationsValidationService
   - Validates a single object or property
   - Returns structured errors (key + messages)

### Step 5: ViewModel Layer
EmployeeListViewModel:
- ObservableCollection<Employee> Employees
- ICommand RefreshCommand
- IsBusy (bool), ErrorMessage (string?)
- Load logic:
  - Sets IsBusy
  - Calls data service
  - Validates each Employee; invalid entries collected into a separate ValidationErrors list
  - Populates Employees only with valid items
- Implements INotifyPropertyChanged (use a BaseViewModel with SetProperty helper)

### Step 6: UI (XAML)
MainPage.xaml:
- Resource dictionaries for colors, spacing, text styles
- DataTemplate for Employee item row
- CollectionView bound to Employees
- RefreshView bound to RefreshCommand / IsBusy
- Validation summary area (if ValidationErrors.Count > 0)
- ControlTemplate (optional) for a consistent page frame
Accessibility:
- AutomationProperties.Name on interactive elements

### Step 7: Localhost HTTPS Support
Ensure the dev certificate is trusted:
- Run: dotnet dev-certs https --trust
If using a mock test server for integration tests, expose the same endpoint signature: /orgdata

### Step 8: Error Handling & Resilience
- Wrap fetch logic with try/catch (HttpRequestException, TaskCanceledException, JsonException)
- Show user-friendly ErrorMessage
- Use CancellationToken in RefreshCommand to allow abort on navigation
- Avoid blocking UI thread; all network and parsing async

### Step 9: Performance Considerations
- Use ObservableCollection only for UI-bound mutable lists (avoid reassigning whole collection)
- Deserialize with streams (await JsonSerializer.DeserializeAsync)
- Avoid synchronous .Result or .Wait()
- Keep validation outside UI thread heavy loops (validate as you add)

### Step 10: Testing (NUnit)
Project: orgchart.Tests
Test categories:
1. Model Validation
   - Valid employee passes
   - Out-of-range HourlyRate fails
   - Missing DepartmentName fails
2. Data Service
   - MockDataService returns 10 employees
   - Corrupted JSON throws JsonException
3. ViewModel
   - RefreshCommand populates Employees
   - Invalid entries are excluded & surfaced in ValidationErrors
   - Network exception sets ErrorMessage
Use mock HttpMessageHandler for HttpDataService tests (no real network).
Embed test JSON fixtures (Copy to Output or as EmbeddedResource).

### Step 11: UI Automation (Appium)
Project: orgchart.UITests
- Use Appium + WinAppDriver (Windows platform)
- Launch MAUI WinUI build
Scenarios:
- Initial load shows 10 rows
- Refresh after mock server error shows error banner
- Invalid data scenario (inject malformed JSON) shows validation summary
Tag tests with [Category("UI")]

### Step 12: Continuous Validation of Code Quality
- Enable analyzers (TreatWarningsAsErrors true for core warnings)
- Add EditorConfig for consistent style (indent size 2)
- Verify only required usings included
- All public methods have XML documentation
- Private fields prefixed with _

### Step 13: Extensibility (Optional Enhancements)
- Add filtering by Department (Picker bound to Departments collection)
- Add average hourly rate summary (computed property)
- Add theming (light/dark resource dictionaries)
- Add cancellation token to RefreshCommand for long-running expansions

### Prompting Guidance (Copilot Usage)
Examples:
- "Generate Employee model with DataAnnotations and Validate method returning IList<ValidationResult>."
- "Create IDataService and HttpDataService using HttpClient and System.Text.Json with error handling."
- "Scaffold an EmployeeListViewModel with RefreshCommand, validation, and ObservableCollection."
- "Write NUnit tests for Employee validation edge cases."
- "Generate Appium test to assert 10 employee rows appear on main page."

### Completion Criteria Checklist
- Model: Employee with validation + serialization ready
- Services: Mock + HTTP implementations
- ViewModel: Async loading, validation, error reporting
- UI: DataTemplate, RefreshView, error + validation summaries
- Tests: Model, service, view model (NUnit) all passing
- UI Tests: Basic load + error scenarios automated
- No blocking calls, all async
- Localhost HTTPS works
- Coding standards (naming, XML docs, private field underscores) enforced





