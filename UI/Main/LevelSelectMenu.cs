using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public int totalLevel = 0;
    public int unlockedLevel = 1;
    private LevelButton[] levelButtons;
    private int totalPage = 0; 
    private int page = 0; // 페이지
    private int pageItem = 9; // 페이지 당 스테이지 개수

    private int starLength = 3;

    private GameObject bfObj;
    private bool isFirst;
    private bool isLaunched;
    // public GameObject nextButton;
    // public GameObject backButton;

    private void OnEnable()
    {
        levelButtons = GetComponentsInChildren<LevelButton>();
    }

    private void Start()
    {
        for (int j = 1; j < totalLevel; j++)
        {
            int ct = 0;
            for (int i = 0; i < starLength; i++)
            {
                if (PlayerPrefs.GetInt(j + i.ToString()) == 1)
                {
                    ct++;
                    int star = GetStar(j, this.gameObject);
                    star = Mathf.Clamp(ct, 0, 3);
                    SetStar(j, star);
                }
            } 
            
        }
        Refresh();
    }
    /*
    public void StartLevel(int level)
    {
        for (int i = 0; i < starLength; i++)
        {

            // SceneManager.LoadScene(2);
            if (PlayerPrefs.GetInt(level + i.ToString()) == 1)
            {
                if (level == unlockedLevel)
                {
                    unlockedLevel += 1;
                }
                int star = GetStar(level, this.gameObject);
                star = Mathf.Clamp(star + 1, 0, 3);
                SetStar(level, star);
                break;
            }
        }

        Refresh();
    }
    */

    /*
    public void ClickNext()
    {
        page += 1;
        Refresh();
    }
    
    public void ClickBack()
    {
        page -= 1;
        Refresh();
    }
    */
    public void Refresh()
    {
        totalPage = totalLevel / pageItem;

        int idx = page * pageItem;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = idx + i + 1;

            if (level <= totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                int star = GetStar(level, levelButtons[i].gameObject);
                
                if(star > 0 && isFirst && !isLaunched) { unlockedLevel += 1; }
                levelButtons[i].Setup(level, star, level <= unlockedLevel);

            }
            else
                levelButtons[i].gameObject.SetActive(false);
        }
        isLaunched = true;
        // CheckButton();
    }
    /*
    private void CheckButton()
    {
        backButton.SetActive(page > 0);
        nextButton.SetActive(page < totalPage);
    }
    */

    private void SetStar(int level, int starAmt)
    {
        PlayerPrefs.SetInt(GetKey(level), starAmt);
        Debug.Log(GetKey(level));
    }

    private int GetStar(int level, GameObject obj)
    {
        if (!isLaunched)
        {
            if(bfObj != obj)
            {
                bfObj = obj;
                isFirst = true;
            }
            else { isFirst = false; }

        }
        return PlayerPrefs.GetInt(GetKey(level));
    }

    private string GetKey(int level)
    {
        return "Level " + level + "_Star";
    }
}
