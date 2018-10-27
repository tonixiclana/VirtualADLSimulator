using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UMP_LevelInfo : MonoBehaviour {

    public Text Title;
    public Text Description;
    public Image Preview;
    [Space(7)]
    public string EventAnimation = "LevelEnter";
    //Name of scene of build setting
    private string LevelName;

    /// <summary>
    /// Level Info
    /// </summary>
    /// <param name="title"></param>
    /// <param name="desc"></param>
    /// <param name="preview"></param>
    /// <param name="scene"></param>
    public void GetInfo(string title,string desc,Sprite preview,string scene)
    {
        Title.text = title;
        Description.text = desc;
        Preview.sprite = preview;

        LevelName = scene;
    }
    /// <summary>
    /// 
    /// </summary>
    public void OpenLevel() { Application.LoadLevel(LevelName); }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Forward"></param>
    public void EventMouse(bool Forward = true)
    {
        Animation a = this.GetComponent<Animation>();
        if (Forward)
        {
            a[EventAnimation].time = 0.0f;
            a[EventAnimation].speed = 1.0f;
            a.CrossFade(EventAnimation);
        }
        else
        {
            if (a[EventAnimation].time == 0.0f)
            {
                a[EventAnimation].time = a[EventAnimation].length;
            }
            a[EventAnimation].speed = -1.0f;
            a.CrossFade(EventAnimation);
        }
    }
}