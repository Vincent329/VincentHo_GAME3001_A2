using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringInput : MonoBehaviour
{
    // input
    [SerializeField]
    private KeyCode m_input;

    // physics variables
    [SerializeField]
    private float m_fSpringConstant; // spring constant
    [SerializeField]
    private Vector3 m_vRestPosition; // resting position
    [SerializeField]
    private float m_fPullPosition; // increase this value to pull back the plunger
    [SerializeField]
    private Rigidbody m_attachedRB; // use this to access the attached rigidbody which gets the spring
    private Vector3 m_vForce; // represents the force of our plunger

    [SerializeField]
    private Transform m_tPullTransform;
    // Start is called before the first frame update
    void Start()
    {
        m_fPullPosition = -0.5f;
        m_attachedRB.transform.localPosition = new Vector3(0.0f, 0.1f, m_fPullPosition);
        m_attachedRB.isKinematic = true; // set it so the attached body doesn't move at the start
        m_vRestPosition = gameObject.transform.position; // rest position will be where the collision box is
    }

    private void Update()
    {
        if (Input.GetKey(m_input))
        {
            Debug.Log("KeyDown");
            m_attachedRB.isKinematic = true;
            m_fPullPosition -= 0.05f;
            if (m_fPullPosition <= m_tPullTransform.localPosition.z)
            {
                m_fPullPosition = m_tPullTransform.localPosition.z;
                Debug.Log(m_tPullTransform.localPosition.z);
            }
            m_attachedRB.transform.localPosition = new Vector3(0.0f, 0.1f, m_fPullPosition);
        }
        if (Input.GetKeyUp(m_input))
        {
            Debug.Log("KeyUp");
            m_attachedRB.isKinematic = false;
            m_fPullPosition = -0.5f; // reset the pull position upon release
        }
    }

    // use fixed update for physics calculations
    private void FixedUpdate()
    {
        UpdateSpringForce();
    }

    private void UpdateSpringForce()
    {
        // F = -kx 
        // F is the force applied
        // k is the spring constant
        // x is the position between the 
        m_vForce = -m_fSpringConstant * (m_vRestPosition - m_attachedRB.transform.position); //(no need for dampening since I want the plunger to stop on collision
        m_attachedRB.AddForce(m_vForce, ForceMode.Acceleration);
    }

    private void OnDrawGizmos()
    {
        
    }

    // event when the key is pressed
    void OnKeyPressed()
    {
    }

    void OnKeyReleased()
    {   
    }
}
