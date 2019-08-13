/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Falicities to select gameobject in real time with the cursor
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Light))]
[System.Serializable]
public class DayNight : MonoBehaviour {

    private Light sunLight;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public float durationOfCompleteDay = 24f;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public DateTime currentDateTime;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public double totalSecondsTranscurred;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public int currentYear;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public int currentMonth;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public int currentDay;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public int currentHour;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public int currentMinute;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public int currentSecond;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public int currentMillisecond;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in hours")]
    public bool synchronizeFromUTC;


    private Quaternion currentRotation;
    private Quaternion targetRotation;


    private double secondsPerDegreesDay;
    private float secondsPerDegreesNight;
    private double degreesPerSecondDay;
    private float degreesPerSecondNight;

    private Dictionary<Light, float> originalScenaryLightsIntensity = new Dictionary<Light, float>();
    private List<Light> actualScenaryLights;


    private void FixedUpdate()
    {
        if (synchronizeFromUTC)
        {
            synchronizeDayFromUTC();
        }

        currentYear = currentDateTime.Year;
        currentMonth = currentDateTime.Month;
        currentDay = currentDateTime.Day;
        currentHour = currentDateTime.Hour;
        currentMinute = currentDateTime.Minute;
        currentSecond = currentDateTime.Second;
        currentMillisecond = currentDateTime.Millisecond;

    }


    private void Awake()
    {
                foreach (Light l in FindObjectsOfType<Light>())
            if (l.type == LightType.Spot || l.type == LightType.Point)
                originalScenaryLightsIntensity.Add(l, l.intensity);

        actualScenaryLights = new List<Light>(FindObjectsOfType<Light>().Where(i => i.type == LightType.Point || i.type == LightType.Spot).Count());

    }

    // Use this for initialization
    void Start () {


        sunLight = GetComponent<Light>();

        //setActualSecondOfDay(convertToUnixTime(new DateTime(1970, 1, 1, 6, 0, 0).ToUniversalTime()));


        secondsPerDegreesDay = ((double)durationOfCompleteDay / (double)2) * (double)60 * (double)60 / (double)180;
        degreesPerSecondDay = (double)180 / (((double)durationOfCompleteDay / (double)2) * (double)60 * (double)60);


        if (synchronizeFromUTC)
        {
            synchronizeDayFromUTC();
        }
        else
            setActualSecondOfDay(totalSecondsTranscurred);

    }

    private void Update()
    {
        foreach (Light l in FindObjectsOfType<Light>())
            if (l.type == LightType.Spot || l.type == LightType.Point)
                if (!originalScenaryLightsIntensity.ContainsKey(l))
                    originalScenaryLightsIntensity.Add(l, l.intensity);

    }

    public void synchronizeDayFromUTC()
    {
        secondsPerDegreesDay = ((double)durationOfCompleteDay / (double)2) * (double)60 * (double)60 / (double)180;
        degreesPerSecondDay = (double)180 / (((double)durationOfCompleteDay / (double)2) * (double)60 * (double)60);
        setActualSecondOfDay(convertToUnixTime(DateTime.Now));
        synchronizeFromUTC = false;
    }

    public void setActualSecondOfDay(double seconds)
    {
        //StopAllCoroutines();
        //transform.rotation = Quaternion.AngleAxis(-90, Vector3.right);
        float rotationToDo = (float)(seconds * degreesPerSecondDay) % 360;
        transform.rotation = Quaternion.AngleAxis(-90 + rotationToDo, Vector3.right);
        currentRotation = transform.rotation;
        totalSecondsTranscurred = seconds;
        StopAllCoroutines();
        StartCoroutine(dayNightSimulator());
        //StartCoroutine(dayNightSimulator());
    }

    public void setActualDateTime(DateTime dt)
    {

        setActualSecondOfDay(convertToUnixTime(dt));
    }

