using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float m_fSpringConstant = 0.0f;
    [SerializeField]
    private float m_fOriginalPos = 0f;
    [SerializeField]
    private float m_fPressedPos = 0f;
    [SerializeField]
    private float m_fFlipperSpringDamp = 0f;
    [SerializeField]
    private KeyCode m_fFlipperInput;

    private HingeJoint m_hingeJoint = null;
    private JointSpring m_jointSpring;
    private void Start()
    {
        m_hingeJoint = GetComponent<HingeJoint>();
        m_hingeJoint.useSpring = true;

        m_jointSpring = new JointSpring();
        m_jointSpring.spring = m_fSpringConstant;
        m_jointSpring.damper = m_fFlipperSpringDamp;

        m_hingeJoint.spring = m_jointSpring;

    }

    private void Update()
    {
        if (Input.GetKeyDown(m_fFlipperInput))
        {
            OnFlipperPressedInternal();
        }
        if (Input.GetKeyUp(m_fFlipperInput))
        {
            OnFlipperReleasedInternal();
        }
    }
    private void OnFlipperPressedInternal()
    {
        m_jointSpring.targetPosition = m_fPressedPos;
        // update the joint
        m_hingeJoint.spring = m_jointSpring;
    }

    private void OnFlipperReleasedInternal()
    {
        m_jointSpring.targetPosition = m_fOriginalPos;
        // update the joint
        m_hingeJoint.spring = m_jointSpring;
    }
}
