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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

// Seasons, in chunks of 3 months
public enum Season { UNDEFINED, SPRIMMER, SUMTUMN, AUNTER, WINTING };

// Months, each chunk of three corresponds to the above seasons
public enum MonthName { UNDEFINED, Janril, Febrarch, Maruary, Aprember, Mayne, Junember, Julober, Augember, Septuary, Octobust, Novay, Decly };

public class DayNightCycleBehaviour : MonoBehaviour
{
    [Header("Game Settings")]
    // Length of each game day (day and night)
    public float gameDayLength = 720.0f;
    // Midday Light colour - Pure white
    public Color dayColor = new Color(1.0f, 1.0f, 1.0f);
    // Midnight Light colour - Dark / muted colours, emphasis on blue tones
    public Color nightColor = new Color(0.25f, 0.25f, 0.6f);
    public int monthLength;


    [Header("Initial Variables")]
    public float initTime;
    public int initDay;
    public int initYear;
    public Season season;
    public MonthName month;

    [Header("Game Objects")]
    public Tilemap[] summer_tiles;
    public Tilemap[] spring_tiles;

    [Header("Monitored Variables")]
    [SerializeField]
    public float time;
    public float relativeTime;

    public int dayCount;
    public int yearCount;

    public bool isDay;
    public bool isNight;

    [SerializeField]
    private GameObject spawningZone;

    // Global light source
    private Light2D timelight;

    // Lightsource Gradients
    private Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;

    // Seasonal Tilemap Opacity Gradients
    private Color neutralColor = new Color(1.0f, 1.0f, 1.0f);
    private Gradient seasonGradient;
    private GradientColorKey[] seasonKey;
    private GradientAlphaKey[] seasonalAlpha;

    private int totalDayCount;
    private int seasonalDayCount;
    private float localTimeElapsed;
    private int yearLength;

    [HideInInspector]
    public UnityEvent t_timeChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent t_lightChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent t_dayChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent t_monthChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent t_seasonChange = new UnityEvent();

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

        if (initDay != 0 && initDay <= monthLength)
        { 
            dayCount += initDay - 1;
            TotalDayCount += initDay;
        } else
        {
            dayCount = initDay % monthLength;
        }
        t_dayChange.Invoke();

        evalSeason();

        evalSeasonGradient();

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
        t_lightChange.Invoke();
    }

    List<float> uniqueTime = new List<float>();

    public int TotalDayCount { get => totalDayCount; set => totalDayCount = value; }

    // Update is called once per frame
    void Update()
    {
        localTimeElapsed += Time.deltaTime;

        float timeRatio = localTimeElapsed / gameDayLength;

        time = (float)Math.Round((double)timeRatio, 2);

        relativeTime = (float)Math.Round((double)timeRatio*1440, 0);

        if (!uniqueTime.Contains(relativeTime))
        {
            uniqueTime.Add(relativeTime);
            t_timeChange.Invoke();
        }


        //0 || 1 = midnight, 0.25 = sunrise, 0.5 = midday, 0.75 = sunset
        if ( time >= 0.25 && time <= 0.75 && !isDay)
        {
            isNight = false;
            isDay = true;
            t_lightChange.Invoke();
            spawningZone.SetActive(false);
        } else if ((time < 0.25 || time > 0.75) && !isNight)
        {
            isNight = true;
            isDay = false;
            t_lightChange.Invoke();
            spawningZone.SetActive(true);
        }

        timelight.color = gradient.Evaluate(timeRatio);


        if (localTimeElapsed >= gameDayLength)
        {
            dayIncrease();
            t_dayChange.Invoke();
        }
        
    }

    void dayIncrease()
    {
        dayCount++;
        TotalDayCount++;
        seasonalDayCount++;

        uniqueTime.Clear();


        localTimeElapsed = 0;

        if (dayCount == monthLength)
        {
            int m = (int)month;
            m++;


            m = MonthIncrease(m);

            month = (MonthName)m;

            t_monthChange.Invoke();

            dayCount = 0;

            evalSeason();
        }

        evalSeasonGradient();

        if (TotalDayCount >= yearLength)
        {
            yearLength++;
            TotalDayCount = 0;
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

    void evalSeason()
    {
        if ((int)month >= 1 && (int)month <= 3)
        {
            if ((int)month == 1)
            {
                seasonalDayCount = 0;
            }
            setSeason(Season.SPRIMMER);
            t_seasonChange.Invoke();
        }
        else if ((int)month >= 4 && (int)month <= 6)
        {
            if ((int)month == 4)
            {
                seasonalDayCount = 0;
            }
            setSeason(Season.SUMTUMN);
            t_seasonChange.Invoke();
        }
        else if ((int)month >= 7 && (int)month <= 9)
        {
            if ((int)month == 7)
            {
                seasonalDayCount = 0;
            }
            setSeason(Season.AUNTER);
            t_seasonChange.Invoke();
        }
        else if ((int)month >= 10 && (int)month <= 12)
        {
            if ((int)month == 10)
            {
                seasonalDayCount = 0;
            }
            setSeason(Season.WINTING);
            t_seasonChange.Invoke();
        }
        else
        {
            setSeason(Season.SPRIMMER);
            t_seasonChange.Invoke();
        }
    }

    void evalSeasonGradient()
    {
        float g = (float)seasonalDayCount / (3 * monthLength);
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
    }


    void setSeason(Season s)
    {
        season = s;
    }

    public String getMonth()
    {
        String shortMon = month.ToString().Substring(0, 3);
        return shortMon;
    }

}
