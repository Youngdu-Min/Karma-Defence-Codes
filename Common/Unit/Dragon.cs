using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : UnitHp
{
    protected override void Disable()
    {
        GameManager.instance.Win();
        SoundManager.instance.PlaySE(SoundManager.instance.bossDie);
        Time.timeScale = 0;
        GameManager.instance.Result();
        gameObject.SetActive(false);
    }
}
