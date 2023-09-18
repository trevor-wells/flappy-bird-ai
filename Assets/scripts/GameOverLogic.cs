using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField] private LogicScript logic;
    public GameObject onesPlaceObject;
    public GameObject tensPlaceObject;
    public GameObject hundredsPlaceObject;
    public GameObject medalObject;
    public GameObject newTag;

    public Sprite[] numberSprites;
    public Sprite[] medalSprites;

    private int score;

    private void Start()
    {
        if (gameObject.CompareTag("HighScore"))
        {
            score = PlayerPrefs.GetInt("hs");
            if (logic.isNewHighScore)
                newTag.SetActive(true);
        }
        else
        {
            SetMedal();
            score = logic.playerScore;
        }
        int onesPlace = score % 10;
        int tensPlace = (score / 10) % 10;
        int hundredsPlace = (score / 100) % 10;
        if (score >= 1)
            onesPlaceObject.GetComponent<Image>().sprite = numberSprites[onesPlace];
        if (score >= 10)
            tensPlaceObject.SetActive(true);
            tensPlaceObject.GetComponent<Image>().sprite = numberSprites[tensPlace];
        if (score >= 100)
            hundredsPlaceObject.SetActive(true);
            hundredsPlaceObject.GetComponent<Image>().sprite = numberSprites[hundredsPlace];
    }

    private void SetMedal()
    {
        Image medal = medalObject.GetComponent<Image>();
        if (logic.playerScore >= 10)
        {
            medalObject.SetActive(true);
            medal.sprite = medalSprites[0];
            if (logic.playerScore >= 20)
            {
                medal.sprite = medalSprites[1];
                if (logic.playerScore >= 30)
                {
                    medal.sprite = medalSprites[2];
                    if (logic.playerScore >= 40)
                        medal.sprite = medalSprites[3];
                }
            }
        }
    }
}
