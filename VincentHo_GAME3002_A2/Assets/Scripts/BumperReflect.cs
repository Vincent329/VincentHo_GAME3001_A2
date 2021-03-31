using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperReflect : MonoBehaviour
{
    [SerializeField]
    private float bumpForce;
    private Ball ball;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit");
        Vector3 collisionNormal = other.contacts[0].normal;
        Debug.Log(collisionNormal);
        Rigidbody ballHit = other.gameObject.GetComponent<Rigidbody>();
        Vector3 oldVel = ball.GetOldVelocity();
        Vector3 contactVelocity = Vector3.Reflect(oldVel, collisionNormal); // use the old velocity instead of current velocity
        
        foreach (var point in other.contacts)
        {
            Debug.DrawLine(other.transform.position, other.transform.position - oldVel.normalized , Color.red, 3f);
            Debug.DrawLine(point.point, point.point - point.normal, Color.blue, 3f);
            Debug.DrawLine(other.transform.position, other.transform.position + contactVelocity.normalized, Color.green, 3f);
        }

        ballHit.AddForce(contactVelocity, ForceMode.VelocityChange);
        ballHit.velocity *= bumpForce; // add force along the normal 

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
