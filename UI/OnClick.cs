using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour
{
    bool isTrue_speed;
    public GameObject GridLayout;
    public GameObject image;
    public GameObject cancelClick;
    private readonly int hashisEnter = Animator.StringToHash("isEnter");
    Animator animator;
    bool isTrue_reinforce;


    void Start()
    {

        try
        {
            animator = image.GetComponent<Animator>();
            animator.SetBool(hashisEnter, true);
            GridLayout.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        ClosePopUP();
    }

    public void ClickSound()
    {
        SoundManager.instance.PlaySE(SoundManager.instance.click);
    }

    public void ReinforceSound()
    {
        SoundManager.instance.PlaySE(SoundManager.instance.reinforce);
    }

    public void LSAnimtaion()
    {
        animator.SetBool(hashisEnter, false);
    }

    public void Faster()
    {
        Debug.Log("Clicked");
        isTrue_speed = !isTrue_speed;
        if (isTrue_speed) { Time.timeScale = GameManager.instance.fast; }
        else { Time.timeScale = 1; }
    }

    public void OpenPopUp(int i)
    {
        transform.GetChild(i).gameObject.SetActive(true);
        if (cancelClick != null)
            cancelClick.SetActive(true);
    }


    public void ClosePopUP()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "PopUp")
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (cancelClick != null)
            cancelClick.SetActive(false);
    }
    public void TryReinforce()
    {
        var name = EventSystem.current.currentSelectedGameObject.name;
        Text childTxt = GameObject.Find(name).transform.GetChild(0).transform.GetComponent<Text>();
        isTrue_reinforce = !isTrue_reinforce;
        if (isTrue_reinforce)
        {
            childTxt.text = "취소";
        }
        else { childTxt.text = "강화"; }
        GridLayout.SetActive(isTrue_reinforce);
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit() // 게임 종료
    {
        Application.Quit();
    }

}
