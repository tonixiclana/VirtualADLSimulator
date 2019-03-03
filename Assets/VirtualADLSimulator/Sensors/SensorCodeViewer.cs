using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SensorCodeViewer : MonoBehaviour {

    Dictionary<Sensor, TextMesh> sensor;
    public GameObject textMeshObj;
    public GameObject container;

    Sensor[] sensorArray;

	// Use this for initialization
	void Start () {

        sensor = new Dictionary<Sensor, TextMesh>();
        sensorArray =  GameObject.FindObjectsOfType<Sensor>() as Sensor[];
       
        StartCoroutine(updateSensorsCodeText());
        //this.transform.Rotate(new Vector3(0,90,0));
	}
	

    IEnumerator updateSensorsCodeText()
    {

        while (true)
        {

            var badKeys = sensor.Where(pair => pair.Key == null)
                        .ToList();

            foreach (var badKey in badKeys)
            {
                Destroy(badKey.Value);
                sensor.Remove(badKey.Key);
            }

            foreach (var element in sensor){

                TextMesh textMesh = new TextMesh();

                element.Value.text = element.Key.Code;
            }

           
            yield return new WaitForSeconds(0.5f);
        }
        
    }

// Update is called once per frame
void FixedUpdate () {
        

        sensorArray = GameObject.FindObjectsOfType<Sensor>();

        for (int i = 0; i < sensorArray.Length; i++)
        {
            if(sensorArray[i].ExportData)

                if (!sensor.ContainsKey(sensorArray[i]))
                {
                    TextMesh textMesh = Instantiate(textMeshObj, container.transform).GetComponent<TextMesh>();

                    textMesh.gameObject.transform.position = sensorArray[i].transform.position;

                    textMesh.transform.LookAt(textMesh.transform.position + Camera.main.transform.rotation * Vector3.forward,
                                             Camera.main.transform.rotation* Vector3.up);
           
                        sensor.Add(sensorArray[i], textMesh);
                }
                else
                {
                    TextMesh textMesh = new TextMesh();
               
                    if (sensor.TryGetValue(sensorArray[i], out textMesh))
                    {
                        textMesh.gameObject.transform.position = sensorArray[i].transform.position;

                        textMesh.transform.LookAt(textMesh.transform.position + Camera.main.transform.rotation * Vector3.forward,
                                                 Camera.main.transform.rotation * Vector3.up);
                    }
                }
            else
            {
                if (sensor.ContainsKey(sensorArray[i]))
                {
                    TextMesh s;
                    sensor.TryGetValue(sensorArray[i], out s);
                    Destroy(s.gameObject);
                    sensor.Remove(sensorArray[i]);
                }
                        
            }


        }

 
            
      
                

            

        
        

    }

}


