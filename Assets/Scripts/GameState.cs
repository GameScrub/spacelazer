using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private int totalScore;
    private int totalHealth;

    void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        int gameStatusCount = FindObjectsOfType<GameState>().Length;

        if (gameStatusCount > 1)
        {
            // Destroy self if exists
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int score)
    {
        totalScore += score;
    }

    public void SetCurrentHealth(int health)
    {
        totalHealth = health;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public int GetPlayerHealth()
    {
        return totalHealth;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
