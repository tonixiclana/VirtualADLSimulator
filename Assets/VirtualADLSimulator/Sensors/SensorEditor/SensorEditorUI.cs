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
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[RequireComponent(typeof(SelectGameobjectCursor))]
[System.Serializable]
public class SensorEditorUI : MonoBehaviour {

    public SelectGameobjectCursor selectedGameObjectCursor;
    public GameObject editSensorLayout;
    public GridLayoutGroup sensorPropertiesContent;
    public GameObject stringPropertyEditor;
    public GameObject booleanPropertyEditor;
    public Sensor inEditSensor;



    // Use this for initialization
    void Start () {
        if(selectedGameObjectCursor == null)
            selectedGameObjectCursor = GetComponent<SelectGameobjectCursor>();
	}

    public void changeFieldValue(Sensor sensor, FieldInfo prop, object value)
    {
        prop.SetValue(sensor, value);
        Debug.Log("Changed!");
    }

    void FixedUpdate () {
        if (selectedGameObjectCursor.gameObjectsSelected.Count != 0)
        {
            if (selectedGameObjectCursor.gameObjectsSelected[0].GetComponent<Sensor>() != inEditSensor)
            {
                inEditSensor = selectedGameObjectCursor.gameObjectsSelected[0].GetComponent<Sensor>();
                Sensor currentInEditSensor = inEditSensor;
                if (currentInEditSensor != null)
                {
                    //To show the editor layout
                    if (!editSensorLayout.activeSelf)
                        editSensorLayout.SetActive(true);

                    sensorPropertiesContent.GetComponent<PopulateGrid>().removeAllElements();
                    foreach (var prop in currentInEditSensor.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {

                        if (prop.FieldType.ToString() == "System.String")
                        {
                            GameObject editor = Instantiate(stringPropertyEditor, sensorPropertiesContent.transform);

                            editor.GetComponentInChildren<TextMeshProUGUI>().text = prop.Name;
                            editor.GetComponent<TMP_InputField>().text = prop.GetValue(currentInEditSensor).ToString();
                            
                            editor.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate {
                                changeFieldValue(currentInEditSensor, prop, editor.GetComponent<TMP_InputField>().text);
                            });

                            sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
                        }
                        
                        if (prop.FieldType.ToString() == "System.Single")
                        {
                            GameObject editor = Instantiate(stringPropertyEditor, sensorPropertiesContent.transform);

                            editor.GetComponentInChildren<TextMeshProUGUI>().text = prop.Name;
                            editor.GetComponent<TMP_InputField>().text = prop.GetValue(currentInEditSensor).ToString();

                            editor.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate {
                                changeFieldValue(currentInEditSensor, prop, float.Parse(editor.GetComponent<TMP_InputField>().text));
                            });

                            sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
                        }

                        if (prop.FieldType.ToString() == "System.Boolean")
                        {
                            GameObject editor = Instantiate(booleanPropertyEditor, sensorPropertiesContent.transform);

                            editor.GetComponentInChildren<TextMeshProUGUI>().text = prop.Name;
                            editor.GetComponent<Toggle>().isOn = (bool)prop.GetValue(inEditSensor);

                            editor.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                                changeFieldValue(currentInEditSensor, prop, editor.GetComponent<Toggle>().isOn);
                            });

                            sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
                        }
                    }
                }
                else
                {
                    editSensorLayout.SetActive(false);
                    inEditSensor = null;
                }


            }
        }
        else
        {
            editSensorLayout.SetActive(false);
            inEditSensor = null;
        }
        
                
	}

    
}
