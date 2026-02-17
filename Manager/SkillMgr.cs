using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr : MonoBehaviour
{
    public GameObject[] SkillObj;

    public Transform ShieldPos;
    public Transform RainPos;
    public Transform ExpPos;
    public Transform RainTargetPos;

    public float Time_shield;
    public float ExpAxisX;

    public float SecAxisX;
    private IEnumerator coroutine;

    Vector3 expStartScale;
    int unitCt;

    private void Awake()
    {
        expStartScale = SkillObj[2].transform.localScale;
    }
    private void Start()
    {
        Time_shield += PlayerPrefs.GetFloat("skill1");
        unitCt = GameObject.Find("MyHq").GetComponent<UnitSpawn>().spawnCt * GameObject.Find("MyHq").GetComponent<UnitSpawn>().Units.Length;
        shieldPool();
        skillPool(1, RainPos);
        skillPool(2, ExpPos);

    }

    void shieldPool()
    {
        for (int j = 0; j < unitCt; j++)
        {
            var spawn = Instantiate(SkillObj[0], transform.position, Quaternion.identity);
            spawn.transform.parent = ShieldPos;
            spawn.SetActive(false);
        }
    }

    void skillPool(int i, Transform tr)
    {
        var spawn = Instantiate(SkillObj[i], transform.position, Quaternion.identity);
        spawn.transform.parent = tr;
        spawn.SetActive(false);
    }

    public void Mercy()
    {
        Vector3 reset = new Vector3(0, 0, 0);
        GameObject[] friendly = GameObject.FindGameObjectsWithTag("Friendly");
        for (int i = 0; i < friendly.Length; i++)
        {
            if (friendly[i].layer == 13)
            {
                var spawn = ShieldPos.transform.GetChild(1);                
                
                /*
                coroutine = shieldCt(spawn.gameObject);

                if (GameManager.instance.Invincible)
                {
                    StopCoroutine(coroutine);
                }
                */
                if(friendly[i].transform.GetChild(1).childCount == 0)
                {
                    spawn.transform.parent = friendly[i].transform.GetChild(1).transform;
                    spawn.transform.localPosition = reset;

                }
                else
                {
                    spawn = friendly[i].transform.GetChild(1).transform.GetChild(0);
                }

                spawn.gameObject.SetActive(true);
                //if(coroutine != null)
                //StopCoroutine(coroutine);
                // StartCoroutine(coroutine);
                StartCoroutine(shieldCt(spawn.gameObject));
            }
        }

    }

    IEnumerator shieldCt(GameObject obj)
    {
        SoundManager.instance.PlaySE(SoundManager.instance.skill1);
        GameManager.instance.Invincible = true;
        yield return new WaitForSeconds(Time_shield);
        GameManager.instance.Invincible = false;
        obj.SetActive(false);
    }


    public void LightRain()
    {
        var player = new Vector3(this.gameObject.transform.position.x, 0, 0);
        for (int i = 0; i < RainPos.transform.childCount; i++)
        {
            SoundManager.instance.PlayLoopSE(SoundManager.instance.skill2);
            var spawn = RainPos.transform.GetChild(i);
            if (!spawn.gameObject.activeSelf)
            {
                spawn.gameObject.SetActive(true);
                spawn.transform.position = player + RainTargetPos.localPosition;
                break;
            }
            else if(i + 1 == RainPos.transform.childCount)
            { 
                skillPool(1, RainPos);
                //LightRain();
            }
        }
        // Instantiate(SkillObj[1], player + RainTargetPos.localPosition, Quaternion.identity);
    }
    public void LightExp()
    {
   

        for (int idx = 0; idx < ExpPos.transform.childCount; idx++)
        {
            SoundManager.instance.PlaySE(SoundManager.instance.skill3);
            var ExpSkill = ExpPos.transform.GetChild(idx);
            if (!ExpSkill.gameObject.activeSelf)
            {
                ExpSkill.gameObject.SetActive(true);
                StartCoroutine(xScaleInc(ExpSkill.gameObject));
                break;
                
            }
            else if (idx + 1 == ExpPos.transform.childCount)
            {
                skillPool(2, ExpPos);
            }

        }
        // yield return new WaitForSeconds(0.1f);
    }

    IEnumerator xScaleInc(GameObject obj)
    {
        var spawnPos = new Vector3(this.gameObject.transform.position.x + RainPos.localPosition.x, ExpPos.position.y, ExpPos.position.z);
        var xScale = 0;
        int i;
        if (RainTargetPos.localPosition.x > 0) 
        { 
            i = 1;
            obj.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 0); // 보는 방향에 따른 이펙트 수정
        }
        else
        {
            i = -1;
            obj.transform.GetChild(1).rotation = Quaternion.Euler(0, 180, 0); // 보는 방향에 따른 이펙트 수정
        }

        obj.transform.position = spawnPos;
        obj.transform.GetChild(0).localPosition = new Vector3(0.5f * i, 0, 0); // 콜라이더를 보는 방향에 맞게 위치 선정

        while (xScale < ExpAxisX)
        {
            obj.transform.localScale = new Vector3(xScale, obj.transform.localScale.y, obj.transform.localScale.z);
            xScale++;
            yield return new WaitForSeconds(SecAxisX);
        }

        yield return new WaitForSeconds(0.1f);
        obj.transform.localScale = expStartScale;
        obj.gameObject.SetActive(false);
    }
}
