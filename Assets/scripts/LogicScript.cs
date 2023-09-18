using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public bool isPaused = false;
    public int playerScore;
    public GameObject onesPlaceBig;
    public GameObject tensPlaceBig;
    public GameObject hundredsPlaceBig;

    public GameObject gameOverScreen;
    public GameObject inGameUI;
    public GameObject pauseScreen;

    public SpriteRenderer background;
    public SpriteRenderer birdColor;
    public Sprite[] backgrounds;
    public int bg;
    public Sprite[] birdColors;
    public int bc;
    public Animator anim;
    public bool isNewHighScore = false;

    public Sprite[] sprites;

    private void OnEnable() {
        bc = PlayerPrefs.GetInt("bci");
        bg = PlayerPrefs.GetInt("bgi");
        birdColor.sprite = birdColors[bc];
        anim.SetInteger("state", bc);
        background.sprite = backgrounds[bg];
    }

    private void Update()
    {
        if (playerScore == 0)
        {
            onesPlaceBig.SetActive(false);
            tensPlaceBig.SetActive(false);
            hundredsPlaceBig.SetActive(false);
        }
    }
    public void AddScore()
    {
        playerScore++;
        int onesPlace = playerScore % 10;
        int tensPlace = (playerScore / 10) % 10;
        int hundredsPlace = (playerScore / 100) % 10;
  
        NumberLogic(playerScore);

        if (playerScore >= 1)
            onesPlaceBig.GetComponent<SpriteRenderer>().sprite = sprites[onesPlace];
        if (playerScore >= 10)
            tensPlaceBig.GetComponent<SpriteRenderer>().sprite = sprites[tensPlace];
        if (playerScore >= 100)
            hundredsPlaceBig.GetComponent<SpriteRenderer>().sprite = sprites[hundredsPlace];
    }

    public void NumberLogic(int playerScore)
    {
        if (playerScore == 1)
        {
            onesPlaceBig.SetActive(true);
        }
        if (playerScore == 10)
        {
            tensPlaceBig.SetActive(true);
            onesPlaceBig.transform.Translate(0.65f,0f,0f);
        }
        if (playerScore == 100)
        {
            hundredsPlaceBig.SetActive(true);
            onesPlaceBig.transform.Translate(0.65f, 0f, 0f);
            tensPlaceBig.transform.Translate(0.65f, 0f, 0f);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        onesPlaceBig.SetActive(false);
        tensPlaceBig.SetActive(false);
        hundredsPlaceBig.SetActive(false);
        gameOverScreen.SetActive(true);
        inGameUI.SetActive(false);
        if ((PlayerPrefs.HasKey("hs") && playerScore > PlayerPrefs.GetInt("hs")) || !PlayerPrefs.HasKey("hs"))
        {
            PlayerPrefs.SetInt("hs", playerScore);
            isNewHighScore = true;
            PlayerPrefs.Save();
        }
    }

    public void TogglePause()
    {
            if (!isPaused)
            {
                inGameUI.SetActive(false);
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }
            else
            {
                inGameUI.SetActive(true);
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("mainMenu");
        Time.timeScale = 1;
    }
}
