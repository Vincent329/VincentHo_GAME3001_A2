/*
 ---------- Developer's notes ------------
 This script is attached to two different trigger boxes
 1) at the end of the plunger chamber
 2) situated along the ramp at the dead zone (triggering a loss of life condition)
 
 When the ball is launched and exits the plunger chamber, upon exiting the trigger the chamber door will close behind the ball, preventing re-entry
 When the ball falls into the pit, the ball will make contact with the trigger zone along the ramp
    - OnTriggerEnd is called once the ball reaches the end of the ramp before reaching the plunger. This will decrement the life counter and re-open the plunger chamber door
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safeguard : MonoBehaviour
{
    [SerializeField]
    private GameObject m_barrier; // reference to the door at the end of the plunger chamber
    [SerializeField]
    private Transform m_vLerpPos; // the position the door will move to upon event call.  Position is different for both trigger volumes.
    [SerializeField]
    private bool m_bTrigger; // trigger condition that will set off to commence linear interpolation

    [SerializeField]
    private ScoreManager scoreManager; // access the score manager to update life count

    private Vector3 newPos; // the m_vLerpPos as a Vector3 format

    // Start is called before the first frame update
    void Start()
    {
        // initializing values
        m_bTrigger = false;
        newPos = m_vLerpPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        PositionShift();
    }

    private void PositionShift()
    {
        // once the flag is triggered, the game object will linearly interpolate to the set position 
        if (m_bTrigger)
        {
            m_barrier.transform.position = Vector3.Lerp(m_barrier.transform.position, newPos, Time.deltaTime * 3); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Trigger Exit is the event call
        // once called we set the flag to commence the interpolation
        // if the collider is flagged as the losing condition, the life counter gets reduced by 1

        m_bTrigger = !m_bTrigger; 
        StartCoroutine(Reset());
        if (gameObject.tag == "Lose")
        {
            scoreManager.UpdateLife();
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.0f);
        m_bTrigger = !m_bTrigger; // after 1 second (when the door animation finishes), reset the flag for repeatable use
    }
}
