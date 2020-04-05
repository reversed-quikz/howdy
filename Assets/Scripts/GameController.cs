using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    private int score;
    private bool gameOver;
    private bool restart;
    private bool gameWin;
    private bool gameWinoverride;

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        gameOver = false;
        restart = false;
        gameWin = false;
        gameWinoverride = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("main");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Q' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
        if(score >= 100)
        {
            WinGame();
        }
    }
    public void GameOver()
    {
        if (gameWin == false)
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
            gameWinoverride = true;
        }
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
    }
    public void WinGame()
    {
        if (gameWinoverride == false)
        {
            winText.text = "Game Created By Josh Wiggins";
            gameOver = true;
            gameWin = true;
        }
    }
}