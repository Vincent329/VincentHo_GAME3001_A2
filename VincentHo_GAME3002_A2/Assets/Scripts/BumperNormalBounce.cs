using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperNormalBounce : MonoBehaviour
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
        Rigidbody ballHit = other.gameObject.GetComponent<Rigidbody>();

        Vector3 forceDirection = -other.contacts[0].normal;//(collision.transform.position - gameObject.transform.position).normalized;

        foreach (var point in other.contacts)
        {
            Debug.DrawLine(other.transform.position, other.transform.position - ball.GetOldVelocity().normalized, Color.red, 3f);
            Debug.DrawLine(point.point, point.point - point.normal, Color.blue, 3f);
        }

        ballHit.AddForce(forceDirection * bumpForce, ForceMode.VelocityChange);
        StartCoroutine(Extend());

    }

    IEnumerator Extend()
    {
        Vector3 previousScale = transform.localScale;
        gameObject.transform.localScale += new Vector3(0.1f, 0.0f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = previousScale;
    }
}
