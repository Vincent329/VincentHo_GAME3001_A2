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

    [SerializeField]
    private AudioClip hitSound; // Sound Effect on collision

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody ballHit = other.gameObject.GetComponent<Rigidbody>();
        Vector3 forceDirection = -other.contacts[0].normal;
        ballHit.AddForce(forceDirection * bumpForce, ForceMode.VelocityChange);
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
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
