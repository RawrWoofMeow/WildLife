using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CharController : NetworkBehaviour {

	[System.Serializable]
	public class MoveSettings
	{
		public float forwardVel = 12;
		public float rotateVel = 100;
		public float jumpVel = 25;
		public float distToGrounded = 0.1f;
		public LayerMask ground;
	}

	[System.Serializable]
	public class PhysSettings
	{
		public float downAccel = 0.75f;
	}	

	[System.Serializable]
	public class InputSettings
	{
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Vertical";
		public string TURN_AXIS = "Horizontal";
		public string JUMP_AXIS = "Jump";
	}

	public Camera cam;
	public MoveSettings moveSetting = new MoveSettings();
	public PhysSettings physSetting = new PhysSettings();
	public InputSettings inputSetting = new InputSettings();

	private Animator anim;
	int jumpHash = Animator.StringToHash ("Jump");
	Vector3 velocity = Vector3.zero;
	Quaternion targetRotation;
	Rigidbody rBody;
	float forwardInput, turnInput, jumpInput;

	public Quaternion TargetRotation
	{
		get { return targetRotation; }
	}

	//raycast to find out and set if grounded or not
	bool Grounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
	}

	void Start()
	{
		if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
		targetRotation = transform.rotation;
		if (GetComponent<Rigidbody>())
			rBody = GetComponent<Rigidbody>();
		else
			Debug.LogError("The character needs a rigid body");

		anim = GetComponent<Animator>();
		forwardInput = turnInput = jumpInput = 0;
	}

	void GetInput()
	{
		forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS); //interpolated
		turnInput = Input.GetAxis(inputSetting.TURN_AXIS);  //interpolated
		jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS); //non-interpolated
	}

	void Update()
	{
		GetInput();
		Turn();
	}

	void FixedUpdate()
	{
		Run();
		Jump();
		rBody.velocity = transform.TransformDirection(velocity);
	}

	void Run()
	{
		if (Mathf.Abs(forwardInput) > inputSetting.inputDelay)
		{
			//move
			velocity.z = moveSetting.forwardVel * forwardInput;
		}
		else
			//zero velocity
			velocity.z = 0;
		
        if (velocity.z > 0f)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
	}

	void Turn()
	{
		if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
		{
			targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
		}
		transform.rotation = targetRotation;
	}

	void Jump()
	{
        ////is grounded pressing jump button
        //if (jumpInput > 0 && Grounded())
        //{
        //	//jump
        //	anim.SetTrigger(jumpHash);
        //	velocity.y = moveSetting.jumpVel;
        //}
        ////grounded not pressing jump button
        //else if (jumpInput == 0 && Grounded())
        //{
        //	//zero out our velocity.y
        //	velocity.y = 0;
        //}
        ////not grounded
        //else
        //{
        //	//decrease velocity.y
        //	velocity.y -= physSetting.downAccel;
        //}
        if (Input.GetKeyDown(KeyCode.Space) && Grounded())
        {
            anim.SetTrigger("Jump");
            velocity.y = moveSetting.jumpVel;
        }
        else if (!Input.GetKeyDown(KeyCode.Space) && Grounded())
        {
            //zero out our velocity.y
            velocity.y = 0;
        }
        else
        {
            velocity.y -= physSetting.downAccel;
        }
	}
}