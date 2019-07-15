using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI playerHealthText;

    private int m_playerHealth;
    private int m_playerScore;

    // Start is called before the first frame update
    void Start()
    {
        m_playerHealth = 3;
        m_playerScore = 0;

        scoreText.text = m_playerScore.ToString();
        playerHealthText.text = "x" + m_playerHealth.ToString();
    }

    public void UpdateScore()
    {
        m_playerScore += 5;
        scoreText.text = m_playerScore.ToString();
    }

    public void UpdateHealth()
    {
        m_playerHealth -= 1;
        playerHealthText.text = $"x{ m_playerHealth }";
    }

    public int GetPlayerHeath()
    {
        return m_playerHealth;
    }
}
