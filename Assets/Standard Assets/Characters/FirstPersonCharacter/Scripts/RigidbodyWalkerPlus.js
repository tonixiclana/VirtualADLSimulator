#pragma strict
#pragma implicit
#pragma downcast

@script RequireComponent ( Rigidbody, CapsuleCollider, AudioSource)

var canControl : boolean = true;
private var useFixedUpdate : boolean = true;
private var canSendMessages = false ;

// The current global direction we want the character to move in.
private var inputMoveDirection : Vector3 = Vector3.zero;

// Is the jump button held down? We use this interface instead of checking
// for the jump button directly so this script can also be used by AIs.
private var inputJump : boolean = false;

private var grounded : boolean = true;
private var groundNormal : Vector3 = Vector3.up ;

private var lastGroundNormal : Vector3 = Vector3.zero;
private var tr : Transform;

var jumping : S_CharacterMotorJumping = S_CharacterMotorJumping();
var movement : S_CharacterMotorMovement = S_CharacterMotorMovement();

private var hit : RaycastHit;

function OnCollisionStay ( colInfo : Collision )
{
	for ( var contact : ContactPoint in colInfo.contacts ) 
	{
		 
		// If we're actually touching the ground, not just some wall.
		if ( Vector3.Angle ( contact.normal, Vector3.up ) < controller.slopeLimit ) 
		{
			 Debug.DrawRay(contact.point, contact.normal, Color.green, 2, false);
			grounded = true;
		
			if (contact.normal.y > 0 && contact.normal.y > groundNormal.y ) {
				if ((contact.point - movement.lastHitPoint).sqrMagnitude > 0.001 || lastGroundNormal == Vector3.zero)
					groundNormal = contact.normal;
				else
					groundNormal = lastGroundNormal;
				
			}
		}
	}
}

class S_CharacterMotorMovement {
	// The maximum horizontal speed when moving
	var maxForwardSpeed : float = 10.0;
	var maxSidewaysSpeed : float = 10.0;
	var maxBackwardsSpeed : float = 10.0;
	
	// How much control does the character have in the air? 0 = none, 1 = full.
	var airControlAmt : float = 0.5 ;

	// Curve for multiplying speed based on slope (negative = downwards)
	@System.NonSerialized
	var slopeSpeedMultiplier : AnimationCurve = AnimationCurve(Keyframe(-90, 1), Keyframe(0, 1), Keyframe(90, 0)); // not used atm
	
	// How fast does the character change speeds?  Higher is faster.
	var maxGroundAcceleration : float = 30.0;
	var maxAirAcceleration : float = 20.0;

	// The gravity for the character
	var gravity : float = 10.0;
	var maxFallSpeed : float = 20.0;

	// For the next variables, @System.NonSerialized tells Unity to not serialize the variable or show it in the inspector view.
	// Very handy for organization!

	// The last collision flags returned from controller.Move
	@System.NonSerialized
	var collisionFlags : CollisionFlags; 

	// We will keep track of the character's current velocity,
	@System.NonSerialized
	var velocity : Vector3;
	
	// This keeps track of our current velocity while we're not grounded
	@System.NonSerialized
	var frameVelocity : Vector3 = Vector3.zero;
	
	@System.NonSerialized
	var hitPoint : Vector3 = Vector3.zero;
	
	@System.NonSerialized
	var lastHitPoint : Vector3 = Vector3(Mathf.Infinity, 0, 0);
}


enum S_MovementTransferOnJump {
	None, // The jump is not affected by velocity of floor at all.
	InitTransfer, // Jump gets its initial velocity from the floor, then gradualy comes to a stop.
	PermaTransfer, // Jump gets its initial velocity from the floor, and keeps that velocity until landing.
	PermaLocked // Jump is relative to the movement of the last touched floor and will move together with that floor.
}

// We will contain all the jumping related variables in one helper class for clarity.
class S_CharacterMotorJumping {
	// Can the character jump?
	var enabled : boolean = true;

	// How many times can the player jump ?
	// Default: 1. Doublejump: 2. Etc.
	var multijumps : int = 1 ;
	@System.NonSerialized
	var multijumpsGetExtraHeight = true ;
	@System.NonSerialized
	var jumpCounter : int = 0 ;

	// Cooldown for jumps so controls can't just rapid fire jumps.
	// This is for convenience.
	@System.NonSerialized
	var jumpCooldown : float = 0.5 ; // Change this one. 
	@System.NonSerialized
	var jumpCooldownTimer : float = 0.0 ; // Don't change this one.

	@System.NonSerialized
	var jump : Array ;

	// How high do we jump when pressing jump and letting go immediately
	var baseHeight : float = 1.0;
	
