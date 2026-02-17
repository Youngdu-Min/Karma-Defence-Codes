using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQHP_Enemy : UnitHp
{
    public int incHp;
    public Transform bossPos;

    protected override void SetHp()
    {
        for (int i = 1; i < PlayerPrefs.GetInt("level"); i++)
        {
            Hp += incHp;
        }
        initHp = Hp;
    }
    protected override void Disable()
    {
        //Instantiate(boss, bossPos);
        SoundManager.instance.PlaySE(SoundManager.instance.bossAppear);
        this.gameObject.transform.GetChild(1).parent = null;
        bossPos.GetChild(0).gameObject.SetActive(true);
        gameObject.SetActive(false);
        SoundManager.instance.ChangeClip(3);
    }
}
