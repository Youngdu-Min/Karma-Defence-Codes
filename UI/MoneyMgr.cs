using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMgr : MonoBehaviour
{
    public Text MoneyText;
    public Text UpgradeMoney;

    public int[] upgradeVal;
    public int[] MoneyCap;
    private int SnowLevel;

    public float IncDelay;
    public int IncAmount;


    void Start()
    {
        GameManager.instance.CurrentMoney = 0;
        MoneyText.text = "0 / " + MoneyCap[SnowLevel];
        UpgradeMoney.text = "UP\n" + upgradeVal[SnowLevel];
        StartCoroutine(MoneyInc());
    }

    void Update()
    {
        MoneyText.text = GameManager.instance.CurrentMoney + "/" + MoneyCap[SnowLevel];
    }




    protected IEnumerator MoneyInc()
    {
        if(GameManager.instance.CurrentMoney + IncAmount <= MoneyCap[SnowLevel])
        {
            GameManager.instance.CurrentMoney += IncAmount;
        }
        else { GameManager.instance.CurrentMoney = MoneyCap[SnowLevel]; }
        yield return new WaitForSeconds(IncDelay);
        StartCoroutine(MoneyInc());
    }

    public void UpgradeSnow()
    {
        if(GameManager.instance.CurrentMoney >= upgradeVal[SnowLevel])
        {
            GameManager.instance.CurrentMoney -= upgradeVal[SnowLevel];
            SnowLevel++;
            UpgradeMoney.text = "UP\n" + upgradeVal[SnowLevel];
        }
    }
}
