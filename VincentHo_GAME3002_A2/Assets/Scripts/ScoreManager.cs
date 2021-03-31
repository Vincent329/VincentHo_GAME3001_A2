using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int m_iScore;
    [SerializeField]
    private int m_iLife;

    [SerializeField]
    private TextMeshPro scoreText;
    [SerializeField]
    private TextMeshPro lifeText;

    // Start is called before the first frame update
    void Start()
    {
        m_iScore = 0;
        m_iLife = 3;
        Assert.IsNotNull(scoreText, "No Score TMP applied");
        Assert.IsNotNull(lifeText, "No Life TMP applied");
        ScoreToText();
    }

    private void Update()
    {
    }

    public void UpdateScore(int scoreValue)
    {
        m_iScore += scoreValue;
        ScoreToText();
    }

    public void UpdateLife()
    {
        m_iLife--;
        ScoreToText();
    }

    void ScoreToText()
    {
        scoreText.text = "Score: " + m_iScore;
        lifeText.text = "Pinballs Remaining: " + m_iLife;
    }
}