	// We add extraHeight units (meters) on top when holding the button down longer while jumping
	var extraHeight : float = 4.1;
	
	// How much does the character jump out perpendicular to the surface on walkable surfaces?
	// 0 means a fully vertical jump and 1 means fully perpendicular.
	var perpAmount : float = 0.0;
	
	// How much does the character jump out perpendicular to the surface on too steep surfaces?
	// 0 means a fully vertical jump and 1 means fully perpendicular.
	var steepPerpAmount : float = 0.5;
	
	// For the next variables, @System.NonSerialized tells Unity to not serialize the variable or show it in the inspector view.
	// Very handy for organization!

	// Are we jumping? (Initiated with jump button and not grounded yet)
	// To see if we are just in the air (initiated by jumping OR falling) see the grounded variable.
	@System.NonSerialized
	var jumping : boolean = false;
	
	@System.NonSerialized
	var holdingJumpButton : boolean = false;

	// the time we jumped at (Used to determine for how long to apply extra jump power after jumping.)
	@System.NonSerialized
	var lastStartTime : float = 0.0;
	
	@System.NonSerialized
	var lastButtonDownTime : float = -100;
	
	@System.NonSerialized
	var jumpDir : Vector3 = Vector3.up;
}


class S_CharacterMotorMovingPlatform {
	var enabled : boolean = true;
	
	var movementTransfer : S_MovementTransferOnJump = S_MovementTransferOnJump.PermaTransfer;
	
	@System.NonSerialized
	var hitPlatform : Transform;
	
	@System.NonSerialized
	var activePlatform : Transform;
	
	@System.NonSerialized
	var activeLocalPoint : Vector3;
	
	@System.NonSerialized
	var activeGlobalPoint : Vector3;
	
	@System.NonSerialized
	var activeLocalRotation : Quaternion;
	
	@System.NonSerialized
	var activeGlobalRotation : Quaternion;
	
	@System.NonSerialized
	var lastMatrix : Matrix4x4;
	
	@System.NonSerialized
	var platformVelocity : Vector3;
	
	@System.NonSerialized
	var newPlatform : boolean;
}

class S_CharacterController {
	// mimics Char controller for those few times where one is needed
	
	var height = 2.0 ;
	var radius = 0.5 ;
	var slopeLimit = 65 ;
	var stepOffset = 0.3 ;
	var skinWidth = 0.08 ;
	var minMoveDistance = 0.0 ;

	var center = Vector3.zero ;
}
private var controller : S_CharacterController = S_CharacterController() ;
	

function Awake ()
{
	GetComponent.<Rigidbody>().freezeRotation = true;
	GetComponent.<Rigidbody>().useGravity = false;
	tr = transform;
}


function Update () {
}

function FixedUpdate ()
{
	// Get the input vector from kayboard or analog stick
	var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	
	if (directionVector != Vector3.zero) {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;
		
		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);
		
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;
		
		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
		ProgressStepCycle();		//---------------------------------------------- StepSounds
	}
	
	// Apply the direction to the RigidbodyCharacterMotor
	inputMoveDirection = transform.rotation * directionVector;
	inputJump = Input.GetButton("Jump");
	var velChange = Vector3.zero ;
	
	// Get desired velocity change
	velChange += ApplyInputVelocityChange ( GetComponent.<Rigidbody>().velocity ) ;

	// Get jump stuff applied
	velChange += ApplyGravityAndJumping ( GetComponent.<Rigidbody>().velocity ) ;

	// Apply a force that attempts to reach our target velocity
	GetComponent.<Rigidbody>().AddForce( velChange, ForceMode.VelocityChange ) ;

	grounded = false;
}

