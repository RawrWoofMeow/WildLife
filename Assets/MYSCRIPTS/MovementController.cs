using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MovementController : NetworkBehaviour
{
    public float m_speed = 1f;
    public float m_TurnSpeed = 180f;
    public Camera cam;

    private Rigidbody m_rigidbody;
    private Animator m_animator;


    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }

        float m_MovementInputValue = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * m_MovementInputValue * m_speed * Time.deltaTime;

        if (movement.magnitude > 0f)
        {
            m_animator.SetBool("Moving", true);
        }
        else
        {
            m_animator.SetBool("Moving", false);
        }

        m_rigidbody.MovePosition(m_rigidbody.position + movement);

        float m_TurnInputValue = Input.GetAxis("Horizontal");
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_rigidbody.MoveRotation(m_rigidbody.rotation * turnRotation);


        if (Input.GetButtonDown("Fire1"))
        {
            m_animator.SetTrigger("Attacking");
        }
		else if (Input.GetButtonDown("Fire2"))
		{
			m_animator.SetTrigger("Attacking_02");
		}
    }
}