    IEnumerator dayNightSimulator()
    {

        double rotationStepDay;



        while (true)
        {
            Color color;
            Color color1;

            //Debug.Log("Diferencia en grados sunrise 90: " + Quaternion.Angle(currentRotation, Quaternion.AngleAxis(90, Vector3.right)));
            //Debug.Log("Diferencia en grados sunrise 0: " + Quaternion.Angle(currentRotation, Quaternion.AngleAxis(0, Vector3.right)));
            if (Quaternion.Angle(currentRotation, Quaternion.AngleAxis(90, Vector3.right)) <= 90.2
                && Quaternion.Angle(currentRotation, Quaternion.AngleAxis(0, Vector3.right)) <= 90.2)
            {
                targetRotation = Quaternion.AngleAxis(90, Vector3.right);
                //GetComponent<Light>().intensity = 0.5f;
                //RenderSettings.ambientIntensity = 0.25f;
                ColorUtility.TryParseHtmlString("#F2ECE5", out color);
                ColorUtility.TryParseHtmlString("#81e1e3", out color1);
                float lerp = 0;

                Debug.Log("Sunrise init");


                while (Quaternion.Angle(currentRotation, Quaternion.AngleAxis(90, Vector3.right)) > 0)
                {
                    rotationStepDay = degreesPerSecondDay * Time.deltaTime;


                    lerp = Mathf.InverseLerp(90.2f, 0, Quaternion.Angle(currentRotation, Quaternion.AngleAxis(90, Vector3.right)));

                    GetComponent<Light>().color = Color.Lerp(color, color1, lerp);
                    GetComponent<Light>().intensity = Mathf.Lerp(0.3f, 0.6f, lerp);
                    GetComponent<Light>().shadowStrength = Mathf.Lerp(0.8f, 0.5f, lerp);
                    RenderSettings.ambientIntensity = Mathf.Lerp(0.3f, 0.6f, lerp);
                    RenderSettings.reflectionIntensity = Mathf.Lerp(0.4f, 0.7f, lerp);

                    /*
                    actualScenaryLights = FindObjectsOfType<Light>().Where(i => i.type == LightType.Point || i.type == LightType.Spot).ToList();

                    for (int i = 0; i < actualScenaryLights.Count(); i++)
                    {
                        float intensity;
                        originalScenaryLightsIntensity.TryGetValue(actualScenaryLights[i], out intensity);
                        actualScenaryLights[i].intensity = Mathf.Lerp(intensity * 0.6f, intensity * 0.2f, Mathf.InverseLerp(0.3f, 0.6f, RenderSettings.ambientIntensity));

                    }
                    */

                    transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, (float)rotationStepDay);
                    currentRotation = transform.localRotation;
                    totalSecondsTranscurred += rotationStepDay * secondsPerDegreesDay;
                    currentDateTime = UnixTimeStampToDateTime(totalSecondsTranscurred);
                    yield return new WaitForSeconds(0);
                }
                
                Debug.Log("Sunrise end");
            }

            //Debug.Log("Diferencia en grados mid day 180: " + Quaternion.Angle(currentRotation, Quaternion.AngleAxis(180, Vector3.right)));
            //Debug.Log("Diferencia en grados mid day 90: " + Quaternion.Angle(currentRotation, Quaternion.AngleAxis(90, Vector3.right)));
            if (Quaternion.Angle(currentRotation, Quaternion.AngleAxis(180, Vector3.right)) <= 90.2
                && Quaternion.Angle(currentRotation, Quaternion.AngleAxis(90, Vector3.right)) <= 90.2)
            {
                targetRotation = Quaternion.AngleAxis(180, Vector3.right);

                ColorUtility.TryParseHtmlString("#81e1e3", out color);
                ColorUtility.TryParseHtmlString("#FAD6A5", out color1);
                float lerp = 0;
                Debug.Log("midDay init");

                while (Quaternion.Angle(currentRotation, Quaternion.AngleAxis(180, Vector3.right)) > 0)
                {
                    rotationStepDay = degreesPerSecondDay * Time.deltaTime;


                    lerp = Mathf.InverseLerp(90.2f, 0, Quaternion.Angle(currentRotation, Quaternion.AngleAxis(180, Vector3.right)));

                    GetComponent<Light>().color = Color.Lerp(color, color1, lerp);
                    GetComponent<Light>().intensity = Mathf.Lerp(0.6f, 0.3f, lerp);
                    GetComponent<Light>().shadowStrength = Mathf.Lerp(0.5f, 0.8f, lerp);
                    RenderSettings.ambientIntensity = Mathf.Lerp(0.6f, 0.4f, lerp);
                    RenderSettings.reflectionIntensity = Mathf.Lerp(0.7f, 0.4f, lerp);

                    /*
                    actualScenaryLights = FindObjectsOfType<Light>().Where(i => i.type == LightType.Point || i.type == LightType.Spot).ToList();

                    for (int i = 0; i < actualScenaryLights.Count(); i++)
                    {
                        float intensity;
                        originalScenaryLightsIntensity.TryGetValue(actualScenaryLights[i], out intensity);
                        actualScenaryLights[i].intensity = Mathf.Lerp(intensity * 0.2f, intensity * 0.6f, Mathf.InverseLerp(0.6f, 0.4f, RenderSettings.ambientIntensity));

                    }
                    */
                   /* for (int i = 0; i < actualScenaryLights.Count(); i++)
                        actualScenaryLights[i].intensity = Mathf.Lerp(originalScenaryLightsIntensity[i] * 0.2f, originalScenaryLightsIntensity[i] * 0.6f, Mathf.InverseLerp(0.6f, 0.4f, RenderSettings.ambientIntensity));
*/
                    transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, (float)rotationStepDay);
                    currentRotation = transform.localRotation;
                    totalSecondsTranscurred += rotationStepDay * secondsPerDegreesDay;
                    currentDateTime = UnixTimeStampToDateTime(totalSecondsTranscurred);
                    yield return new WaitForSeconds(0);
                }

                Debug.Log("mid day end");
            }

