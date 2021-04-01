/*
 ---------------- Developer's Notes -----------------
 This script is attached to the UI game object situated at the back of the pinball machine
 The functionality of this script is to monitor the values of the player's score and remaining pinballs/lives
 Score and Life are two different TMP components which are individually handled.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int m_iScore; // Score value
    [SerializeField]
    private int m_iLife; // Life/Remaining Pinballs value

    // Obtaining the TextMeshPro components for score and life respectively
    [SerializeField]
    private TextMeshPro scoreText;
    [SerializeField]
    private TextMeshPro lifeText;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing the values upon launch of the game
        m_iScore = 0;
        m_iLife = 3; // life value defaulting to 3
        Assert.IsNotNull(scoreText, "No Score TMP applied");
        Assert.IsNotNull(lifeText, "No Life TMP applied");
        ScoreToText(); // Display the text at the start
    }

    public void UpdateScore(int scoreValue)
    {
        // Function is called upon contact with selected bumpers or triggers
        // Updates the score passed in from corresponding script components
        // then updates the display
        m_iScore += scoreValue;
        ScoreToText();
    }

    public void UpdateLife()
    {
        // This Function is only called when the ball falls through the flippers and traverses the end of the ramp to the plunger
        // life value decrements and the display is updated
        // if the life counter reaches 0, then the game over screen is loaded and displayed
        m_iLife--;
        ScoreToText();
        if (m_iLife <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void ScoreToText()
    {
        // Updates UI display
        scoreText.text = "Score: " + m_iScore;
        lifeText.text = "Pinballs Remaining: " + m_iLife;
    }
}
