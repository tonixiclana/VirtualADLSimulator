/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Falicities to select gameobject in real time
 */

using UnityEngine;
using UnityEngine.UI;


/// <summary>Simple FPS-Counter.</summary>
public class FPSDisplay : MonoBehaviour
{

    #region Variables

    /// <summary>
    /// The text component to show the fps 
    /// </summary>
    [Tooltip("The text component to show the fps")]
    public Text FPS;

    /// <summary>
    /// Delta time
    /// </summary>
    private float deltaTime = 0f;

    /// <summary>
    /// Elapsed time
    /// </summary>
    private float elapsedTime = 0f;

    /// <summary>
    /// Microseconds of delay
    /// </summary>
    private float msec;

    /// <summary>
    /// Frames per second
    /// </summary>
    private float fps;

    /// <summary>
    /// Calculating string
    /// </summary>
    private const string wait = "<i>...calculating <b>FPS</b>...</i>";

    /// <summary>
    /// Red color string
    /// </summary>
    private const string red = "<color=#E57373><b>FPS: {0:0.}</b> ({1:0.0} ms)</color>";

    /// <summary>
    /// Orange color string
    /// </summary>
    private const string orange = "<color=#FFB74D><b>FPS: {0:0.}</b> ({1:0.0} ms)</color>";

    /// <summary>
    /// Green color string
    /// </summary>
    private const string green = "<color=#81C784><b>FPS: {0:0.}</b> ({1:0.0} ms)</color>";

    #endregion


    #region MonoBehaviour methods

    public void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 1f)
        {
            if (Time.frameCount % 3 == 0 && FPS != null)
            {
                msec = deltaTime * 1000f;
                fps = 1f / deltaTime;

                if (fps < 15)
                {
                    FPS.text = string.Format(red, fps, msec);
                }
                else if (fps < 29)
                {
                    FPS.text = string.Format(orange, fps, msec);
                }
                else
                {
                    FPS.text = string.Format(green, fps, msec);
                }
            }
        }
        else
        {
            FPS.text = wait;
        }
    }

    #endregion
}
