using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransformActuator :  Actuator<TransformActuator>, IActuator {

    public bool enableRotationActuator;
    public bool enablePositionActuator;

    public bool enableReverseAction;
    public float rotationDegreesPerSecond;
    public float positionUnitsPerSecond;

    public bool isLocalRotation;
    public Vector3 targetRotation;
    public Vector3 originalRotation;



    public bool isLocalPosition;
    public Vector3 targetPosition;
    public Vector3 originalPosition;

    public void doAction()
    {
        StopAllCoroutines();
        StartCoroutine(actionCoroutine());
        
    }

     IEnumerator actionCoroutine()
    {
        Quaternion _targetRotation = Quaternion.identity;
        Vector3 _targetPosition = Vector3.zero;

        Quaternion currentRotation = new Quaternion();
        Vector3 currentPosition = new Vector3(); ;

        currentRotation = (isLocalRotation) ? transform.localRotation : transform.rotation;
        currentPosition = (isLocalPosition) ? transform.localPosition : transform.position;

        if (currentRotation == Quaternion.Euler(originalRotation) || !enableReverseAction)
            _targetRotation = Quaternion.Euler(targetRotation);
        else
            _targetRotation = Quaternion.Euler(originalRotation);
       
        if (currentPosition == originalPosition || !enableReverseAction)
            _targetPosition = targetPosition;
        else
            _targetPosition = originalPosition;

        while ( (currentRotation != _targetRotation && enableRotationActuator) || (currentPosition != _targetPosition && enablePositionActuator))
        {

   
            currentRotation = (isLocalRotation) ? transform.localRotation : transform.rotation;
            currentPosition = (isLocalPosition) ? transform.localPosition : transform.position;

            float rotationStep = rotationDegreesPerSecond * Time.deltaTime;
            float positionStep = positionUnitsPerSecond * Time.deltaTime;

            if (enableRotationActuator)
            {
                if(isLocalRotation)
                    transform.localRotation = Quaternion.RotateTowards(currentRotation, _targetRotation, rotationStep);
                else
                    transform.rotation = Quaternion.RotateTowards(currentRotation, _targetRotation, rotationStep);
            }

            if (enablePositionActuator)
            {
                if(isLocalPosition)
                    transform.localPosition = Vector3.MoveTowards(currentPosition, _targetPosition, positionStep);
                else
                    transform.position = Vector3.MoveTowards(currentPosition, _targetPosition, positionStep);


            }



            yield return new WaitForSeconds(0);
        }
    }

    // Use this for initialization
    void Start () {
        originalPosition = (isLocalPosition) ? transform.localPosition : transform.position;
        originalRotation = (isLocalRotation) ? transform.localEulerAngles : transform.eulerAngles;

        

        //StartCoroutine(actionCoroutine());

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}


#if UNITY_EDITOR
[CustomEditor(typeof(TransformActuator))]
public class MyScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as TransformActuator;

        bool setTargetPosition = false;

        if (myScript.enableRotationActuator || myScript.enablePositionActuator)
        {
            myScript.enableReverseAction = GUILayout.Toggle(myScript.enableReverseAction, "Do reverse action in second activate");
            GUILayout.Space(10);
        }



        myScript.enableRotationActuator = GUILayout.Toggle(myScript.enableRotationActuator, "Rotation");
        GUILayout.Space(5);



        if (myScript.enableRotationActuator)
        {
            myScript.isLocalRotation = GUILayout.Toggle(myScript.isLocalRotation, "Actuate in local rotation coordinates?");
            myScript.rotationDegreesPerSecond = EditorGUILayout.Slider("Degrees per second", myScript.rotationDegreesPerSecond, 0, 360);
            myScript.targetRotation.x = EditorGUILayout.Slider("Target rotation x: ", myScript.targetRotation.x, -360, 360);
            myScript.targetRotation.y = EditorGUILayout.Slider("Target rotation y: ", myScript.targetRotation.y, -360, 360);
            myScript.targetRotation.z = EditorGUILayout.Slider("Target rotation z: ", myScript.targetRotation.z, -360, 360);
        }

        GUILayout.Space(10);

        myScript.enablePositionActuator = GUILayout.Toggle(myScript.enablePositionActuator, "Position");
        GUILayout.Space(5);

        if (myScript.enablePositionActuator)
        {
            setTargetPosition = GUILayout.Toggle(setTargetPosition, "Get current position");
            if (!setTargetPosition)
            {
                myScript.isLocalPosition = GUILayout.Toggle(myScript.isLocalPosition, "Actuate in local position coordinates?");
                myScript.positionUnitsPerSecond = EditorGUILayout.Slider("Position units per second", myScript.positionUnitsPerSecond, 0, 50);
                myScript.targetPosition.x = EditorGUILayout.Slider("Target position x: ", myScript.targetPosition.x, -360, 360);
                myScript.targetPosition.y = EditorGUILayout.Slider("Target position y: ", myScript.targetPosition.y, -360, 360);
                myScript.targetPosition.z = EditorGUILayout.Slider("Target position z: ", myScript.targetPosition.z, -360, 360);
            }
            else
            {
                myScript.targetPosition = myScript.transform.localPosition;
                myScript.isLocalPosition = GUILayout.Toggle(myScript.isLocalPosition, "Actuate in local position coordinates?");
                myScript.positionUnitsPerSecond = EditorGUILayout.Slider("Position units per second", myScript.positionUnitsPerSecond, 0, 50);
                myScript.targetPosition.x = EditorGUILayout.Slider("Target position x: ", myScript.targetPosition.x, -360, 360);
                myScript.targetPosition.y = EditorGUILayout.Slider("Target position y: ", myScript.targetPosition.y, -360, 360);
                myScript.targetPosition.z = EditorGUILayout.Slider("Target position z: ", myScript.targetPosition.z, -360, 360);
            }

        }



    
           
        

    }
}

#endif