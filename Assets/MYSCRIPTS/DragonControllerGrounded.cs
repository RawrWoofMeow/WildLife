using UnityEngine;
using System.Collections;

public class DragonControllerGrounded : MonoBehaviour 
{
	//External scripts
	private DragonController dCont;

	//Animator rename
	private Animator anim;

	//Animator trigger 'Fly'
	private int TakeOffHash = Animator.StringToHash ("TakeOff");

	float forwardInput, turnInput, flyInput, landInput;

    void Start()
    {
        //Get controller script
        dCont = GetComponent<DragonController>();
    }

	//Updates as fast as can render
	void Update()
	{
        //Turn function
        Run();
        Turn();
	}

	void FixedUpdate()
	{
		
		TakeOff();

		//Update rigidbody's transforms?
		dCont.rBody.velocity = transform.TransformDirection(dCont.velocity);
	}

	//Run function. For ground forward and backward movement
	void Run()
	{
		//If something to do with math, the forward input and the input delay
		if (Mathf.Abs(dCont.forwardInput) > dCont.inputSetting.inputDelay)
		{
			//move
			dCont.velocity.z = dCont.moveSetting.forwardVel * dCont.forwardInput;
		}
		//If not pressing forward input
		else
			//zero velocity
			dCont.velocity.z = 0;
		
        //If velocity is greater than 0
        if (dCont.velocity.z > 0f || dCont.velocity.z < 0f)
        {
            //set animator bool 'Moving' to true
            dCont.anim.SetBool("Moving", true);
        }
        
        //Otherwise
        else
        {
            //Set animator bool 'Moving' to false
            dCont.anim.SetBool("Moving", false);
        }
	}

	//Turn function. For ground rotation
	void Turn()
	{
		//If something to do with math, the turn input and input delay
		if (Mathf.Abs(dCont.turnInput) > dCont.inputSetting.inputDelay)
		{
			//Change target's rotation
			dCont.targetRotation *= Quaternion.AngleAxis(dCont.moveSetting.rotateVel * dCont.turnInput * Time.deltaTime, Vector3.up);
		}
		//Update the transform rotation
		dCont.transform.rotation = dCont.targetRotation;
	}

	//TakeOff function. Transition from ground to air. (more or less a jump that doesn't go back down)
	void TakeOff()
	{
		//Is grounded and pressing ascention input
		if (dCont.flyInput > 0)
		{
            //Activate animator trigger 'TakeOff'
            dCont.anim.SetTrigger(TakeOffHash);
			//Ascention velocity
			//dCont.rBody.useGravity = false;
			dCont.velocity.y = dCont.moveSetting.flyVel;
            //Change animator bool 'Grounded' to false
            dCont.anim.SetBool("isGrounded", false);
		}
		//Grounded and not pressing fly button
		else if (dCont.flyInput == 0)
		{
			//Zero out our velocity.y
			dCont.velocity.y = 0;
		}
		//Not grounded. THIS NEEDS THE EDITING AND SHIZZLE
		//else if (dCont.Grounded())
	//	{
		//Descention velocity
		//	dCont.velocity.y -= dCont.physSetting.downAccel;
	//	}
	}
}