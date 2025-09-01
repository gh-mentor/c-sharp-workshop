
This exercise starts with an empty Visual Studio solution. You will be responsible for adding the recommended projects and implementing changes. Start with a single platform (e.g., Windows) to simplify testing. Once the app works as expected, expand to other platforms.

Use Copilot to assist in completing the application using following steps:


1) Generate a JSON array containing 10 rows of sample data. Each row should include the following keys: 
   - employeeID (integer)
   - departmentName (string)
   - firstName (string)
   - lastName (string)
   - hourlyRate (decimal, ranging from $20.00 to $50.00 with a standard deviation of $3.50)
   The departments should be limited to 'HR', 'QA', and 'Engineering'. Ensure the data is formatted for efficient consumption by a web application.

2) Define a C# model to represent the data described in Step 1. 
   - Include properties for each key in the JSON data.
   - Ensure the model follows best practices for data representation.
   - Design the model to support easy serialization and deserialization.
   - Include validation attributes where appropriate to ensure data integrity.

3) Create a .NET C# project named 'orgchart' in the active solution using the .NET MAUI template in Visual Studio. 
   - Set up the project to display JSON data representing salary distributions within an organization, as described in Step 1.
   - Configure the app to consume the data asynchronously using the GET method over `https://localhost:6543/orgdata`.
   - Optimize the app to render the data asynchronously for better performance.


4) Add the model defined in Step 2 to the 'orgchart' project. 
   - Integrate the model to represent the data structure for the JSON data consumed by the app.
   - Verify that the model supports serialization and deserialization.

5) Implement the functionality to fetch and display the JSON data in the 'orgchart' project. 
   - Use asynchronous programming to fetch the data from the endpoint.
   - Bind the data to the UI for rendering.

6) Create an integration test project named 'orgchart.tests' for the 'orgchart' project using the xUnit Visual Studio project template. 
   - Include tests to validate the integration of the `Employee` model with the app.
   - Test scenarios should include:
     - Successful data retrieval and binding to the UI.
     - Handling invalid or incomplete JSON data.
     - Error handling for network issues or server errors.
   - Use mock data or a mock API endpoint to simulate the JSON data described in Step 1.
   - Follow best practices for integration testing, including testing edge cases and error handling.





