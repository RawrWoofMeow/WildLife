using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {

	public Transform target;
	public float lookSmooth = 0.09f;
	public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
	public float xTilt = 10;

	Vector3 destination = Vector3.zero;
	CharController charController;
	float rotateVel = 0;

	void Start()
	{
		SetCameraTarget(target);
	}

	void SetCameraTarget(Transform t)
	{
		target = t;

		if (target != null)
		{
			if (target.GetComponent<CharController>())
			{
				charController = target.GetComponent<CharController>();
			}
			else
				Debug.LogError("The camera's target needs a character controller.");
		}
		else
			Debug.LogError("Your camera needs a target.");
	}

	void LateUpdate()
	{
		//rotating
		LookAtTarget();
	}

	void LookAtTarget()
	{
		float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
	}
}