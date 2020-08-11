using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text score = null;
    private GameState gameState = null;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        gameState = FindObjectOfType<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = gameState.GetTotalScore().ToString();
    }
}
