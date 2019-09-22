/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description The PlayerController Class control the physics, movements 
 * and rest of functions relational with the players control
 */

using System.Collections;
using UnityEngine;

/**
 * <summary>
 * PlayerController Class control the physics, movements 
 * and rest of functions relational with the players control
 * </summary>  
 */
 [RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[AddComponentMenu("ADLVirtualSimulator/PlayerController")]
public class PlayerController : MonoBehaviour {
    /// <summary>
    /// The walk speed of the Character
    /// </summary>
    [Tooltip("Walk Speed")]
    public float walkSpeed = 1;

    /// <summary>
    /// The run speed of the Character
    /// </summary>
    [Tooltip("Run Speed")]
    public float runSpeed = 6;

    /// <summary>
    /// Smoothdamp of the movement increment
    /// </summary>
    [Tooltip("Smoothdamp of the movement increment")]
    public float speedSmoothTime = 0.1f;

    /// <summary>
    /// The diferents foot step sounds of the character
    /// </summary>
    [Tooltip("The diferents foot step sounds of the character")]
    public AudioClip[] footStepSounds;

    /// <summary>
    /// The current speed of the Character
    /// </summary>
    [Tooltip("The current speed of the Character")]
    public float currentSpeed = 0;

    /// <summary>
    /// Indicate if the Character is in the floor
    /// </summary>
    [Tooltip("Indicate if the Character is in the floor")]
    public bool isGrounded;

    /*
    /// <summary>
    /// Animator of the Character
    /// </summary>
    [Tooltip("Animator of the Character")]
    public Animator animator;

    /// <summary>
    /// Audio Source for the sounds that emits the Character
    /// </summary>
    [Tooltip("Audio Source for the sounds that emits the Character")]
    public AudioSource audioSource;
    */

    /// <summary>
    /// The target and maximal speed that the character will take
    /// </summary>
    private float targetSpeed = 0;

    /// <summary>
    /// The time left by arrive finish of smooth
    /// </summary>
    private float currentVelocity = 0;

    /// <summary>
    /// Vector to save the axis input values
    /// </summary>
    private Vector2 inputDir;

    /// <summary>
    /// Bool for indicate is Character is running
    /// </summary>
    private bool running;

	void Start () {
		//StartCoroutine(playFootStepSounds());
		//StartCoroutine (movePlayer());
	}

    void FixedUpdate () {

        //[NEED UPDATE]To activate a animation 
        if (Input.GetKey (KeyCode.E))
			GetComponent<Animator>().SetBool ("HandMove", true);

        //Get the inputs
        running = Input.GetButton("Run");
        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        //[NEED UPDATE] Set the grounded flag if the velocity of fallen en less than 3
        isGrounded = (GetComponent<Rigidbody>().velocity.y < -3) ? false : true;

        //Calculate the target speed that the character will take
        targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;

        //Apply a SmoothDamp function at the increment of speed movement 
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, speedSmoothTime);

        //[NEED UPDATE] modify the speedPercent of animation
        GetComponent<Animator>().SetFloat("speedPercent", ((running) ? 1 : .5f) * inputDir.magnitude, speedSmoothTime, Time.deltaTime);

        //Move the Character when is in ground
        if (inputDir.y != 0 && isGrounded)
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + inputDir.y * transform.forward * currentSpeed * Time.deltaTime);
        //transform.position += inputDir.y * transform.forward * currentSpeed * Time.deltaTime;

        if (inputDir.x != 0 && isGrounded)
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + inputDir.x * transform.right * currentSpeed * Time.deltaTime);
        //transform.position += inputDir.x * transform.right * currentSpeed * Time.deltaTime;

        //[NEED UPDATE] Set the currentSpeed at 0 when the speed is negligible 
        if (currentSpeed < 0.001f)
            currentSpeed = 0;
    }

    /// <summary>
    /// This function execute when the actual gameobject detect a collision with another collider
    /// </summary>
    /// <param name="colInfo"></param>
	void OnCollisionEnter (Collision colInfo)
	{
		
	}

    /// <summary>
    /// This function execute when the actual gameobject left the collision with another collider
    /// </summary>
    /// <param name="colInfo"></param>
    void OnCollisionExit (Collision colInfo)
	{

	}
		
    /// <summary>
    /// This Coroutine control the Foot Step Sounds
    /// </summary>
    /// <returns></returns>
	IEnumerator playFootStepSounds()
	{
		float stepCycle = 2;
		float nextStep = stepCycle / 2f;
		float stepLong;

		while (true) 
		{
			stepLong = GetComponent<Rigidbody> ().velocity.magnitude;
			Debug.Log ("StepLong: " + stepLong);
			//Debug.Log(waterMask.GetComponent<Rigidbody> ().velocity.sqrMagnitude);
			if (isGrounded) {
				int n = Random.Range (1, footStepSounds.Length);
                GetComponent<AudioSource>().clip = footStepSounds [n];
                GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
				// move picked sound to index 0 so it's not picked next time
				footStepSounds [n] = footStepSounds [0];
				footStepSounds [0] = GetComponent<AudioSource>().clip;
			}
			yield return new WaitForSeconds (0);
		}
	}

}
