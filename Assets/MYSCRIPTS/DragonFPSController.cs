using UnityEngine;
//using UnityEngine.Networking;
using System.Collections;

public class DragonFPSController : MonoBehaviour 
{
	//Move Settings
	[System.Serializable]
	public class MoveSettings
	{
		public float speed = 10.0F;
	}
	//Physics Settings
	[System.Serializable]
	public class PhysSettings
	{

	}
	//Input Settings
	[System.Serializable]
	public class InputSettings
	{

	}

//	public Animator anim;
	public Camera cam;

	//Serialised settings
	public MoveSettings moveSetting = new MoveSettings();
	public PhysSettings physSetting = new PhysSettings();
	public InputSettings inputSetting = new InputSettings();

	public Rigidbody rBody;										//Rigid body
	public bool cursorLockState = true;
	//Initialization
	void Start()
	{	
//		anim = GetComponent<Animator>();		//get animator
		Cursor.lockState = CursorLockMode.Locked;
	}
	//updates as fast as can render
	void Update()
	{
		float translation = Input.GetAxis("Vertical") * moveSetting.speed;	//set forward/backward axis
		float straffe = Input.GetAxis("Horizontal") * moveSetting.speed;	//set left/right axis
		translation *= Time.deltaTime;					//Keeps movement smooth and in time with update
		straffe *= Time.deltaTime;						//Keeps movement smooth and in time with update

		transform.Translate(straffe, 0, translation);
//		cursorLock();
	}
	//updates at fixed interval. good for physics stuff.
	void FixedUpdate()
	{

	}

	void cursorLock()					//Unlock the mouse cursor
	{
		if (Input.GetKeyDown("escape") && cursorLockState == true)	//if cursor is locked, unlock
		{
			Cursor.lockState = CursorLockMode.None;
			cursorLockState = false;
		}

		else if (Input.GetKeyDown("escape") && cursorLockState == false)	//if cursor is unlocked, lock
		{
			Cursor.lockState = CursorLockMode.Locked;
			cursorLockState = true;
		}
	}
}