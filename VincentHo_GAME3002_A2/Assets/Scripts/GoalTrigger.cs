using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField]
    private int scoreValue;

    [SerializeField]
    private ScoreManager scoreManager;

    private void OnTriggerEnter(Collider other)
    {
        scoreManager.UpdateScore(scoreValue);
    }
}
