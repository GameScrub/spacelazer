using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private float delayInSeconds = 2f;

    private GameState gameState = null;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {
        FindObjectOfType<GameState>().ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
