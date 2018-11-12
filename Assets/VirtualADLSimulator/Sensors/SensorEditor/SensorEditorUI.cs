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

    public List<Sensor> availableSensors;
    public SelectGameobjectCursor selectedGameObjectCursor;
    public GameObject editSensorLayout;
    public GridLayoutGroup sensorPropertiesContent;
    public Button addSensorButton;
    public GameObject stringPropertyEditor;
    public GameObject booleanPropertyEditor;
    public GameObject inEditGameobject;

    private int lastGameObjectsSelectedCount = 0;

    // Use this for initialization
    void Start () {
        if(selectedGameObjectCursor == null)
            selectedGameObjectCursor = GetComponent<SelectGameobjectCursor>();
	}

    public void changeFieldValue(Sensor sensor, FieldInfo prop, object value)
    {
        prop.SetValue(sensor, value);
    }

    void FixedUpdate () {

        editSelectedGameobject();
        
        
                
	}

    private Sensor addSensor(GameObject gm,  Sensor sensor)
    {
        Sensor ret = Instantiate(sensor.gameObject, gm.transform).GetComponent<Sensor>();
        return ret;
    }

    private void removeSensor(Sensor sensor)
    {
        Destroy(sensor.gameObject);
    }

    public void removeInEditSensor()
    {
        Destroy(inEditGameobject.GetComponentInChildren<Sensor>().gameObject);
    }



    public void editSelectedGameobject()
    {
        if(selectedGameObjectCursor.gameObjectsSelected.Count == 0)
        {
            editSensorLayout.SetActive(false);
            inEditGameobject = null;
        }
        else
        if (selectedGameObjectCursor.gameObjectsSelected[0] != inEditGameobject)
        {
            lastGameObjectsSelectedCount = selectedGameObjectCursor.gameObjectsSelected.Count;
            inEditGameobject = selectedGameObjectCursor.gameObjectsSelected[0];

            //Sensor currentInEditSensor = inEditGameobject.GetComponent<Sensor>();
            Sensor currentInEditSensor = (inEditGameobject.GetComponent<Sensor>() == null)
                ? inEditGameobject.GetComponentInChildren<Sensor>()
                : inEditGameobject.GetComponent<Sensor>();

            if (currentInEditSensor != null)
            {
                editSensor(currentInEditSensor);
            }
            else
            {

                listSensorTypesInGUI();
            }
        }

    }

    private void listSensorTypesInGUI()
    {
        sensorPropertiesContent.GetComponent<PopulateGrid>().removeAllElements();

        if (!editSensorLayout.activeSelf)
            editSensorLayout.SetActive(true);
        foreach (var sensor in availableSensors)
        {
            GameObject editor = Instantiate(addSensorButton.gameObject, sensorPropertiesContent.transform);

            editor.GetComponent<Button>().onClick.AddListener(
                delegate {
                    addSensor(inEditGameobject, sensor);
                    editSensor(inEditGameobject.GetComponentInChildren<Sensor>());
                });

            editor.GetComponentInChildren<Text>().text = sensor.GetType().Name;

            sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
        }
    }


    private void editSensor(Sensor currentInEditSensor)
    {
        //To show the editor layout
        if (!editSensorLayout.activeSelf)
            editSensorLayout.SetActive(true);

        sensorPropertiesContent.GetComponent<PopulateGrid>().removeAllElements();
        
        foreach (var prop in currentInEditSensor.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            GameObject editor;
            if (prop.FieldType.ToString() == "System.String")
            {
                editor = Instantiate(stringPropertyEditor, sensorPropertiesContent.transform);

                editor.GetComponentInChildren<TextMeshProUGUI>().text = prop.Name;
                editor.GetComponent<TMP_InputField>().text = prop.GetValue(currentInEditSensor).ToString();

                editor.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate {
                    changeFieldValue(currentInEditSensor, prop, editor.GetComponent<TMP_InputField>().text);
                });

                sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
            }
            else
            if (prop.FieldType.ToString() == "System.Single")
            {
                editor = Instantiate(stringPropertyEditor, sensorPropertiesContent.transform);

                editor.GetComponentInChildren<TextMeshProUGUI>().text = prop.Name;
                editor.GetComponent<TMP_InputField>().text = prop.GetValue(currentInEditSensor).ToString();

                editor.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate {
                    changeFieldValue(currentInEditSensor, prop, float.Parse(editor.GetComponent<TMP_InputField>().text));
                });

                sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
            }
            else
            if (prop.FieldType.ToString() == "System.Boolean")
            {
                editor = Instantiate(booleanPropertyEditor, sensorPropertiesContent.transform);

                editor.GetComponentInChildren<TextMeshProUGUI>().text = prop.Name;
                editor.GetComponent<Toggle>().isOn = (bool)prop.GetValue(currentInEditSensor);

                editor.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                    changeFieldValue(currentInEditSensor, prop, editor.GetComponent<Toggle>().isOn);
                });

                sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
            }
        }
        
        GameObject removeButton = Instantiate(addSensorButton.gameObject, sensorPropertiesContent.transform);

        removeButton.GetComponent<Button>().onClick.AddListener(
            delegate {
                removeInEditSensor();
                listSensorTypesInGUI();
            });

        removeButton.GetComponentInChildren<Text>().text = "Remove Sensor";

        sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(removeButton);
        
    }

}

