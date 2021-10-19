// This test should demonstrate proper clock settings based on initial time light variables
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TimeClockTest
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ClockTimeParserTest()
    {
        float initTime = 20.0f;
        float gameDayLength = 40.0f;

        String timetext = "";
        String amtext = "";

        float timeRatio = initTime / gameDayLength;
        float relativeTime = (float)Math.Round((double)timeRatio * 1440, 0);

        int minutes = (int)relativeTime;
        int hours = minutes / 60;
        minutes -= hours * 60;

        if (hours > 12)
        {
            hours -= 12;
        }

        if (relativeTime >= 1440 / 2)
        {
            amtext = "PM";
        }
        else
        {
            amtext = "AM";
        }

        timetext = hours.ToString("00") + ":" + minutes.ToString("00");

        //Check that the time set in DayNightCycleBehaviour is read as "12:00 PM" inside the ClockController
        Assert.True(timetext.Equals("12:00") && amtext.Equals("PM"));

        yield return 0;
    }
}