            //sunset
            //Debug.Log("Diferencia en grados sunset 270: " + Quaternion.Angle(currentRotation, Quaternion.AngleAxis(270, Vector3.right)));
            //Debug.Log("Diferencia en grados sunset 180: " + Quaternion.Angle(currentRotation, Quaternion.AngleAxis(180, Vector3.right)));
            if (Quaternion.Angle(currentRotation, Quaternion.AngleAxis(270, Vector3.right)) <= 90.2
                && Quaternion.Angle(currentRotation, Quaternion.AngleAxis(180, Vector3.right)) <= 90.2)
            {
                targetRotation = Quaternion.AngleAxis(270, Vector3.right);

                
                ColorUtility.TryParseHtmlString("#FAD6A5", out color);
                ColorUtility.TryParseHtmlString("#282550", out color1);
                float lerp = 0;

                Debug.Log("sunset init");

                while (Quaternion.Angle(currentRotation, targetRotation) > 0)
                {
                    rotationStepDay = degreesPerSecondDay * Time.deltaTime;


                    lerp = Mathf.InverseLerp(90.2f, 0, Quaternion.Angle(currentRotation, Quaternion.AngleAxis(270, Vector3.right)));

                    GetComponent<Light>().color = Color.Lerp(color, color1, lerp);
                    GetComponent<Light>().intensity = Mathf.Lerp(0.3f, 0f, lerp);
                    GetComponent<Light>().shadowStrength = Mathf.Lerp(0.8f, 1f, lerp);
                    RenderSettings.ambientIntensity = Mathf.Lerp(0.4f, 0.05f, lerp);
                    RenderSettings.reflectionIntensity = Mathf.Lerp(0.4f, 0.2f, lerp);

                    /*
                    actualScenaryLights = FindObjectsOfType<Light>().Where(i => i.type == LightType.Point || i.type == LightType.Spot).ToList();

                    for (int i = 0; i < actualScenaryLights.Count(); i++)
                    {
                        float intensity;
                        originalScenaryLightsIntensity.TryGetValue(actualScenaryLights[i], out intensity);
                        actualScenaryLights[i].intensity = Mathf.Lerp(intensity * 0.6f, intensity, Mathf.InverseLerp(0.4f, 0.05f, RenderSettings.ambientIntensity));

                    }
                    */
                    /*for (int i = 0; i < actualScenaryLights.Count(); i++)
                        actualScenaryLights[i].intensity = Mathf.Lerp(originalScenaryLightsIntensity[i] * 0.6f, originalScenaryLightsIntensity[i], Mathf.InverseLerp(0.4f, 0.05f, RenderSettings.ambientIntensity));
*/

                    transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, (float)rotationStepDay);
                    currentRotation = transform.localRotation;
                    totalSecondsTranscurred += rotationStepDay * secondsPerDegreesDay;
                    currentDateTime = UnixTimeStampToDateTime(totalSecondsTranscurred);
                    yield return new WaitForSeconds(0);
                }