private function ApplyInputVelocityChange (velocity : Vector3) 
{
	if (!canControl)
		inputMoveDirection = Vector3.zero;
	// Find desired velocity
	var desiredVelocity : Vector3;
	// Log Max velocity allowed
	var maxVelocityChange : float = GetMaxAcceleration(grounded) * Time.deltaTime;
	if ( grounded && TooSteep() ) 
	{
		// do nothing. Let the rigidbody handle the velocity
		// todo: this should really be more gradual, rather than just going from full control to 0 control @ toosteep.
	}
	else if ( velocity.sqrMagnitude > movement.maxFallSpeed * movement.maxFallSpeed) 
	{
		// Player is moving really really fast (maybe from an explosion).
		// Let this happen, but add some drag
		desiredVelocity = velocity * ( 1 - .05 ) ;
	} 
	else 
	{

		desiredVelocity = GetDesiredHorizontalVelocity();
	}
	if (grounded)
		desiredVelocity = AdjustGroundVelocityToNormal(desiredVelocity, groundNormal);
	else
		velocity.y = 0;
	
	// Enforce max velocity change
	var velocityChangeVector : Vector3 = (desiredVelocity - velocity);
	if (velocityChangeVector.sqrMagnitude > maxVelocityChange * maxVelocityChange) 
	{
		velocityChangeVector = velocityChangeVector.normalized * maxVelocityChange;
	}
	// If we're in the air, only apply the amount of control we have
	if ( ! grounded ) 
	{
		velocityChangeVector *= movement.airControlAmt ;
	}

	// If we're on the ground and don't have control we do apply it - it will correspond to friction.
	if ( ! ( grounded || canControl ) ) 
	{
		velocityChangeVector = Vector3.zero ; 
	}
	
	if (grounded) {

		//todo: add a func so rigidbody adjusts for upcoming stair / ramp
		// needs to move up by needed amt
		// does extra vel.y need to be added when going down?
	}

	return velocityChangeVector ;
}

