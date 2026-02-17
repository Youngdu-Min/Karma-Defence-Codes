using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelSelectMenu menu;
    public Sprite btLockSprite;
    private Sprite btSprite;
    private Text levelText;
    private int level = 0;
    private Button button;
    private Image image;
    private Image childImage;
    private LevelStar levelStar;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        btSprite = image.sprite;
        childImage = transform.GetChild(0).GetComponent<Image>();
        levelText = GetComponentInChildren<Text>();
        levelStar = GetComponentInChildren<LevelStar>();
    }

    public void Setup(int level, int star, bool isUnlock)
    {
        this.level = level;
        levelText.text = "스테이지" + level.ToString();

        if (isUnlock)
        {
            image.sprite = btSprite;
            childImage.color = new Color(1, 1, 1, 0);
            button.enabled = true;
            levelText.gameObject.SetActive(true);
            levelStar.SetStarSprite(star);
            levelStar.gameObject.SetActive(true);
        }
        else
        {
            image.sprite = btLockSprite;
            childImage.color = new Color(1, 1, 1, 1);
            button.enabled = false;
            levelText.gameObject.SetActive(false);
            levelStar.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        PlayerPrefs.SetInt("level", level);
        GameManager.instance.quest = GameManager.instance.stQuest[PlayerPrefs.GetInt("level") - 1];
        GameManager.instance.Result();
        // menu.StartLevel(level);
    }

}
