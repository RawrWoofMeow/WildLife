using UnityEngine;
using System.Collections;

public class DragonControllerFly : MonoBehaviour
{
	//External scripts
	private DragonController dCont;

	//Animator rename
	private Animator anim;

	float forwardInput, turnInput, flyInput, landInput;

    void Start()
    {
        //Get controller script
        dCont = GetComponent<DragonController>();

        dCont.targetRotation = transform.rotation;
    }

	void Update()
	{
		//Turn function
		Turn();
	}

	void FixedUpdate()
	{
		//fly function
		Fly();
	}

	//Turn function. For ground rotation
	void Turn()
	{
		//if something to do with math, the turn input and input delay
		if (Mathf.Abs(dCont.turnInput) > dCont.inputSetting.inputDelay)
		{
			//change target's rotation
			dCont.targetRotation *= Quaternion.AngleAxis(dCont.moveSetting.rotateVel * dCont.turnInput * Time.deltaTime, Vector3.up);
		}
		//update the transform rotation
		dCont.transform.rotation = dCont.targetRotation;
	}

    //Fly funtion. For in-air forwards movement
    void Fly()
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
        //Vertical movement
        //if something to do with math, ascention input and input delay
        if (Mathf.Abs(dCont.flyInput) > dCont.inputSetting.inputDelay)
        {
            //ascend
            
            dCont.velocity.y = dCont.moveSetting.flyVel * dCont.flyInput;
        }

        else if (Mathf.Abs(dCont.landInput) > dCont.inputSetting.inputDelay)
		{
            //desend
            transform.position = new Vector3(transform.position.x, transform.position.y - dCont.physSetting.downAccel * Time.deltaTime, transform.position.z);
            //dCont.velocity.y -= dCont.moveSetting.flyVel * dCont.flyInput;
		}
		//if not pressing ascention input
		else
			//zero velocity
			dCont.velocity.y = 0;
    }
}