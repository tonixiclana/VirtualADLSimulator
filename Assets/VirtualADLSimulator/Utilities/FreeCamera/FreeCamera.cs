/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Control the functions for the camera to allow a free camera experience
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the functions for the camera to allow a free camera experience
/// </summary>
[RequireComponent(typeof(Camera))]
[System.Serializable]
public class FreeCamera : MonoBehaviour {


    private Camera _camera;

    /// <summary>
    /// Target direction
    /// </summary>
    [Tooltip("Target direction of the camera")]
    public Vector2 targetDirection;

    /// <summary>
    /// The sensivity in each axe mouse
    /// </summary>
    [Tooltip("The sensivity in each axe mouse")]
    public Vector2 sensitivity = new Vector2(2, 2);

    /// <summary>
    /// Smoothing factor in each axe mouse
    /// </summary>
    [Tooltip("Smoothing factor in eash axe")]
    public Vector2 smoothing = new Vector2(3, 3);

    /// <summary>
    /// Time for setup the Smooth of the movement of camera
    /// </summary>
    [Tooltip("Time for setup the Smooth of the movement of camera")]
    public float speedSmoothTime = 0.1f;

    /// <summary>
    /// Movement speed of camera
    /// </summary>
    [Tooltip("Movement speed of camera")]
    public float movementSpeed = 1;

    /// <summary>
    /// The current speed of camera
    /// </summary>
    private float currentSpeed = 0;

    /// <summary>
    /// The target and maximal speed that the camera will take
    /// </summary>
    private float targetSpeed = 0;

    /// <summary>
    /// Vector to save the axis input values
    /// </summary>
    private Vector3 inputDir;

    /// <summary>
    /// The time left by arrive finish of smooth
    /// </summary>
    private float currentVelocity = 0;

    /// <summary>
    /// Vector with the position of the mouse
    /// </summary>
    Vector2 _mouseAbsolute;

    /// <summary>
    /// Content the mouse delta information
    /// </summary>
    Vector2 mouseDelta;

    /// <summary>
    /// The smoothing delta
    /// </summary>
    Vector2 _smoothMouse;

    // Use this for initialization
    void Start () {
        _camera = this.GetComponent<Camera>();

        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

	}

    private void FixedUpdate()
    {

        //Rotate when pulse the middle mouse button
        if (Input.GetMouseButton(2))
        {
            // Allow the script to clamp based on a desired target value.
            var targetOrientation = Quaternion.Euler(targetDirection);

            // Get raw mouse input for a cleaner reading on more sensitive mice.
            mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Scale input against the sensitivity setting and multiply that against the smoothing value.
            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

            // Interpolate mouse movement over time to apply smoothing delta.
            _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
            _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

            // Find the absolute mouse movement value from point zero.
            _mouseAbsolute += _smoothMouse;

            _camera.transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right * Time.deltaTime)
                * targetOrientation;
            _camera.transform.localRotation *= Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
        }
        



        inputDir =  new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        targetSpeed = movementSpeed * inputDir.magnitude;

        //Apply a SmoothDamp function at the increment of speed movement 
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, speedSmoothTime);

        GetComponent<Camera>().transform.position += inputDir.y * transform.forward * currentSpeed * Time.deltaTime;
        GetComponent<Camera>().transform.position += inputDir.x * transform.right * currentSpeed * Time.deltaTime;
    }
}
