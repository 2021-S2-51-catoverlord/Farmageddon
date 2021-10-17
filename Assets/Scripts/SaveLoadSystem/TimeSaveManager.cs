using System.IO;
using UnityEngine;

public class TimeSaveManager : MonoBehaviour, ISaveable
{
    protected string TimeDataPath;
    public DayNightCycleBehaviour TimeController;

    public void Awake()
    {
        TimeDataPath = $"{Application.persistentDataPath}/Time.dat";

        if(TimeController == null)
        {
            TimeController = GetComponent<DayNightCycleBehaviour>();
        }
    }

    /// <summary>
    /// Method to save current time data to file.
    /// </summary>
    public void SaveData()
    {
        FileIO.WriteBinToFile(TimeDataPath, new TimeSaveData(TimeController));

        Debug.Log($"Time data loaded from: {TimeDataPath}");
    }

    /// <summary>
    /// Method to load time data stored in the time file onto dayNightCycleBehaviour
    /// class which will cascade the new changes throughout the game via its listeners. 
    /// </summary>
    public void LoadData()
    {
        TimeSaveData loadedData = null;

        if(File.Exists(TimeDataPath))
        {
            loadedData = FileIO.ReadBinFromFile<TimeSaveData>(TimeDataPath);
        }

        if(loadedData != null)
        {
            ReconstructTimeData(loadedData);
            Debug.Log($"Time data loaded from: {TimeDataPath}");
        }
        else
        {
            Debug.Log("DayNightCycleBehaviour's saved data is currently unavailable.");
        }
    }

    /// <summary>
    /// Helper method to deserialise information and load 
    /// it straight onto the DayNightCycleBehaviour class.
    /// </summary>
    /// <param name="loadedData"></param>
    private void ReconstructTimeData(TimeSaveData loadedData)
    {
        LoadCycles(loadedData);
        LoadInitialValues(loadedData);
        LoadEnums(loadedData);
        LoadCurrentValues(loadedData);
        LoadToggleDayNight(loadedData);
        LoadBackgroundValues(loadedData);
    }

    private void LoadCycles(TimeSaveData loadedData)
    {
        TimeController.gameDayLength = loadedData.GameDayLength;
        TimeController.monthLength = loadedData.MonthLength;
    }

    private void LoadInitialValues(TimeSaveData loadedData)
    {
        TimeController.initTime = loadedData.InitTime;
        TimeController.initDay = loadedData.InitDay;
        TimeController.initYear = loadedData.InitYear;
    }

    private void LoadEnums(TimeSaveData loadedData)
    {
        TimeController.season = (Season)loadedData.SeasonIndex;
        TimeController.month = (MonthName)loadedData.MonthIndex;
    }

    private void LoadCurrentValues(TimeSaveData loadedData)
    {
        TimeController.time = loadedData.Time;
        TimeController.relativeTime = loadedData.RelativeTime;
        TimeController.dayCount = loadedData.DayCount;
        TimeController.yearCount = loadedData.YearCount;
    }

    private void LoadToggleDayNight(TimeSaveData loadedData)
    {
        TimeController.isDay = loadedData.IsDay;
        TimeController.isNight = loadedData.IsNight;
    }

    private void LoadBackgroundValues(TimeSaveData loadedData)
    {
        TimeController.SetBackgroundValues(loadedData);
    }
}
