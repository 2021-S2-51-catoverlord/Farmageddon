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
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycleBehaviour : MonoBehaviour
{
    // Length of each game day (day and night)
    public float gameDayLength = 720.0f;

    public float initTime;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    // Midday Light colour - Pure white
    public Color dayColor = new Color(1.0f, 1.0f, 1.0f);
    // Midnight Light colour - Dark / muted colours, emphasis on blue tones
    public Color nightColor = new Color(0.25f, 0.25f, 0.6f);
    // Global light source
    private Light2D light2D;

    public float timeElapsed { get; private set; }

    public int dayCount { get; private set; }

    public float localTimeElapsed { get; private set; }

    public float time;

    public bool isDay;
    public bool isNight;

    // Start is called before the first frame update
    void Start()
    {
        localTimeElapsed += initTime;

        if ((float)Math.Round((double)(localTimeElapsed / gameDayLength), 2) < 0.25 || (float)Math.Round((double)(localTimeElapsed / gameDayLength), 2) >= 0.75)
        {
            isNight = true;
        } else
        {
            isDay = true;
        }


            light2D = GetComponent<Light2D>();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        gradient = new Gradient();
        colorKey = new GradientColorKey[3];
        colorKey[0].color = nightColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = dayColor;
        colorKey[1].time = 0.5f;
        colorKey[2].color = nightColor;
        colorKey[2].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

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

        light2D.color = gradient.Evaluate(timeRatio);

        if (localTimeElapsed >= gameDayLength)
        {
            dayCount += 1;
            localTimeElapsed = 0;
        }
    }
}
