using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    private int i = 0;

    public GameObject[] hideObj;
    public GameObject tutoPopUpObj;

    // Start is called before the first frame update
    void Start()
    {
        i = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        i = SceneManager.GetActiveScene().buildIndex;
        this.GetComponent<OnClick>().ClosePopUP();
        if (level == 2)
        {
            if (PlayerPrefs.GetInt("Next") == 1)
            {
                GameManager.instance.Result();
                this.GetComponent<OnClick>().OpenPopUp(3);
                HideObj(0);
                PlayerPrefs.SetInt("Next", 0);
                Debug.Log("next");
            }
            else if (PlayerPrefs.GetInt("Level 1_Star") == 0)
            {
                tutoPopUpObj.SetActive(true);
                Time.timeScale = 0;
            }
            else if (PlayerPrefs.GetInt("level") == 5)
            {
                HideObj(1);
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        else if(level == 3)
        {
            LoadScene(GameManager.instance.lastScene);
            Debug.Log("load");
        }
        else
        {
            BackAll();

        }

        HideObj(2);
    }

    public void LoadSpecificScene(string scene) // 특정 씬 호출
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadScene(int s)
    {
        SceneManager.LoadScene(s, LoadSceneMode.Single);
    }



    public void GoNext() // 다음 씬으로 이동
    {
        GameManager.instance.lastScene = i;
        PlayerPrefs.SetInt("Next", 1);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        this.GetComponent<OnClick>().ClosePopUP();
        LoadScene(3);
    }

    public void GoMain()
    {
        LoadScene(1);
    }

    public void GoAgain()
    {
        GameManager.instance.lastScene = i;
        LoadScene(3);
    }


    public void HideObj(int i)
    {
        hideObj[i].SetActive(false);
    }

    public void BackAll()
    {
        for (int i = 0; i < hideObj.Length; i++)
        {
            hideObj[i].SetActive(true);
        }
    }
}
