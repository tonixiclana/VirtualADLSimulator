/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Falicities to select gameobject in real time with the cursor
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
[System.Serializable]
public class DayNight : MonoBehaviour {

   private Light sunLight;

    /// <summary>
    /// The duration of day in minutes
    /// </summary>
    [Tooltip("The duration of day in minutes")]
    public float durationOfDay;

    /// <summary>
    /// The duration of night in minutes
    /// </summary>
    [Tooltip("The duration of night in minutes")]
    public float durationOfNight;

    private float secondsPerDegreesDay;
    private float secondsPerDegreesNight;
    private float degreesPerSecondDay;
    private float degreesPerSecondNight;


    // Use this for initialization
    void Start () {

        sunLight = GetComponent<Light>();
        sunLight.transform.eulerAngles = new Vector3(0, -90, 0);
        secondsPerDegreesDay = durationOfDay * 60/ 180;
        secondsPerDegreesNight = durationOfNight * 60 / 180;

        degreesPerSecondDay = 180 / (durationOfDay * 60);
        degreesPerSecondNight = 180 / (durationOfNight * 60);

        StartCoroutine(dayNightSimulator());
    }
	
    IEnumerator dayNightSimulator()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(-180, -90, 0));
        float rotationStepDay;

        while (true)
        {
            while (transform.rotation != targetRotation)
            {
                rotationStepDay = degreesPerSecondDay * Time.deltaTime;

                currentRotation = transform.rotation;

                transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationStepDay);

                yield return new WaitForSeconds(0);
            }

            targetRotation = Quaternion.Euler(new Vector3(-360, -90, 0));
            while (transform.rotation != targetRotation)
            {
                rotationStepDay = degreesPerSecondDay * Time.deltaTime;

                currentRotation = transform.rotation;

                transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationStepDay);

                yield return new WaitForSeconds(0);
            }
            yield return new WaitForSeconds(0);
        }
    }
}
