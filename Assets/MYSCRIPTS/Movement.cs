using UnityEngine;
using System.Collections;
/*
public class Movement : MonoBehaviour
{
    public float m_speed = 1f;
    public float m_TurnSpeed = 180f;

    public float jump;

    public float m_jumpSpeed = 5f;

    public Vector3 movement;

    public float m_animationTime = 1.5f;

    private Rigidbody m_rigidbody;
    private Animator m_animator;
    private CharacterController m_characterController;

    private bool m_attacking = false;
    private bool m_airbourne = false;
     
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>();
    }

    public override void OnStartLocalPlayer()
    {
        cameraGameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!m_attacking)
        {
            if (Input.GetButton("Jump") && m_characterController.isGrounded)
            {
                jump = 1;
                movement.y = transform.position.y * jump * m_jumpSpeed * Time.deltaTime;
                jump = 0;
                //m_animator.SetTrigger("Jump"); //for when the animation is done
            }
            else
            {
                float m_MovementInputValue = Input.GetAxis("Vertical");
                movement = transform.forward * m_MovementInputValue * m_speed * Time.deltaTime;


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
            }
        }


        if (Input.GetButtonDown("Fire1"))
        {
            m_attacking = true;
            m_animator.SetTrigger("Attacking");
            Invoke("Unattack", m_animationTime);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            m_attacking = true;
            m_animator.SetTrigger("Attacking_02");
            Invoke("Unattack", m_animationTime);
        }
    }

    void Unattack()
    {
        m_attacking = false;
    }
}
*/