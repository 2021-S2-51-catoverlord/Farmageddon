using System;

[Serializable]
public class TimeSaveData
{
    public float GameDayLength;
    public int MonthLength;

    public float InitTime;
    public int InitDay, InitYear;

    public int SeasonIndex, MonthIndex;
    public float Time, RelativeTime;
    public int DayCount, YearCount;
    public bool IsDay, IsNight;

    public int TotalDayCount, SeasonalDayCount, YearLength;
    public float LocalTimeElapsed;

    /// <summary>
    /// Default constructor for the use of time data's persistence.
    /// </summary>
    /// <param name="timeController"></param>
    public TimeSaveData(DayNightCycleBehaviour timeController)
    {
        SetCycles(timeController);
        SetInitialValues(timeController);
        SetEnums(timeController);
        SetCurrentValues(timeController);
        ToggleDayNight(timeController);
        SetBackgroundValues(timeController);
    }

    private void SetCycles(DayNightCycleBehaviour timeController)
    {
        GameDayLength = timeController.gameDayLength;
        MonthLength = timeController.monthLength;
    }

    private void SetInitialValues(DayNightCycleBehaviour timeController)
    {
        InitTime = timeController.initTime;
        InitDay = timeController.initDay;
        InitYear = timeController.initYear;
    }

    private void SetEnums(DayNightCycleBehaviour timeController)
    {
        SeasonIndex = (int)timeController.season;
        MonthIndex = (int)timeController.month;
    }

    private void SetCurrentValues(DayNightCycleBehaviour timeController)
    {
        Time = timeController.time;
        RelativeTime = timeController.relativeTime;
        DayCount = timeController.dayCount;
        YearCount = timeController.yearCount;
    }

    private void ToggleDayNight(DayNightCycleBehaviour timeController)
    {
        IsDay = timeController.isDay;
        IsNight = timeController.isNight;
    }

    private void SetBackgroundValues(DayNightCycleBehaviour timeController)
    {
        timeController.GetBackgroundValues(this);
    }
}
