using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioActuator : Actuator, IActuator
{

    public void doAction()
    {
        Debug.Log("Holaaa0");
        //StopAllCoroutines();
        //StartCoroutine(actionCoroutine());

    }

    IEnumerator actionCoroutine()
    {

        GetComponent<AudioSource>().Play();
        Debug.Log("Desencadetad");

            yield return new WaitForSeconds(0);
        
    }

}


#if UNITY_EDITOR
[CustomEditor(typeof(AudioActuator))]
public class AudioActuatorEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as AudioActuator;

        myScript.actionName = EditorGUILayout.TextField("Action Name", myScript.actionName);










    }
}

#endif