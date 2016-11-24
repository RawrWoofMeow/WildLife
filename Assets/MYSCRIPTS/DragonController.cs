using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DragonController : NetworkBehaviour 
{
	[System.Serializable]
	public class MoveSettings
	{
		public float forwardVel = 12;			//Forward Velocity (speed)
		public float rotateVel = 100;			//Rotation Velocity (speed)
		public float flyVel = 25;				//Ascend/Descend Velocity (speed)
		public float distToGrounded = 0.1f;		//Distance to the ground
		public LayerMask ground;				//What classifies as 'ground'
	}

	[System.Serializable]
	public class PhysSettings
	{
		public float downAccel;		//Downward accelleration
	}	

	[System.Serializable]
	public class InputSettings
	{
		public float inputDelay = 0.1f;				//Delay before action
		public string FORWARD_AXIS = "Vertical";	//Forward input
		public string TURN_AXIS = "Horizontal";		//Turn input
		public string JUMP_AXIS = "Jump";			//Ascend input
		public string LAND_AXIS = "Left Shift";		//Descend input
	}
	//Animator rename
	public Animator anim;

	//Camera
	public Camera cam;

	//Serialised settings
	public MoveSettings moveSetting = new MoveSettings();
	public PhysSettings physSetting = new PhysSettings();
	public InputSettings inputSetting = new InputSettings();

	//External scripts
	private DragonControllerFly fCont;
	private DragonControllerGrounded gCont;


	//public int TakeOffHash = Animator.StringToHash ("TakeOff");		//Animator trigger 'Fly'


	public Vector3 velocity = Vector3.zero;						//Velocity
	public Quaternion targetRotation;							//Rotation
	public Rigidbody rBody;										//Rigid body
	public float forwardInput, turnInput, flyInput, landInput;	//Inputs


	bool Grounded()		//Raycast to find out and set if grounded or not
	{
		return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
	}


	void Start()		//Initialization
	{

        if (!isLocalPlayer)        //Local player and cam instancing for network settings
        {
            cam.enabled = false;
            return;
        }
		
		rBody = GetComponent<Rigidbody>();		//Get rigidbody
		anim = GetComponent<Animator>();		//get animator

        fCont = GetComponent<DragonControllerFly>();			//Get flight script
        gCont = GetComponent<DragonControllerGrounded>();       //Get ground script
		
		forwardInput = turnInput = flyInput = landInput = 0;	//Set input default??
	}


	void GetInput()		//Get inputs
	{
		forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);	//interpolated
		turnInput = Input.GetAxis(inputSetting.TURN_AXIS);			//interpolated
		flyInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);		//non-interpolated
		landInput = Input.GetAxis(inputSetting.LAND_AXIS);			//interpolated
	}	


	void Update()		//updates as fast as can render
	{
		GetInput();		//Check for inputs

		if (Grounded() == true)		//If grounded, enable ground script. Enables Gravity
		{
			anim.SetBool("isGrounded", true);
			fCont.enabled = false;
			gCont.enabled = true;
			rBody.useGravity = true;
		}

		else						//otherwise, enable flying script. Disables Gravity
		{
			anim.SetBool("isGrounded", false);
			fCont.enabled = true;
			gCont.enabled = false;
			rBody.useGravity = false;
		}
	}


	void FixedUpdate()		//updates at fixed interval. good for physics stuff.
	{
		Grounded();
		rBody.velocity = transform.TransformDirection(velocity);		//update rigidbody's transforms?
	}
}