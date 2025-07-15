# Assembly Line Simulation

In this exercise, you will implement all methods of the `FactoryAssemblyLine` class. The `FactoryAssemblyLine` class is a minimal simulation of an assembly line in a factory. The `tests` folder contains completed NUnit tests that will validate your implementation.

The goal of this exercise is to implement the `FactoryAssemblyLine` class so that all tests pass.

## Instructions

1. **Class Overview**: The `FactoryAssemblyLine` class represents an assembly line with multiple stations. Each station has a unique ID, a processing time, and an active/inactive state.

2. **Required Methods**:
   - `AddStation(int stationId, int processingTime)`: Adds a station with the specified ID and processing time.
   - `RemoveStation(int stationId)`: Removes the station with the specified ID.
   - `StartAssembly(int stationId)`: Activates the station with the specified ID.
   - `StopAssembly(int stationId)`: Deactivates the station with the specified ID.
   - `GetProcessingTime(int stationId)`: Returns the processing time of the specified station.
   - `GetTotalProcessingTime()`: Returns the total processing time of all active stations.
   - `GetNumStations()`: Returns the total number of stations.
   - `GetNumActiveStations()`: Returns the number of active stations.
   - `GetNumInactiveStations()`: Returns the number of inactive stations.
   - `IsStationActive(int stationId)`: Returns `true` if the specified station is active, otherwise `false`.

3. **Error Handling**:
   - Throw an `ArgumentException` if invalid arguments are provided (e.g., negative station IDs or processing times).
   - Throw an `InvalidOperationException` if attempting to add a duplicate station or remove/start/stop a non-existent station.

4. **Implementation Details**:
   - Use a dictionary to store station data, where the key is the station ID, and the value is an object containing the processing time and active state.
   - Ensure proper error handling and validation in all methods.
   - Use descriptive names for variables and methods, following .NET naming conventions.

5. **Testing**:
   - The `tests` folder contains NUnit test cases to validate your implementation.
   - Run the tests using the NUnit test runner in Visual Studio or your preferred IDE.
   - Ensure all tests pass before submitting your solution.



