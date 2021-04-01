/*
 ------------- Developer's Notes ---------------
 This script is attached to the box collider inside the goal net
 Implementation is that once the ball makes contact with the trigger volume, 
 the player is granted 100 points as assigned in the inspector and plays a cheer sound
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField]
    private int scoreValue; // score value of the trigger volume (set to 100 in the inspector)

    [SerializeField]
    private ScoreManager scoreManager; // reference to the score manager

    [SerializeField]
    private AudioClip triggerSound; // sound effect to play upon contact.  Cheer sound passed into the variable

    private void OnTriggerEnter(Collider other)
    {
        // upon collision, score updates and the clip plays
        scoreManager.UpdateScore(scoreValue);
        AudioSource.PlayClipAtPoint(triggerSound, gameObject.transform.position);

    }
}
