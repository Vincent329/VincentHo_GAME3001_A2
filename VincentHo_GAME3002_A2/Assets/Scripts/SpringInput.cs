/*
 -------------------- Developer's Notes -----------------
 This script is attached to the plunger, more specifically, on the collision box of the plunger
 The collision matrix is updated so that the prefab that acts as the plunger would stop at the collider that this script is attached to
 upon input by holding the down arrow key, the plunger is pulled back, and the ball is positined 
 */

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
    private Rigidbody m_attachedRB; // use this to access the attached rigidbody's components (the plunger object)
    private Vector3 m_vForce; // represents the force of our plunger

    [SerializeField]
    private Transform m_tPullTransform; // An empty game object transform is used to mark the limit in which the plunger can be pulled back to

    // Start is called before the first frame update
    void Start()
    {
        m_fPullPosition = -0.5f; // provide an offset to the plunger 
        m_attachedRB.transform.localPosition = new Vector3(0.0f, 0.0f, m_fPullPosition); // updates position on startup
        m_attachedRB.isKinematic = true; // set it so the attached body doesn't move at the start
        m_vRestPosition = gameObject.transform.position; // rest position will be where the collision box is
    }

    private void Update()
    {
        // when the down arrow key is held, isKinematic is set to true so that physics won't be applied as the plunger is pulled back
        // position is pulled back in small increments
        // should the pull position value exceed that of the z value of m_tPullTransform.localPosition
        // the plunger will cease to pull back any further

        if (Input.GetKey(m_input))
        {
            m_attachedRB.isKinematic = true;
            m_fPullPosition -= 0.05f;
            if (m_fPullPosition <= m_tPullTransform.localPosition.z)
            {
                m_fPullPosition = m_tPullTransform.localPosition.z;
            }
            m_attachedRB.transform.localPosition = new Vector3(0.0f, 0.0f, m_fPullPosition);
        }

        // Should the player release the button, isKinematic is set to false, meaning that physics will take effect
        // the pull position is reset back to its default value
        if (Input.GetKeyUp(m_input))
        {
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
        // x is the displacement between the resting position of the spring, and how far the plunger is pulled back
        m_vForce = -m_fSpringConstant * (m_vRestPosition - m_attachedRB.transform.position); //(no need for dampening since I want the plunger to stop on collision
        m_attachedRB.AddForce(m_vForce, ForceMode.Acceleration);
    }
}
