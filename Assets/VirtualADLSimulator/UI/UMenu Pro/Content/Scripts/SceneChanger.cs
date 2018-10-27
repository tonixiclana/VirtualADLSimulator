using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">window to active</param>
    /// <param name="disable">disabled currents window?</param>
    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);
    }

}
