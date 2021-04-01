/*
------- Developer's notes ---------
This script is attached to the bumper object prefabs in the scene

ACTIVE BUMPER
Black Bumpers utilize the reflected function to reflect off the contact normal
    - the score value for the black bumpers is set to 10
    - Bumper force is set to 1 in the Inspector so the magnitude of the velocity will stay the same after collision

PASSIVE BUMPER
Red Bumpers follow similar functionality, but the bumper force is diminished to almost zero to slow the ball
    - point value set to 2 in the inspector

BASH TOY
Script attached to a bash toy
    - Bumper Force set to 0, should just generally slow the ball like the passive bumper
    - point value set to 25 in the inspector
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperReflect : MonoBehaviour
{
    [SerializeField]
    private float bumpForce; // bumper force to be scaled on collision
    [SerializeField]
    private ScoreManager scoreManager; // reference to the score manager

    private Ball ball; // getting a reference to ball in order to access the ball's velocity before collision
   
    [SerializeField]
    private int scoreValue; // Different score values for each bumper
    [SerializeField]
    private AudioClip hitSound; // Sound Effect on collision

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }
    private void OnCollisionEnter(Collision other)
    {
        // upon collision, the collision normal is obtained from the ball
        // the rigid body of the ball is accessed in order to update the velocity after collision
        // the contact velocity is calculated by using the Vector3.Reflect function to reflect the velocity prior to collision along the collision normal
        // An audio clip will play upon contact, and the corresponding bumper will scale up and down for a visual effect
        // The score will then be updated based upon the attached bumper's score value

        Vector3 collisionNormal = other.contacts[0].normal;
        Rigidbody ballHit = other.gameObject.GetComponent<Rigidbody>();
        Vector3 oldVel = ball.GetOldVelocity();
        Vector3 contactVelocity = Vector3.Reflect(oldVel, collisionNormal); 
        ballHit.AddForce(contactVelocity * bumpForce, ForceMode.VelocityChange);
        AudioSource.PlayClipAtPoint(hitSound, gameObject.transform.position);
        StartCoroutine(Extend());
        scoreManager.UpdateScore(scoreValue);
    }

    IEnumerator Extend()
    {
        Vector3 previousScale = transform.localScale; // keep a reference of the old scale for reversion
        gameObject.transform.localScale += new Vector3(0.1f, 0.0f, 0.1f); // the bumper's scale will be increased slightly
        yield return new WaitForSeconds(0.1f);
        transform.localScale = previousScale; // after 1/10th of a second, the scale will revert back to its original position
    }

}
