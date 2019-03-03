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
using System.ComponentModel;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[RequireComponent(typeof(SelectGameobjectCursor))]
[System.Serializable]
public class SensorEditorUI : MonoBehaviour {

    [Description("The available Sensors to put in a object")]
    public List<Sensor> availableSensors;
    public SelectGameobjectCursor selectedGameObjectCursor;
    public GameObject editSensorLayout;
    public GridLayoutGroup sensorPropertiesContent;
    public Button addSensorButton;
    public Button deleteSensorButton;
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

        GameObject addSensorTitleGm = new GameObject("AddSensorTitle");

        addSensorTitleGm.AddComponent<TextMeshProUGUI>().text = "Sensors availables for this Element";
        addSensorTitleGm.GetComponent<TextMeshProUGUI>().fontSize = 60;
        addSensorTitleGm.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;

        addSensorTitleGm = Instantiate(addSensorTitleGm, sensorPropertiesContent.transform);
        sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(addSensorTitleGm);
        foreach (var sensor in availableSensors)
        {
            if (sensor.tagsAvailable.Contains(inEditGameobject.tag))
            {
                GameObject editor = Instantiate(addSensorButton.gameObject, sensorPropertiesContent.transform);

               

                editor.GetComponent<Button>().onClick.AddListener(
                    delegate {
                        Sensor s = addSensor(inEditGameobject, sensor);
           
                        editSensor(inEditGameobject.GetComponentInChildren<Sensor>());
                    });

                editor.GetComponentInChildren<Text>().text = sensor.GetType().Name;
                editor.GetComponent<SensorDescriptionPopUp>().setDescription(getDescriptionOfClass(sensor.GetType()));
                sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
            }   
        }
    }


    private void editSensor(Sensor currentInEditSensor)
    {
        //To show the editor layout
        if (!editSensorLayout.activeSelf)
            editSensorLayout.SetActive(true);

        sensorPropertiesContent.GetComponent<PopulateGrid>().removeAllElements();

        GameObject addSensorTitleGm = new GameObject("EditSensorTitle");

        addSensorTitleGm.AddComponent<TextMeshProUGUI>().text = "Edit Sensor Properties";
        addSensorTitleGm.GetComponent<TextMeshProUGUI>().fontSize = 60;
        addSensorTitleGm.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;

        addSensorTitleGm = Instantiate(addSensorTitleGm, sensorPropertiesContent.transform);
        sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(addSensorTitleGm);

        
        
        foreach (var prop in currentInEditSensor.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            //Debug.Log(prop.Name);

            GameObject editor;
            if (prop.FieldType.ToString() == "System.String")
            {
                editor = Instantiate(stringPropertyEditor, sensorPropertiesContent.transform);

                editor.GetComponentInChildren<TextMeshProUGUI>().text = prop.Name;
                try
                {
                    editor.GetComponent<TMP_InputField>().text = prop.GetValue(currentInEditSensor).ToString();
                }
                catch (Exception e)
                {
                    Debug.Log(prop.Name);
                }
                

                editor.GetComponent<SensorDescriptionPopUp>().setDescription(getDescriptionOfAttribute(currentInEditSensor.GetType(), prop.Name));

                
                editor.GetComponent<TMP_InputField>().onSelect.AddListener(delegate {
                    foreach(var shortCut in FindObjectsOfType<ShortCutKeysEditMode>()){
                        shortCut.enabled = false;
                    }
                    FindObjectOfType<FreeCamera>().enabled = false;
                });
                

                editor.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate {
                    changeFieldValue(currentInEditSensor, prop, editor.GetComponent<TMP_InputField>().text);
                });

                editor.GetComponent<TMP_InputField>().onDeselect.AddListener(delegate {
                    foreach (var shortCut in FindObjectsOfType<ShortCutKeysEditMode>())
                    {
                        shortCut.enabled = true;
                    }
                    FindObjectOfType<FreeCamera>().enabled = true;
                });

                sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
            }
            else
            if (prop.FieldType.ToString() == "System.Single")
            {
                editor = Instantiate(stringPropertyEditor, sensorPropertiesContent.transform);

                editor.GetComponentInChildren<TextMeshProUGUI>().text = prop.Name;
                editor.GetComponent<TMP_InputField>().text = prop.GetValue(currentInEditSensor).ToString();
                editor.GetComponent<SensorDescriptionPopUp>().setDescription(getDescriptionOfAttribute(currentInEditSensor.GetType(), prop.Name));

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
                editor.GetComponent<SensorDescriptionPopUp>().setDescription(getDescriptionOfAttribute(currentInEditSensor.GetType(), prop.Name));

                editor.GetComponent<Toggle>().isOn = (bool)prop.GetValue(currentInEditSensor);

                editor.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                    changeFieldValue(currentInEditSensor, prop, editor.GetComponent<Toggle>().isOn);
                });

                sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(editor);
            }
        }
        
        GameObject removeButton = Instantiate(deleteSensorButton.gameObject, sensorPropertiesContent.transform);

        removeButton.GetComponent<Button>().onClick.AddListener(
            delegate {
                removeInEditSensor();
                listSensorTypesInGUI();
            });

        removeButton.GetComponentInChildren<Text>().text = "Remove Sensor";

        sensorPropertiesContent.GetComponent<PopulateGrid>().addElement(removeButton);
        
    }

    
   public string getDescriptionOfClass(Type type)
   {
       var descriptions = (DescriptionAttribute[])type.GetCustomAttributes(typeof(DescriptionAttribute), false);

       if (descriptions == null)
           return "";

       return descriptions[0].Description;
   }
   
    public string getDescriptionOfAttribute(Type type, string attrName)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);

        
        try
        {
           // Debug.Log(type + " atributo name: " + attrName);
            return properties.Find(attrName, true).Description;
        }
        catch (Exception e)
        {
            //Debug.Log("no se encuentra");
        }
        
        return "";
    }


}

