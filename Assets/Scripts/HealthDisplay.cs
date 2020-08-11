using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Text health = null;
    private GameState gameState = null;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Text>();
        gameState = FindObjectOfType<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = gameState.GetPlayerHealth().ToString();
    }
}
