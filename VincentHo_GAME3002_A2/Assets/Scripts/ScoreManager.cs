using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int score;

    [SerializeField]
    private TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GetComponent<TextMeshPro>();
        Assert.IsNotNull(scoreText, "No TMP applied");
        ScoreToText();
    }

    private void Update()
    {
    }

    public void UpdateScore(int scoreValue)
    {
        score += scoreValue;
        ScoreToText();
    }

    void ScoreToText()
    {
        scoreText.text = "Score: " + score;
    }
}
