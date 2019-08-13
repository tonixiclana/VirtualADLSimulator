/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Control the firts person camera
 */

using UnityEngine;

/// <summary>
/// Control the firts person camera
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("ADLVirtualSimulator/FirstPersonCamera")]

public class FirstPersonCamera : MonoBehaviour
{
    /// <summary>
    /// The maximal rotations of camera in diferents axes
    /// </summary>
    [Tooltip("The maximal rotations of camera in diferents axes")]
    public Vector2 clampInDegrees = new Vector2(360, 180);

    /// <summary>
    /// Lock the cursor in a screen
    /// </summary>
    [Tooltip("Lock the cursor in a screen")]
    public bool lockCursor = true;

    /// <summary>
    /// To enable or no the cursor movement
    /// </summary>
    [Tooltip("To enable or no the cursor movement")]
    public bool enableCursorMovement = true;

    /// <summary>
    /// The sensivity in each axe
    /// </summary>
    [Tooltip("The sensivity in each axe")]
    public Vector2 sensitivity = new Vector2(2, 2);

    /// <summary>
    /// Smoothing factor in eash axe
    /// </summary>
    [Tooltip("Smoothing factor in eash axe")]
    public Vector2 smoothing = new Vector2(3, 3);

    /// <summary>
    /// Target direction
    /// </summary>
    [Tooltip("Target direction of the camera")]
    public Vector2 targetDirection;

    /// <summary>
    /// Target character direction
    /// </summary>
    [Tooltip("Target character direction")]
    public Vector2 targetCharacterDirection;


    /// <summary>
    /// Camera GameObject
    /// </summary>
    [Tooltip("Camera GameObject")]
    public Camera camera;

    /*
        /// <summary>
        /// Assign this if there's a parent object controlling motion, such as a Character Controller.
        /// Yaw rotation will affect this object instead of the camera if set.
        /// </summary>
        public GameObject characterBody;
        */

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

    void Start()
    {
        // Ensure the cursor is always locked when set
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
            Cursor.lockState = CursorLockMode.None;

        // Set target direction to the camera's initial orientation.
        //targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (GetComponent<Rigidbody>())
            targetCharacterDirection = GetComponent<Rigidbody>().transform.localRotation.eulerAngles;
    }

    /// <summary>
    /// Change lock state cursor
    /// </summary>
    public void changeStateCursor()
    {
        lockCursor = !lockCursor;
    }

    /// <summary>
    /// Controller the rotation of Character
    /// </summary>
    void FixedUpdate()
    {
        // Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        camera.transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right * Time.deltaTime) * targetOrientation;

        // If there's a character body that acts as a parent to the camera
        if (GetComponent<Rigidbody>())
        {
      
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
            GetComponent<Rigidbody>().transform.localRotation = yRotation * targetCharacterOrientation;
        }
        else
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            camera.transform.localRotation *= yRotation;
        }
    }
}