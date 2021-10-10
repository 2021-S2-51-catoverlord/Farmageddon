/* Feature created by Macauley Cunningham (19072621)
/  This module will affect global 2D light levels in the scene. The module uses a defined gradient colour key based on 
/  the idea that darker, blue are reminiscent of night time. Through the deltaTime function, we can create a timer
/  which increments each second. We then set a limit to this timer, "dayLength", in seconds. As the timer increments,
/  we calculate the timeElapse to dayLength ratio to produce a "time" for the light colour key. This "time" will be at
/  its darkest at a value of 0 or 1, and at its lightest at 0.5. The day loops from 0 to 1 rounded to 2 decimal places.
/  This means that, from 0.25 onwards, it could be considered "daytime", and from 0.75 onwards, it is "nighttime".
/  An initial time can be set, which will be 360 (sunrise) on first starting the game. This will allow us to save the
/  current time and write it back into the game.
/
/  This code will need to be refactored to implement changing seasons, as length of day and night would differ. 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

// Seasons, in chunks of 3 months
public enum Season { UNDEFINED, SPRIMMER, SUMTUMN, AUNTER, WINTING };

// Months, each chunk of three corresponds to the above seasons
public enum MonthName { UNDEFINED, Janril, Febrarch, Maruary, Aprember, Mane, Junember, Julober, Augember, Septuary, Octobust, Novay, Decly };

public class DayNightCycleBehaviour : MonoBehaviour
{
    // Length of each game day (day and night)
    public float gameDayLength = 720.0f;

    public float initTime;
    public int initDay;
    public int initMonth;
    public int initYear;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    // Midday Light colour - Pure white
    public Color dayColor = new Color(1.0f, 1.0f, 1.0f);
    // Midnight Light colour - Dark / muted colours, emphasis on blue tones
    public Color nightColor = new Color(0.25f, 0.25f, 0.6f);
    // Global light source
    private Light2D timelight;

    public Tilemap[] summer_tiles;
    public Tilemap[] spring_tiles;

    public Season season;
    public MonthName month;
    public int monthLength;
    private int yearLength;

    private Gradient seasonGradient;
    private GradientColorKey[] seasonKey;
    private GradientAlphaKey[] seasonalAlpha;

    public Color neutralColor = new Color(1.0f, 1.0f, 1.0f);

    public float timeElapsed { get; private set; }

    public int dayCount { get; private set; }
    private int totalDayCount;
    private int seasonalDayCount;
    public int yearCount { get; private set; }
    public float g;//debugging
    public float localTimeElapsed { get; private set; }
    public int TotalDayCount { get => totalDayCount; set => totalDayCount = value; }

    public float time;

    public bool isDay;
    public bool isNight;

 // Start is called before the first frame update
void Start()
    {
        timelight = GetComponent<Light2D>();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        gradient = new Gradient();
        colorKey = new GradientColorKey[3];
        colorKey[0].color = nightColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = dayColor;
        colorKey[1].time = 0.5f;
        colorKey[2].color = nightColor;
        colorKey[2].time = 1.0f;

        // Populate the alpha  keys at relative time 0
        alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;

        gradient.SetKeys(colorKey, alphaKey);

        seasonGradient = new Gradient();
        seasonKey = new GradientColorKey[2];

        seasonKey[0].color = neutralColor;
        seasonKey[0].time = 0.0f;
        seasonKey[1].color = neutralColor;
        seasonKey[1].time = 1.0f;

        seasonalAlpha = new GradientAlphaKey[2];
        seasonalAlpha[0].alpha = 0.0f;
        seasonalAlpha[0].time = 0.0f;
        seasonalAlpha[1].alpha = 1.0f;
        seasonalAlpha[1].time = 1.0f;

        seasonGradient.SetKeys(seasonKey, seasonalAlpha);

        yearLength = (Enum.GetNames(typeof(MonthName)).Length - 1) * monthLength;
        if (initTime != 0)
        {
            localTimeElapsed += initTime;
        } else
        {
            localTimeElapsed = 0;
        }

        if (initDay != 0)
        { 
            dayCount += initDay;
            totalDayCount += dayCount;
        } else
        {
            dayCount = 0;
        }

        if (initMonth != 0)
        {
            month = (MonthName)initMonth;
        } else
        {
            month = MonthName.Janril;
            season = Season.SPRIMMER;
        }

        float g = (float)totalDayCount / ((float)yearLength / 4);
        switch (season)
        {
            case Season.SPRIMMER:
                setSeasonGradient(spring_tiles, g);
                setSeasonGradient(summer_tiles, 0);
                break;
            case Season.SUMTUMN:
                setSeasonGradient(summer_tiles, g);
                setSeasonGradient(spring_tiles, 1 - g);
                break;
            case Season.AUNTER:
                setSeasonGradient(summer_tiles, 1 - g);
                setSeasonGradient(spring_tiles, 0);
                break;
            case Season.WINTING:
                setSeasonGradient(spring_tiles, 0);
                setSeasonGradient(summer_tiles, 0);
                break;
            case Season.UNDEFINED:
                break;
        }

        if (monthLength < 1)
        {
            monthLength = 30;
        }

        if ((float)Math.Round((double)(localTimeElapsed / gameDayLength), 2) < 0.25 || (float)Math.Round((double)(localTimeElapsed / gameDayLength), 2) >= 0.75)
        {
            isNight = true;
        } else
        {
            isDay = true;
        }


     

    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        localTimeElapsed += Time.deltaTime;

        float timeRatio = localTimeElapsed / gameDayLength;

        time = (float)Math.Round((double)timeRatio, 2);

        //0 || 1 = midnight, 0.25 = sunrise, 0.5 = midday, 0.75 = sunset
        if ( time == 0.25 )
        {
            isNight = false;
            isDay = true;
        } else if (time == 0.75)
        {
            isNight = true;
            isDay = false;
        }

        timelight.color = gradient.Evaluate(timeRatio);


        if (localTimeElapsed >= gameDayLength)
        {
            dayIncrease();
        }
        
    }

    void dayIncrease()
    {
        dayCount++;
        totalDayCount++;
        seasonalDayCount++;

        g = (float)seasonalDayCount / (3*monthLength);

        localTimeElapsed = 0;

        if (dayCount == monthLength)
        {
            int m = (int)month;
            m++;

            m = MonthIncrease(m);

            month = (MonthName)m;

            dayCount = 0;

            if ((int)month >= 1 && (int)month <= 3)
            {
                if ((int)month == 1)
                {
                    seasonalDayCount = 0;
                }
                setSeason(Season.SPRIMMER);
            }
            else if ((int)month >= 4 && (int)month <= 6)
            {
                if ((int)month == 4)
                {
                    seasonalDayCount = 0;
                }
                setSeason(Season.SUMTUMN);
            }
            else if ((int)month >= 7 && (int)month <= 9)
            {
                if ((int)month == 7)
                {
                    seasonalDayCount = 0;
                }
                setSeason(Season.AUNTER);
            }
            else if ((int)month >= 10 && (int)month <= 12)
            {
                if ((int)month == 10)
                {
                    seasonalDayCount = 0;
                }
                setSeason(Season.WINTING);
            }
            else
            {
                setSeason(Season.SPRIMMER);
            }
        }

        switch (season)
        {
            case Season.SPRIMMER:
                setSeasonGradient(spring_tiles, g);
                break;
            case Season.SUMTUMN:
                setSeasonGradient(summer_tiles, g);
                setSeasonGradient(spring_tiles, 1 - g);
                break;
            case Season.AUNTER:
                setSeasonGradient(summer_tiles, 1 - g);
                break;
            case Season.WINTING:
                break;
            case Season.UNDEFINED:
                break;
        }

        if (totalDayCount >= yearLength)
        {
            yearLength++;
            totalDayCount = 0;
        }
    }

    int MonthIncrease(int m)
    {

        if ((int)month == 12)
        {
            return 1;
        }
        else
        {
            return m;
        }
    }

    void setSeasonGradient(Tilemap[] t, float g)
    {
        foreach (Tilemap tm in t)
        {
            tm.color = seasonGradient.Evaluate(g);
        }
    }

    void setSeason(Season s)
    {
        if (g >= 1)
        {
            g = 0;
        }

        season = s;
    }

}
