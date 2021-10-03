using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    public DayNightCycleBehaviour time;

    public Text timetext;
    public Text amtext;
    public Text monthtext;
    public Text daytext;

    public Image Window;

    [SerializeField]
    private Sprite[] springWindows;

    [SerializeField]
    private Sprite[] summerWindows;

    [SerializeField]
    private Sprite[] autumnWindows;

    [SerializeField]
    private Sprite[] winterWindows;

    private Sprite currWindowSprite;

    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<DayNightCycleBehaviour>();

        time.t_timeChange.AddListener(timeChange);
        time.t_lightChange.AddListener(lightChange);
        time.t_monthChange.AddListener(monthChange);
        time.t_dayChange.AddListener(dayChange);
        time.t_seasonChange.AddListener(seasonChange);

        switch (time.season)
        {
            case Season.SPRIMMER:
                if (time.isDay)
                {
                    currWindowSprite = springWindows[0];
                }
                else
                {
                    currWindowSprite = springWindows[1];
                }
                break;
            case Season.SUMTUMN:
                if (time.isDay)
                {
                    currWindowSprite = summerWindows[0];
                }
                else
                {
                    currWindowSprite = summerWindows[1];
                }
                break;
            case Season.AUNTER:
                if (time.isDay)
                {
                    currWindowSprite = autumnWindows[0];
                }
                else
                {
                    currWindowSprite = autumnWindows[1];
                }
                break;
            case Season.WINTING:
                if (time.isDay)
                {
                    currWindowSprite = winterWindows[0];
                }
                else
                {
                    currWindowSprite = winterWindows[1];
                }
                break;
            default:
                if (time.isDay)
                {
                    currWindowSprite = springWindows[0];
                }
                else
                {
                    currWindowSprite = springWindows[1];
                }
                break;
        }

        Window.sprite = currWindowSprite;

    }


    void timeChange()
    {
        int minutes = (int)time.relativeTime;
        int hours = minutes / 60;
        minutes -= hours * 60;

        if(hours > 12)
        {
            hours -= 12;
        }

        if (time.relativeTime >= 1440 / 2)
        {
            amtext.text = "PM";
        }
        else
        {
            amtext.text = "AM";
        }

        timetext.text = hours.ToString("00") + ":" + minutes.ToString("00");
    }
    void lightChange()
    {
        switch (time.season)
        {
            case Season.SPRIMMER:
                if (time.isDay)
                {
                    currWindowSprite = springWindows[0];
                }
                else
                {
                    currWindowSprite = springWindows[1];
                }
                break;
            case Season.SUMTUMN:
                if (time.isDay)
                {
                    currWindowSprite = summerWindows[0];
                }
                else
                {
                    currWindowSprite = summerWindows[1];
                }
                break;
            case Season.AUNTER:
                if (time.isDay)
                {
                    currWindowSprite = autumnWindows[0];
                }
                else
                {
                    currWindowSprite = autumnWindows[1];
                }
                break;
            case Season.WINTING:
                if (time.isDay)
                {
                    currWindowSprite = winterWindows[0];
                }
                else
                {
                    currWindowSprite = winterWindows[1];
                }
                break;
            default:
                if (time.isDay)
                {
                    currWindowSprite = springWindows[0];
                }
                else
                {
                    currWindowSprite = springWindows[1];
                }
                break;
        }
        Window.sprite = currWindowSprite;
    }

    void dayChange()
    {
        daytext.text = (time.dayCount +1 ).ToString("00");
    }
    void monthChange()
    {
        monthtext.text = time.getMonth();
    }
    void seasonChange()
    {
        switch (time.season)
        {
            case Season.SPRIMMER:
                if (time.isDay)
                {
                    currWindowSprite = springWindows[0];
                }
                else
                {
                    currWindowSprite = springWindows[1];
                }
                break;
            case Season.SUMTUMN:
                if (time.isDay)
                {
                    currWindowSprite = summerWindows[0];
                }
                else
                {
                    currWindowSprite = summerWindows[1];
                }
                break;
            case Season.AUNTER:
                if (time.isDay)
                {
                    currWindowSprite = autumnWindows[0];
                }
                else
                {
                    currWindowSprite = autumnWindows[1];
                }
                break;
            case Season.WINTING:
                if (time.isDay)
                {
                    currWindowSprite = winterWindows[0];
                }
                else
                {
                    currWindowSprite = winterWindows[1];
                }
                break;
            default:
                if (time.isDay)
                {
                    currWindowSprite = springWindows[0];
                }
                else
                {
                    currWindowSprite = springWindows[1];
                }
                break;
        }
        Window.sprite = currWindowSprite;
    }
}
