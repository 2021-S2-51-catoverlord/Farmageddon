using UnityEngine;
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
    public void Start()
    {
        time = FindObjectOfType<DayNightCycleBehaviour>();

        time.t_timeChange.AddListener(TimeChange);
        time.t_lightChange.AddListener(LightChange);
        time.t_monthChange.AddListener(MonthChange);
        time.t_dayChange.AddListener(DayChange);
        time.t_seasonChange.AddListener(SeasonChange);

        LightChange();
        TimeChange();
        MonthChange();
        DayChange();
        SeasonChange();
    }

    private void TimeChange()
    {
        int minutes = (int)time.relativeTime;
        int hours = minutes / 60;
        minutes -= hours * 60;

        if(hours > 12)
        {
            hours -= 12;
        }

        if(time.relativeTime >= 1440 / 2)
        {
            amtext.text = "PM";
        }
        else
        {
            amtext.text = "AM";
        }

        timetext.text = hours.ToString("00") + ":" + minutes.ToString("00");
    }

    private void LightChange()
    {
        switch(time.season)
        {
            case Season.SPRIMMER:
                if(time.isDay)
                {
                    currWindowSprite = springWindows[0];
                }
                else
                {
                    currWindowSprite = springWindows[1];
                }
                break;
            case Season.SUMTUMN:
                if(time.isDay)
                {
                    currWindowSprite = summerWindows[0];
                }
                else
                {
                    currWindowSprite = summerWindows[1];
                }
                break;
            case Season.AUNTER:
                if(time.isDay)
                {
                    currWindowSprite = autumnWindows[0];
                }
                else
                {
                    currWindowSprite = autumnWindows[1];
                }
                break;
            case Season.WINTING:
                if(time.isDay)
                {
                    currWindowSprite = winterWindows[0];
                }
                else
                {
                    currWindowSprite = winterWindows[1];
                }
                break;
            default:
                if(time.isDay)
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

    private void DayChange()
    {
        daytext.text = (time.dayCount + 1).ToString("00");
    }

    private void MonthChange()
    {
        monthtext.text = time.GetMonth();
    }

    private void SeasonChange()
    {
        switch(time.season)
        {
            case Season.SPRIMMER:
                if(time.isDay)
                {
                    currWindowSprite = springWindows[0];
                }
                else
                {
                    currWindowSprite = springWindows[1];
                }
                break;
            case Season.SUMTUMN:
                if(time.isDay)
                {
                    currWindowSprite = summerWindows[0];
                }
                else
                {
                    currWindowSprite = summerWindows[1];
                }
                break;
            case Season.AUNTER:
                if(time.isDay)
                {
                    currWindowSprite = autumnWindows[0];
                }
                else
                {
                    currWindowSprite = autumnWindows[1];
                }
                break;
            case Season.WINTING:
                if(time.isDay)
                {
                    currWindowSprite = winterWindows[0];
                }
                else
                {
                    currWindowSprite = winterWindows[1];
                }
                break;
            default:
                if(time.isDay)
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
