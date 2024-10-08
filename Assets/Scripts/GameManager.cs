using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private PlayerController playerJump;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Animator playerAnimator;

    private int playerScore;
    private int scoreIncrement = 10;
    private float elapsedTime;
    public static bool isGameOn = false;

    private void Start()
    {
        elapsedTime = 0f;
        playerScore = 0;
        playerScoreText.text = playerScore.ToString();
        mainMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isGameOn)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1f)
            {
                AddScore();
                elapsedTime = 0f;
            }
        }
    }

    public void AddScore()
    {
        playerScore += scoreIncrement;
        playerScoreText.text = playerScore.ToString();
    }

    public void AddScore(int score)
    {
        playerScore += score;
        playerScoreText.text = playerScore.ToString();
    }

    public void ResetGame()
    {
        playerScore = 0;
        playerScoreText.text = playerScore.ToString();
        playerJump.ResetPlayerPosition();
        spawnManager.DeactivateAllObjects();
        mainMenuPanel.SetActive(true);
        Time.timeScale = 0;
        isGameOn = false;
        playerAnimator.SetTrigger("Idle");
    }
}
