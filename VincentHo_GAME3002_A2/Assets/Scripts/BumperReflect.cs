/*
------- Developer's notes ---------
This script is attached to the bumper object prefabs in the scene
Black Bumpers utilize the reflected 
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
        
        //foreach (var point in other.contacts)
        //{
        //    Debug.DrawLine(other.transform.position, other.transform.position - oldVel.normalized , Color.red, 3f);
        //    Debug.DrawLine(point.point, point.point - point.normal, Color.blue, 3f);
        //    Debug.DrawLine(other.transform.position, other.transform.position + contactVelocity.normalized, Color.green, 3f);
        //}

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
