using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DatePicker : MonoBehaviour {

    public DayNight dayNightSystem;

    public TextMeshProUGUI dateVisor;

    public TMP_Dropdown year;
    public TMP_Dropdown month;
    public TMP_Dropdown day;
    public Button today;
    public Slider time;

    

    // Use this for initialization
    void Start() {

   /*
*/
        for (int i = 1970; i <= 2050; i++)
        {
            year.options.Add(new TMP_Dropdown.OptionData() { text = i.ToString() });
        }

        for (int i = 1; i <= 12; i++)
        {

            month.options.Add(new TMP_Dropdown.OptionData() { text = new KeyValuePair<int, string>(i, DateTimeFormatInfo.CurrentInfo.GetMonthName(i)).Key + " " + new KeyValuePair<int, string>(i, DateTimeFormatInfo.CurrentInfo.GetMonthName(i)).Value });
        }


        year.onValueChanged.AddListener(delegate {

            dayNightSystem.setActualDateTime(new DateTime(Int32.Parse(year.options[year.value].text), dayNightSystem.currentDateTime.Month
                ,dayNightSystem.currentDateTime.Day, dayNightSystem.currentDateTime.Hour, 
                dayNightSystem.currentDateTime.Minute, dayNightSystem.currentDateTime.Second));

        });

        month.onValueChanged.AddListener(delegate {

            day.options.Clear();
            day.options.Add(new TMP_Dropdown.OptionData() { text = "Day" });

            
            for(int u = 1; u <= (DateTime.DaysInMonth(Int32.Parse(year.options[year.value].text), month.value)); u++)
                day.options.Add(new TMP_Dropdown.OptionData() { text = u.ToString() });

            if (day.options.Count - 1 < dayNightSystem.currentDateTime.Day)
                dayNightSystem.setActualDateTime(new DateTime(Int32.Parse(year.options[year.value].text), month.value, day.options.Count - 1,
                    dayNightSystem.currentDateTime.Hour, dayNightSystem.currentDateTime.Minute,
                    dayNightSystem.currentDateTime.Second));
            else
                dayNightSystem.setActualDateTime(new DateTime(Int32.Parse(year.options[year.value].text), month.value, dayNightSystem.currentDateTime.Day,
                    dayNightSystem.currentDateTime.Hour, dayNightSystem.currentDateTime.Minute,
                    dayNightSystem.currentDateTime.Second));

        });

        day.onValueChanged.AddListener(delegate {
            if (day.value != 0)
            {
                dayNightSystem.setActualDateTime(new DateTime(Int32.Parse(year.options[year.value].text), month.value, day.value,
                    dayNightSystem.currentDateTime.Hour, dayNightSystem.currentDateTime.Minute,
                    dayNightSystem.currentDateTime.Second));

            }
        });

        today.onClick.AddListener(delegate {
            dayNightSystem.synchronizeDayFromUTC();
        });

    
        //(int)(((time.value % 3600) % 60) / 60)
        time.onValueChanged.AddListener(delegate {

            if (time.value < time.maxValue && time.value > time.minValue)
            {
                time.interactable = true;
                dayNightSystem.setActualDateTime(new DateTime(dayNightSystem.currentDateTime.Year, dayNightSystem.currentDateTime.Month,
                    dayNightSystem.currentDateTime.Day, (int)((time.value) / 3600), (int)(((time.value) % 3600) / 60),
                     dayNightSystem.currentDateTime.Second));
            }
            else
            {
                if(time.value >= time.maxValue)
                    dayNightSystem.setActualDateTime(new DateTime(dayNightSystem.currentDateTime.Year, dayNightSystem.currentDateTime.Month,
                        dayNightSystem.currentDateTime.Day, 0, 0, 0).AddDays(1));
                else if (time.value <= time.minValue)
                    dayNightSystem.setActualDateTime(new DateTime(dayNightSystem.currentDateTime.Year, dayNightSystem.currentDateTime.Month,
                        dayNightSystem.currentDateTime.Day, 23, 59, 0).AddDays(-1));
            }
        });

        

    }

    

    // Update is called once per frame
    void FixedUpdate () {

        for (int i = 0; i < year.options.Count(); i++)
            if (year.options[i].text == dayNightSystem.currentDateTime.Year.ToString())
                year.value = i;

        month.value = dayNightSystem.currentDateTime.Month;


        day.value = dayNightSystem.currentDateTime.Day;


        time.value = 1 + (60 * dayNightSystem.currentDateTime.Minute) + (60 * 60 * dayNightSystem.currentDateTime.Hour);
   

        dateVisor.text = dayNightSystem.currentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fffff");
    }
}
