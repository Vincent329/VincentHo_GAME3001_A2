/*
 --------------- Developer's Notes ----------------
 This script is attached to a volume placed along the ramp situated at the bottom of the ramp
 This is the same trigger volume that is used to re-open the plunger chamber door
 the purpose of this ramp is to accelerate the ball to the plunger's base to quicken the pace of the game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelBall : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        // The ball will be staying within the trigger volume once it falls past the flippers
        // as it stays within the trigger volume

        if (other.gameObject.layer == 3) // checks if it's the ball that comes in contact with the trigger volume (collision matrix is set so that Ball is at index 3)
        {
            Rigidbody refBody = other.gameObject.GetComponent<Rigidbody>();
            refBody.AddForce(Vector3.right * 5); // continually adds a rightward force to the ball
        }
    }
}
