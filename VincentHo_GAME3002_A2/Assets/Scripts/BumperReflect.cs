using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperReflect : MonoBehaviour
{
    [SerializeField]
    private float bumpForce = 5.0f;
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        Vector3 collisionNormal = collision.contacts[0].normal;
        Debug.Log(collisionNormal);
        Rigidbody ballHit = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 forceDirection = (collision.transform.position - gameObject.transform.position).normalized;
        //Vector3 contactVelocity = Vector3.Reflect(ballHit.velocity, collisionNormal);
        
        foreach (var point in collision.contacts)
        {
            Debug.DrawLine(point.point, point.point - point.normal, Color.blue, 3f);
            Debug.DrawLine(collision.transform.position, collision.transform.position - forceDirection.normalized, Color.green, 3f);
            Debug.DrawLine(point.point, point.point - forceDirection.normalized, Color.red, 3f);
        }

        ballHit.AddForce(forceDirection * bumpForce, ForceMode.Impulse);
        //Debug.Log("Velocity: " + contactVelocity);
        
    }
}
