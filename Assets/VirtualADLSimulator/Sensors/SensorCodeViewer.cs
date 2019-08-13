using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SensorCodeViewer : MonoBehaviour {

    Dictionary<Sensor, TextMesh> sensor = new Dictionary<Sensor, TextMesh>();
    public GameObject textMeshObj;
    public GameObject container;

    Sensor[] sensorArray;
    private Coroutine coroutine;
	// Use this for initialization
	void Start () {

        sensorArray =  GameObject.FindObjectsOfType<Sensor>() as Sensor[];
        //this.transform.Rotate(new Vector3(0,90,0));
	}

    private void Awake()
    {
        //StartCoroutine(openTextFile());
    }




    private void OnDisable()
    {
        StopCoroutine(coroutine);
        coroutine = null;
       
        foreach (var element in sensor)
            if(element.Value != null)
                element.Value.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(updateSensorsCodeText());
            foreach (var element in sensor)
                element.Value.gameObject.SetActive(true);
        }

    
    }

    IEnumerator openTextFile()
    {

        while (true)
        {
            Debug.Log("Hola");

            yield return new WaitForSeconds(1);
        }

    }

    IEnumerator updateSensorsCodeText()
    {

        while (true)
        {
            if(sensor.Count != 0)
            {
                var badKeys = sensor.Where(pair => pair.Key == null)
                        .ToList();

                foreach (var badKey in badKeys)
                {
                    Destroy(badKey.Value.gameObject);
                    sensor.Remove(badKey.Key);
                }

                foreach (var element in sensor)
                {
                    element.Value.text = element.Key.Code;
   
                    element.Value.fontStyle = FontStyle.Normal;
                    element.Value.color = Color.white;

                }

                if (transform.GetComponent<SelectGameobjectCursor>().inCursorGameobject != null)
                {        
                    if (transform.GetComponent<SelectGameobjectCursor>().inCursorGameobject.GetComponentInChildren<Sensor>() != null)
                    {
                        
                        TextMesh textMesh = new TextMesh();
                        if (sensor.TryGetValue(transform.GetComponent<SelectGameobjectCursor>().inCursorGameobject.GetComponentInChildren<Sensor>(), out textMesh) )
                        {
                            textMesh.fontStyle = FontStyle.Bold;
                            textMesh.color = Color.blue;
                        }

                       
                    }
                   
                }

                foreach (var e in transform.GetComponent<SelectGameobjectCursor>().gameObjectsSelected)
                {
                    TextMesh textMesh = new TextMesh();
                    if(e.GetComponentInChildren<Sensor>() != null)
                        if (sensor.TryGetValue(e.GetComponentInChildren<Sensor>(), out textMesh))
                        {
                            textMesh.fontStyle = FontStyle.Bold;
                            textMesh.color = Color.blue;
                        }
                }
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