                Debug.Log("sunset end");
            }
            

            //Debug.Log("Diferencia en grados midnight 360: " + Quaternion.Angle(currentRotation, Quaternion.AngleAxis(-90, Vector3.right)));
            //Debug.Log("Diferencia en grados midnight 270: " + Quaternion.Angle(currentRotation, Quaternion.AngleAxis(0, Vector3.right)));
            if (Quaternion.Angle(currentRotation, Quaternion.AngleAxis(-90, Vector3.right)) <= 90.2
                && Quaternion.Angle(currentRotation, Quaternion.AngleAxis(0, Vector3.right)) <= 90.2)
            {
                targetRotation = Quaternion.AngleAxis(0, Vector3.right);

                ColorUtility.TryParseHtmlString("#282550", out color);
                ColorUtility.TryParseHtmlString("#F2ECE5", out color1);

                Debug.Log("midNight init");

                float lerp = 0;



          

                while (Quaternion.Angle(currentRotation, Quaternion.AngleAxis(0, Vector3.right)) > 0)
                {
                    rotationStepDay = degreesPerSecondDay * Time.deltaTime;



                    lerp = Mathf.InverseLerp(90.2f, 0, Quaternion.Angle(currentRotation, Quaternion.AngleAxis(0, Vector3.right)));
                    GetComponent<Light>().color = Color.Lerp(color, color1, lerp);
                    GetComponent<Light>().intensity = Mathf.Lerp(0f, 0.3f, lerp);
                    GetComponent<Light>().shadowStrength = Mathf.Lerp(1f, 0.8f, lerp);
                    RenderSettings.ambientIntensity = Mathf.Lerp(0.05f, 0.3f, lerp) ;
                    RenderSettings.reflectionIntensity = Mathf.Lerp(0.2f, 0.4f, lerp);

                    /*
                    actualScenaryLights = FindObjectsOfType<Light>().Where(i => i.type == LightType.Point || i.type == LightType.Spot).ToList();


                    for (int i = 0; i < actualScenaryLights.Count(); i++)
                    {
                        float intensity;
                        originalScenaryLightsIntensity.TryGetValue(actualScenaryLights[i], out intensity);
                        actualScenaryLights[i].intensity = Mathf.Lerp(intensity, intensity * 0.6f, Mathf.InverseLerp(0.05f, 0.3f, RenderSettings.ambientIntensity));

                    }
                    */
                    /*
                    for (int i = 0; i < actualScenaryLights.Count(); i++)
                        actualScenaryLights[i].intensity = Mathf.Lerp(originalScenaryLightsIntensity[i], originalScenaryLightsIntensity[i] * 0.6f, Mathf.InverseLerp(0.05f, 0.3f, RenderSettings.ambientIntensity));
*/

                    transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, (float)rotationStepDay);
                    currentRotation = transform.localRotation;
                    totalSecondsTranscurred += rotationStepDay * secondsPerDegreesDay;
                    currentDateTime = UnixTimeStampToDateTime(totalSecondsTranscurred);
                    yield return new WaitForSeconds(0);
                }

                Debug.Log("miNight end");
            }

            yield return new WaitForSeconds(0.1f);
            
        }

    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {

        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
        
        return dtDateTime;
    }

    public double convertToUnixTime(DateTime datetime)
    {
       
        DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0);
        
        return (double)(datetime - sTime).TotalSeconds;
    }

}

