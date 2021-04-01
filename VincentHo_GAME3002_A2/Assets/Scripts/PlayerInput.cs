/*
 ------------ Developer's Notes --------------
 This script is attached to the flippers, when pressing the corresponding key, it rotates the flippers attached to that key
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // physics variables
    [SerializeField]
    private float m_fSpringConstant = 0.0f; // spring constant for the flipper (set to 2500 in the inspector)
    [SerializeField]
    private float m_fOriginalPos = 0f; // angle at rest
    [SerializeField]
    private float m_fPressedPos = 0f; // once pressed, the flipper will rotate to this set value (left paddle set to 60, and right paddle set to -60)
    [SerializeField]
    private float m_fFlipperSpringDamp = 0f; // damper setting (set to 50 in the inspector)

    [SerializeField]
    private KeyCode m_fFlipperInput; // keycode set in the inspector, (Z for left flipper, ? key for right flipper)

    [SerializeField]
    private AudioClip flipperSound; 

    // hinge joint variables
    private HingeJoint m_hingeJoint = null;
    private JointSpring m_jointSpring;

    private void Start()
    {
        // initialization of values
        m_hingeJoint = GetComponent<HingeJoint>();
        m_hingeJoint.useSpring = true;

        m_jointSpring = new JointSpring();
        m_jointSpring.spring = m_fSpringConstant;
        m_jointSpring.damper = m_fFlipperSpringDamp;

        m_hingeJoint.spring = m_jointSpring;

    }

    private void Update()
    {
        // flipper hit and release events
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
        // when the corresponding button is pressed, the flipper will rotate to the set value
        m_jointSpring.targetPosition = m_fPressedPos; 
        m_hingeJoint.spring = m_jointSpring;
        AudioSource.PlayClipAtPoint(flipperSound, gameObject.transform.position); // an audio clip will play once the flipper is paressed
    }

    private void OnFlipperReleasedInternal()
    {
        // upon release, the flipper will return to the original resting position
        m_jointSpring.targetPosition = m_fOriginalPos;
        m_hingeJoint.spring = m_jointSpring;
    }
}
