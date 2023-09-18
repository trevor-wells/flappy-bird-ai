using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public SpriteRenderer background;
    public Image birdColor;
    public Sprite[] backgrounds;
    public int bg;
    public Sprite[] birdColors;
    public int bc;
    public Animator anim;

    private void OnEnable() {
        if (PlayerPrefs.HasKey("bci"))
        {
            bc = PlayerPrefs.GetInt("bci");
        }
        if (PlayerPrefs.HasKey("bgi"))
        {
            bg = PlayerPrefs.GetInt("bgi");
        }
    }

    private void Update()
    {
        birdColor.sprite = birdColors[bc];
        anim.SetInteger("state", bc);
        background.sprite = backgrounds[bg];
    }

    public void StartGame()
    {
        SceneManager.LoadScene("game");
    }

    public void ChangeBackground()
    {
        if (bg >= backgrounds.Length - 1)
            bg = 0;
        else
            bg++;
    }

    public void ChangeBirdColor()
    {
        if (bc >= birdColors.Length - 1)
            bc = 0;
        else
            bc++;
    }

    public void ResetPrefs()
    {
        bc = 0;
        bg = 0;
        PlayerPrefs.DeleteAll();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("bci", bc);
        PlayerPrefs.SetInt("bgi", bg);
        PlayerPrefs.Save();
    }
}
