using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject[] closeObj;


    public GameObject[] keepObj;
    public static SceneChanger instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            //First run, set the instance
            instance = this;
            for (int i = 0; i < keepObj.Length; i++)
            {
                DontDestroyOnLoad(keepObj[i]);
            }

        }
        else if (instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            foreach (GameObject obj in keepObj) //부속 오브젝트 삭제
                Destroy(obj);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Stage_Scene")
        {
            foreach (GameObject obj in closeObj)
                obj.SetActive(false);

        }
        
    }

}
