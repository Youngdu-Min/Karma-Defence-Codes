using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQHP : UnitHp
{
    public int incHp;


    protected override void SetHp()
    {
        for (int i = 1; i < PlayerPrefs.GetInt("level"); i++)
        {
            Hp += incHp;
        }
        initHp = Hp;
        GameManager.instance.saveHQCt = (int)(Hp / initHp) * 100;
    }
    protected override void Disable()
    {
        GameManager.instance.Lose();
        Time.timeScale = 0;
        gameObject.SetActive(false);
    }
}
