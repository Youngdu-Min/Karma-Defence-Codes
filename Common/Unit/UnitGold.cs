using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGold : UnitHp
{
    public int[] incGold;
    // Start is called before the first frame update
    protected override void KillCheck(string str)
    {
        if (str == "Skill")
            GameManager.instance.killbySkillCt++;
        else if (str == "Sword")
            GameManager.instance.killbySwordCt++;
        else if (str == "Bow")
            GameManager.instance.killbyBowCt++;

    }
    protected override void Disable()
    {
        Hp = (int)initHp;
        // GameManager.instance.killCt++;
        GameManager.instance.killCt += 10;
        GameManager.instance.Gold += incGold[PlayerPrefs.GetInt("level") - 1];
        gameObject.SetActive(false);
    }
}
