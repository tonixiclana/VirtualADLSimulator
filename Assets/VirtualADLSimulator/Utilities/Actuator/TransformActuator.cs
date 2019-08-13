using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class TransformActuator :  Actuator, IActuator {


    public Transform targetTransform;
    public bool enableRotationActuator;
    public bool enablePositionActuator;

    public bool enableReverseAction;
    public float rotationDegreesPerSecond;
    public float positionUnitsPerSecond;

    public bool isLocalRotation;
    public Vector3 targetRotation;
    public Vector3 originalRotation;

    public AudioClip actionAudio;
    public AudioClip reverseActionAudio;

    public bool isLocalPosition;
    public Vector3 targetPosition;
    public Vector3 originalPosition;



    public void doAction()
    {
 

        Quaternion currentRotation = new Quaternion();
        Vector3 currentPosition = new Vector3(); 



        currentRotation = (isLocalRotation) ? targetTransform.localRotation : targetTransform.rotation;
        currentPosition = (isLocalPosition) ? targetTransform.localPosition : targetTransform.position;

        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Stop();

        foreach (Light l in GetComponents<Light>())
        {
            l.enabled = (enableReverseAction && (currentPosition == originalPosition || currentRotation == Quaternion.Euler(originalRotation)));
        }

        foreach (Light l in GetComponentsInChildren<Light>())
        {
            l.enabled = (enableReverseAction && (currentPosition == originalPosition || currentRotation == Quaternion.Euler(originalRotation)));
        }

        StopAllCoroutines();
        StartCoroutine(actionCoroutine());
        
    }

    IEnumerator actionCoroutine()
    {
      

        Quaternion _targetRotation = Quaternion.identity;
        Vector3 _targetPosition = Vector3.zero;


        

        Quaternion currentRotation = new Quaternion();
        Vector3 currentPosition = new Vector3(); ;



        currentRotation = (isLocalRotation) ? targetTransform.localRotation : targetTransform.rotation;
        currentPosition = (isLocalPosition) ? targetTransform.localPosition : targetTransform.position;

  

        if (currentRotation == Quaternion.Euler(originalRotation) || !enableReverseAction)
        {
            
            _targetRotation = Quaternion.Euler(targetRotation);
        }
        else
        {
  
            _targetRotation = Quaternion.Euler(originalRotation);
        }

        if (currentPosition == originalPosition || !enableReverseAction)
        {
            
            _targetPosition = targetPosition;

        }
        else
        {
            
            _targetPosition = originalPosition;

        }

        if (enableReverseAction && (currentPosition == originalPosition || currentRotation == Quaternion.Euler(originalRotation)))
        {
            string temp = actionName;
            actionName = reverseActionName;
            reverseActionName = temp;
        }
        else
        {
            string temp = actionName;
            actionName = reverseActionName;
            reverseActionName = temp;
        }


        if (enableReverseAction && (currentPosition == originalPosition || currentRotation == Quaternion.Euler(originalRotation)))
        {
            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().PlayOneShot(actionAudio);
        }
        else
            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().PlayOneShot(reverseActionAudio == null ? actionAudio : reverseActionAudio);

        while ( (currentRotation != _targetRotation && enableRotationActuator) || (currentPosition != _targetPosition && enablePositionActuator))
        {

   
            currentRotation = (isLocalRotation) ? targetTransform.localRotation : targetTransform.rotation;
            currentPosition = (isLocalPosition) ? targetTransform.localPosition : targetTransform.position;

            float rotationStep = rotationDegreesPerSecond * Time.deltaTime;
            float positionStep = positionUnitsPerSecond * Time.deltaTime;

            if (enableRotationActuator)
            {
                if(isLocalRotation)
                    targetTransform.localRotation = Quaternion.RotateTowards(currentRotation, _targetRotation, rotationStep);
                else
                    targetTransform.rotation = Quaternion.RotateTowards(currentRotation, _targetRotation, rotationStep);
            }

            if (enablePositionActuator)
            {
                if(isLocalPosition)
                    targetTransform.localPosition = Vector3.MoveTowards(currentPosition, _targetPosition, positionStep);
                else
                    targetTransform.position = Vector3.MoveTowards(currentPosition, _targetPosition, positionStep);


            }



            yield return new WaitForSeconds(0);
        }
    }

    // Use this for initialization
    void Start () {
        //originalPosition = (isLocalPosition) ? transform.localPosition : transform.position;
        //originalRotation = (isLocalRotation) ? transform.localEulerAngles : transform.eulerAngles;
        if (targetTransform == null)
            targetTransform = transform;


        //StartCoroutine(actionCoroutine());

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}


#if UNITY_EDITOR
[CustomEditor(typeof(TransformActuator))]

public class MyScriptEditor : Editor { 


   

    override public void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        var myScript = target as TransformActuator;

        if (myScript.targetTransform == null)
            myScript.targetTransform = myScript.transform;

        bool setTargetPosition = false;
        bool setOriginalPosition = false;

        bool setTargetRotation = false;
        bool setOriginalRotation = false;

        myScript.targetTransform = (Transform)EditorGUILayout.ObjectField("Target Transform", myScript.targetTransform, typeof(Transform), true);
        myScript.actionName = EditorGUILayout.TextField("Action Name", myScript.actionName);
        myScript.actionAudio = (AudioClip)EditorGUILayout.ObjectField("Action Clip Audio", myScript.actionAudio, typeof(AudioClip), true);
        myScript.reverseActionAudio = (AudioClip)EditorGUILayout.ObjectField("Reverse Action Clip Audio", myScript.reverseActionAudio, typeof(AudioClip), true);

        if (myScript.enableRotationActuator || myScript.enablePositionActuator)
        {
            myScript.enableReverseAction = GUILayout.Toggle(myScript.enableReverseAction, "Do reverse action in second activate?");
            GUILayout.Space(10);
        }

        if (myScript.enableReverseAction)
            myScript.reverseActionName = EditorGUILayout.TextField("Reverse Action Name", myScript.reverseActionName);


        myScript.enableRotationActuator = GUILayout.Toggle(myScript.enableRotationActuator, "Rotation");
        GUILayout.Space(5);



        if (myScript.enableRotationActuator)
        {
            setOriginalRotation = GUILayout.Toggle(setTargetRotation, "Set original current rotation");
            setTargetRotation = GUILayout.Toggle(setTargetRotation, "Set target current Rotation");

            myScript.originalRotation = (setOriginalRotation) ? myScript.targetTransform.localRotation.eulerAngles : myScript.originalRotation;
            myScript.targetRotation = (setTargetRotation) ? myScript.targetTransform.localRotation.eulerAngles : myScript.targetRotation;

            myScript.isLocalRotation = GUILayout.Toggle(myScript.isLocalRotation, "Actuate in local rotation coordinates?");
            myScript.rotationDegreesPerSecond = EditorGUILayout.Slider("Degrees per second", myScript.rotationDegreesPerSecond, 0, 360);

            myScript.originalRotation = EditorGUILayout.Vector3Field("Original rotation: ", myScript.originalRotation);

            myScript.targetRotation = EditorGUILayout.Vector3Field("Target rotation: ", myScript.targetRotation);
   
        }

        GUILayout.Space(10);

        myScript.enablePositionActuator = GUILayout.Toggle(myScript.enablePositionActuator, "Position");
        GUILayout.Space(5);

        if (myScript.enablePositionActuator)
        {
            setOriginalPosition = GUILayout.Toggle(setTargetPosition, "Set original current position");
            setTargetPosition = GUILayout.Toggle(setTargetPosition, "Set target current position");

            myScript.originalPosition = (setOriginalPosition) ? myScript.targetTransform.localPosition : myScript.originalPosition;
            myScript.targetPosition = (setTargetPosition)? myScript.targetTransform.localPosition : myScript.targetPosition;

            myScript.isLocalPosition = GUILayout.Toggle(myScript.isLocalPosition, "Actuate in local position coordinates?");
            myScript.positionUnitsPerSecond = EditorGUILayout.Slider("Position units per second", myScript.positionUnitsPerSecond, 0, 1000);

            myScript.originalPosition = EditorGUILayout.Vector3Field("Original Position: ", myScript.originalPosition);
            myScript.targetPosition = EditorGUILayout.Vector3Field("Target Position: ", myScript.targetPosition);
            

        }

    }
}

#endif