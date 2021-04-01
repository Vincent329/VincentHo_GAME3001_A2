/*
------- Developer's notes ---------
This script is attached to the bumper object prefabs in the scene
Black Bumpers utilize the reflected function to reflect off the contact normal
    - the score value for the black bumpers is set to 10
    - Bumper force is set to 1 in the Inspector so the magnitude of the velocity will stay the same after collision

 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperReflect : MonoBehaviour
{
    [SerializeField]
    private float bumpForce;

    [SerializeField]
    private ScoreManager scoreManager;
    private Ball ball; // getting a reference to ball in order to access the ball's velocity before collision

    [SerializeField]
    private int scoreValue; // Different score values for each bumper

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }
    private void OnCollisionEnter(Collision other)
    {
        Vector3 collisionNormal = other.contacts[0].normal;
        Rigidbody ballHit = other.gameObject.GetComponent<Rigidbody>();
        Vector3 oldVel = ball.GetOldVelocity();
        Vector3 contactVelocity = Vector3.Reflect(oldVel, collisionNormal); // use the old velocity instead of current velocity
        
        ballHit.AddForce(contactVelocity * bumpForce, ForceMode.VelocityChange);

        StartCoroutine(Extend());
        scoreManager.UpdateScore(scoreValue);
    }

    IEnumerator Extend()
    {
        Vector3 previousScale = transform.localScale;
        gameObject.transform.localScale += new Vector3(0.1f, 0.0f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = previousScale;
    }

}
