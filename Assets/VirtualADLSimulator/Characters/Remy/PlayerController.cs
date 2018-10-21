using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float speedSmoothTime = 0.1f;
	public float gravity = -12;
	public AudioClip[] footStepSounds;

    public float speedSmoothVelocity = 0, currentSpeed = 0, targetSpeed = 0;


    private Vector2 inputDir;
	private bool running;
	public bool isGrounded;
	private float collisionCount = 0;

	private Animator animator;
	private AudioSource audioSource;

	void Start () {
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
		//StartCoroutine(playFootStepSounds());
		//StartCoroutine (movePlayer());
	}


    void FixedUpdate () {
		if(Input.GetKey (KeyCode.E))
			animator.SetBool ("HandMove", true);

        running = KeyCapture.runButtom;
        inputDir = KeyCapture.axis;
        isGrounded = (GetComponent<Rigidbody>().velocity.y < -3) ? false : true;
        targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        animator.SetFloat("speedPercent", ((running) ? 1 : .5f) * inputDir.magnitude, speedSmoothTime, Time.deltaTime);
        //Debug.Log ("currentSpeed: " + currentSpeed);
        if (inputDir.y != 0 && isGrounded)
            //this.GetComponent<Rigidbody>().velocity = inputDir.y * transform.forward * currentSpeed * Time.deltaTime;
        transform.position += inputDir.y * transform.forward * currentSpeed * Time.deltaTime;
        if (inputDir.x != 0 && isGrounded)
            //this.GetComponent<Rigidbody>().velocity = inputDir.x * transform.right * currentSpeed * Time.deltaTime;
        transform.position += inputDir.x * transform.right * currentSpeed * Time.deltaTime;

        if (currentSpeed < 0.001f)
            currentSpeed = 0;
    }

	void OnCollisionEnter (Collision colInfo)
	{
		collisionCount++;
	}

	void OnCollisionExit (Collision colInfo)
	{
		collisionCount--;
	}

	void setIsGrounded(bool isGnd){
		isGrounded = isGnd;
	}

	void PlayFootStepAudio()
	{
		/*if (!grounded)
		{
			return;
		}*/



		/*if(GetComponent<Rigidbody>().velocity.sqrMagnitude > 0)
			stepCycle += stepLong * Time.fixedDeltaTime;*/
		
		// pick & play a random footstep sound from the array,
		// excluding sound at index 0

	}
		
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
				audioSource.clip = footStepSounds [n];
				audioSource.PlayOneShot (audioSource.clip);
				// move picked sound to index 0 so it's not picked next time
				footStepSounds [n] = footStepSounds [0];
				footStepSounds [0] = audioSource.clip;
			}
			yield return new WaitForSeconds (0);
		}
	}

}
