using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperNormalBounce : MonoBehaviour
{
    [SerializeField]
    private float bumpForce;

    private Ball ball;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        Rigidbody ballHit = collision.gameObject.GetComponent<Rigidbody>();

        Vector3 forceDirection = -collision.contacts[0].normal;//(collision.transform.position - gameObject.transform.position).normalized;

        foreach (var point in collision.contacts)
        {
            Debug.DrawLine(collision.transform.position, collision.transform.position - ball.GetOldVelocity().normalized, Color.red, 3f);
            Debug.DrawLine(point.point, point.point - point.normal, Color.blue, 3f);
        }

        ballHit.AddForce(forceDirection * bumpForce, ForceMode.VelocityChange);
        
    }
}
