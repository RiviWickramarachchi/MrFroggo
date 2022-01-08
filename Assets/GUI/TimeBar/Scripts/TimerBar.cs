using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    //Controls the time bar 
    [SerializeField]private Gradient gradient;
    [SerializeField] private Slider timeBar;
    [SerializeField] private Image fillImg;
    

    public float getTimeVal()
    {
        return timeBar.value;
    }
    public void setTime(float time)
    {
        timeBar.value = time;
        fillImg.color = gradient.Evaluate(timeBar.normalizedValue);
    }

    public void setMaxTime(float time)
    {
        timeBar.maxValue = time;
    }

}
