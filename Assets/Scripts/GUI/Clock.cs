using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private float timeScale = 1;
    private float elapsedTime = 0;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color CautionColor;
    [SerializeField]
    private Color DangerColor;

    [SerializeField]
    private int cautionhour;
    [SerializeField] 
    private int dangerHour;


    [SerializeField]
    private int startHours;
    [SerializeField]
    private int startMinutes;

    [SerializeField]
    private int stopHour;

    public string hours;
    public string minutes;

    private int hoursInt;
    private int minutesInt;


    private TMP_Text clockBox;

    private void Awake()
    {
        clockBox = GetComponent<TMP_Text>();

    }

    private void Start()
    {
        clockBox.color = defaultColor;
    }

    void Update()
    {
        if (hoursInt < stopHour)
        {
            elapsedTime += Time.deltaTime * timeScale;

            hoursInt = (int)(elapsedTime / 3600) + startHours;
            minutesInt = (int)((elapsedTime % 3600) / 60) + startMinutes;

            hours = hoursInt.ToString("D2");
            minutes = minutesInt.ToString("D2");

            if (hoursInt == cautionhour)
            {
                clockBox.color = CautionColor;
            }

            if (hoursInt == dangerHour)
            {
                clockBox.color = DangerColor;
            }

            clockBox.text = hours + ":" + minutes;
        }
    }
}
