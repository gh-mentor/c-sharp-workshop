using System;
using System.Collections.Generic;

public class FactoryAssemblyLine : IFactoryAssemblyLine
{
  private readonly Dictionary<int, Station> _stations;
  private readonly int _numStations;
  private int _numActiveStations;

  public FactoryAssemblyLine(int numStations)
  {
    if (numStations < 0)
    {
      throw new ArgumentException("Number of stations cannot be negative");
    }

    _numStations = numStations;
    _stations = new Dictionary<int, Station>();
    _numActiveStations = 0;
  }

  public void AddStation(int stationId, int processingTime)
  {
    throw new NotImplementedException();
  }

  public void RemoveStation(int stationId)
  {
    throw new NotImplementedException();
  }

  public void StartAssembly(int stationId)
  {
    throw new NotImplementedException();
  }

  public void StopAssembly(int stationId)
  {
    throw new NotImplementedException();
  }

  public int GetProcessingTime(int stationId)
  {
    throw new NotImplementedException();
  }

  public int GetTotalProcessingTime()
  {
    throw new NotImplementedException();
  }

  public int GetNumStations()
  {
    throw new NotImplementedException();
  }

  public int GetNumActiveStations()
  {
    throw new NotImplementedException();
  }

  public int GetNumInactiveStations()
  {
    throw new NotImplementedException();
  }

  public bool IsStationActive(int stationId)
  {
    throw new NotImplementedException();
  }

  private class Station
  {
    public int ProcessingTime { get; set; }
    public bool IsActive { get; set; }
  }
}