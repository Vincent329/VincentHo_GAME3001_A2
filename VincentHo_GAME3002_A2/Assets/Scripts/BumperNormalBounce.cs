/*
 --------------- Developer's Notes ------------------
 This particular bumper script is attached to the flat bumpers that lay just above the flippers
 Rather than use Vector3.Reflect, the ball's velocity goes along the bumper's normal vector, which pushes it away from the bumper
 point value is set to 10 in the inspector
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperNormalBounce : MonoBehaviour
{
    [SerializeField]
    private float bumpForce; // scaling factor of the ball's velocity after collision, set to 10 in the inspector

    [SerializeField]
    private ScoreManager scoreManager; // getting a reference to the score manager

    private Ball ball; // getting a reference to ball in order to access the ball's velocity before collision
    
    [SerializeField]
    private int scoreValue; // the score value of the bumper
    [SerializeField]
    private AudioClip hitSound; // Sound Effect on collision

    private void OnCollisionEnter(Collision other)
    {
        // upon entry of collision, we take the ball's rigidbody and the force direction, which is set as the normal pointing away from the ball/the normal of the bumper surface
        // Add a velocity change force along the force direction multiplied by the bump force scaling factor
        // like BumperReflect, a sound will play and a scaling visual effect will be applied
        // score is then increased 

        Rigidbody ballHit = other.gameObject.GetComponent<Rigidbody>();
        Vector3 forceDirection = -other.contacts[0].normal;
        ballHit.AddForce(forceDirection * bumpForce, ForceMode.VelocityChange);
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
        StartCoroutine(Extend());
        scoreManager.UpdateScore(scoreValue);
    }

    IEnumerator Extend()
    {
        // Same logic as bumper reflect, where we keep track of the previous scale, bump the scale on collision
        // after a small amount of thime, the scale will be reverted back to its original state
        Vector3 previousScale = transform.localScale;
        gameObject.transform.localScale += new Vector3(0.1f, 0.0f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = previousScale;
    }
}