private function ApplyGravityAndJumping (velocity : Vector3) 
{

	
	// Init the vel change at zero
	var velChange = Vector3.zero ;

	if ( inputJump && ! jumping.holdingJumpButton && jumping.lastButtonDownTime < 0 && canControl && ( Time.time > jumping.jumpCooldownTimer ) && ( jumping.jumpCounter < jumping.multijumps ) ) 
	{
		// Record time of button down. Used to trigger delayed jumps, rather than just firing jump func here.
		jumping.lastButtonDownTime = Time.time ;
	}

	// Jump only if the jump button was pressed down in the last 0.2 seconds.
	// We use this check instead of checking if it's pressed down right now
	// because players will often try to jump in the exact moment when hitting the ground after a jump
	// and if they hit the button a fraction of a second too soon and no new jump happens as a consequence,
	// it's confusing and it feels like the game is buggy.
	if ( Time.time - jumping.lastButtonDownTime < 0.2 ) 
	{
		jumping.jumping = true;
		jumping.lastStartTime = Time.time;
		jumping.lastButtonDownTime = -100;
		jumping.holdingJumpButton = true;
		jumping.jumpCounter++ ;
		jumping.jumpCooldownTimer = jumping.jumpCooldown + Time.time ;
		// Calculate the jumping direction
		if ( grounded && TooSteep() )
		{
			jumping.jumpDir = Vector3.Slerp(Vector3.up, groundNormal, jumping.steepPerpAmount);
		}
		else
		{
			jumping.jumpDir = Vector3.Slerp(Vector3.up, groundNormal, jumping.perpAmount);
		}
		
		// Apply the jumping force to the velocity. Cancel any vertical velocity first.
		velocity.y = 0;
		GetComponent.<Rigidbody>().velocity.y = 0 ;
		velChange += jumping.jumpDir * CalculateJumpVerticalSpeed (jumping.baseHeight);
		
		if ( canSendMessages ) 
		{
			SendMessage("OnJump", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	// When jumping up we don't apply gravity for some time when the user is holding the jump button.
	// This gives more control over jump height by pressing the button longer.
	if ( jumping.jumping && jumping.holdingJumpButton ) 
	{
		// If we can jump an extra height (if we're on our first jump, or multijump extra height is ok'd)
		// Calculate the duration that the extra jump force should have effect.
		// If we're still less than that duration after the jumping time, apply the force.
		if ( ( jumping.jumpCounter == 1 || ( jumping.multijumpsGetExtraHeight && jumping.jumpCounter < jumping.multijumps ) ) && ( Time.time < jumping.lastStartTime + jumping.extraHeight / CalculateJumpVerticalSpeed ( jumping.baseHeight ) ) ) 
		{
			// Negate the gravity we just applied, except we push in jumpDir rather than jump upwards.
			velChange += jumping.jumpDir * movement.gravity * Time.deltaTime;
		}
	}
	// Apply gravity no matter what we're doing. 
	velChange.y += -movement.gravity * Time.deltaTime;
	
	// Make sure we don't fall any faster than maxFallSpeed. This gives our character a terminal velocity.
	velocity.y = Mathf.Max (velocity.y, -movement.maxFallSpeed);

	if ( grounded && ! inputJump ) 
	{
		jumping.jumpCounter = 0 ;
	}

	if ( ! inputJump ) 
	{
		jumping.holdingJumpButton = false ;
	}
	if ( ! canControl ) 
	{
		jumping.holdingJumpButton = false;
		jumping.lastButtonDownTime = -100;
		jumping.jumpCounter = 0 ;
	}

	return velChange ;
}

private function GetDesiredHorizontalVelocity () {
	// Find desired velocity
	var desiredLocalDirection : Vector3 = tr.InverseTransformDirection(inputMoveDirection);
	var maxSpeed : float = MaxSpeedInDirection(desiredLocalDirection);
	if (grounded) {
		// Modify max speed on slopes based on slope speed multiplier curve
		var movementSlopeAngle = Mathf.Asin(movement.velocity.normalized.y)  * Mathf.Rad2Deg;
		maxSpeed *= movement.slopeSpeedMultiplier.Evaluate(movementSlopeAngle);
	}
	var horizontalVelocity = tr.TransformDirection ( desiredLocalDirection ) ;
	horizontalVelocity.y = 0 ;
	horizontalVelocity = horizontalVelocity.normalized * maxSpeed ;

	return horizontalVelocity ;
}

private function AdjustGroundVelocityToNormal (hVelocity : Vector3, groundNormal : Vector3) : Vector3 
{
	var sideways : Vector3 = Vector3.Cross(Vector3.up, hVelocity);
	return Vector3.Cross(sideways, groundNormal).normalized * hVelocity.magnitude;
}

private function IsGroundedTest () {
	return (groundNormal.y > 0.01);
}

function GetMaxAcceleration (grounded : boolean) : float {
	// Maximum acceleration on ground and in air
	if (grounded)
		return movement.maxGroundAcceleration;
	else
		return movement.maxAirAcceleration;
}

function CalculateJumpVerticalSpeed (targetJumpHeight : float) {
	// From the jump height and gravity we deduce the upwards speed 
	// for the character to reach at the apex.
	return Mathf.Sqrt (2 * targetJumpHeight * movement.gravity);
}

function IsJumping () {
	return jumping.jumping;
}

function IsTouchingCeiling () {
	return (movement.collisionFlags & CollisionFlags.CollidedAbove) != 0;
}

function IsGrounded () {
	return grounded;
}

function TooSteep () {
	return (groundNormal.y <= Mathf.Cos(controller.slopeLimit * Mathf.Deg2Rad));
}

function GetDirection () {
	return inputMoveDirection;
}

function SetControllable (controllable : boolean) {
	canControl = controllable;
}

// Project a direction onto elliptical quater segments based on forward, sideways, and backwards speed.
// The function returns the length of the resulting vector.
function MaxSpeedInDirection (desiredMovementDirection : Vector3) : float {
	if (desiredMovementDirection == Vector3.zero)
		return 0;
	else {
		var zAxisEllipseMultiplier : float = (desiredMovementDirection.z > 0 ? movement.maxForwardSpeed : movement.maxBackwardsSpeed) / movement.maxSidewaysSpeed;
		var temp : Vector3 = new Vector3(desiredMovementDirection.x, 0, desiredMovementDirection.z / zAxisEllipseMultiplier).normalized;
		var length : float = new Vector3(temp.x, 0, temp.z * zAxisEllipseMultiplier).magnitude * movement.maxSidewaysSpeed;
		return length;
	}
}

function SetVelocity (velocity : Vector3) {
	grounded = false;
	movement.velocity = velocity;
	movement.frameVelocity = Vector3.zero;
	if ( canSendMessages ) 
	{
		SendMessage("OnExternalVelocity");
	}
}


var m_FootstepSounds : AudioClip[] ;    // an array of footstep sounds that will be randomly selected from.
private var m_StepCycle : float = 0;
private var m_NextStep : float = m_StepCycle/2f;

function ProgressStepCycle()
{
	var stepLong : float = GetComponent.<Rigidbody>().velocity.magnitude;
	if(GetComponent.<Rigidbody>().velocity.sqrMagnitude > 0)
    	m_StepCycle += stepLong * Time.fixedDeltaTime;         

    if (!(m_StepCycle > m_NextStep))
        return;

      //añado incremento a ojo de buen cubero para regular la velocidad dela progresion de los pasos
    m_NextStep += (100 + 50 - stepLong) * Time.fixedDeltaTime;
    PlayFootStepAudio();
}

function PlayFootStepAudio()
{
	var m_AudioSource : AudioSource;
	m_AudioSource = GetComponent.<AudioSource>();
    if (!grounded)
    {
        return;
    }
    // pick & play a random footstep sound from the array,
    // excluding sound at index 0
    var n : int = Random.Range(1, m_FootstepSounds.Length);
    m_AudioSource.clip = m_FootstepSounds[n];
    m_AudioSource.PlayOneShot(m_AudioSource.clip);
    // move picked sound to index 0 so it's not picked next time
    m_FootstepSounds[n] = m_FootstepSounds[0];
    m_FootstepSounds[0] = m_AudioSource.clip;
}