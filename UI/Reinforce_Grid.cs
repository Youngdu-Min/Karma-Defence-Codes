using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reinforce_Grid : MonoBehaviour
{
    float amt;
    public int[] upPer;
    public enum Type
    {
        Unit,
        Skill1,
        Skill2,
        Skill3,

        S_Hp,
        S_Damage,
        S_Skill1,
        S_Skill2,
        S_Skill3
    }

    public enum Type_UI
    {
        Upgrade,
        Reinforce
    }

    public Type type;
    public Type_UI type_UI;
    public GameObject upObj;
    public int[] price;
    Text txt;
    [HideInInspector]
    public Text starTxt;
    Button b;
    [HideInInspector]
    public int currTier;

    void Start()
    {
        b = GetComponent<Button>(); 
        if(type_UI == Type_UI.Reinforce)
            b.onClick.AddListener(() => Reinforce());
        else 
        {
            b.onClick.AddListener(() => Upgrade());
            starTxt = GameObject.Find("StarText").GetComponent<Text>();
            currTier = PlayerPrefs.GetInt(this.gameObject.name);
        }
        txt = transform.GetChild(0).GetComponent<Text>();
        Refresh();

        if (type == Type.Skill1)
            amt = upObj.GetComponent<SkillMgr>().Time_shield;
        else if (type == Type.Skill2)
        {
            amt = upObj.GetComponent<SkillDam>().DamageAmt;

        }
        else if (type == Type.Skill3)
        {
            amt = upObj.transform.GetChild(0).GetComponent<SkillDam>().DamageAmt;
        }
    }

    void Update()
    {

    }

    public void Reinforce()
    {
        if (price[currTier] <= GameManager.instance.Gold)
        {
            SoundManager.instance.PlaySE(SoundManager.instance.reinforce);

            GameManager.instance.Gold -= price[currTier];
            if (type == Type.Unit)
                upObj.GetComponent<Unit>().Upgrade(upPer[currTier]);
            

            // amt += amt * upPer[currTier] / 100; // 퍼센트 방식
            amt += upPer[currTier]; // 값 증가 방식

            currTier++;
            GameManager.instance.reinforceCt++;

            if (type == Type.Skill1)
                upObj.GetComponent<SkillMgr>().Time_shield = amt;
            else if (type == Type.Skill2)
            {
                upObj.GetComponent<SkillDam>().DamageAmt = (int)amt;

            }
            else if (type == Type.Skill3)
            {
                upObj.transform.GetChild(0).GetComponent<SkillDam>().DamageAmt = (int)amt;
            }

            if (price.Length - 1 < currTier)
            {
                txt.text = "Max";
                b.onClick.RemoveAllListeners();
                return;
            }
            Refresh();
        }
    }

    public void Upgrade()
    {
        Debug.Log(currTier);
        if (price[currTier] <= int.Parse(starTxt.text))
        {
            starTxt.text = (int.Parse(starTxt.text) - price[currTier]).ToString();
            // GameManager.instance.RefreshStar();
            // amt += amt * upPer[currTier] / 100; // 퍼센트 방식
            amt += upPer[currTier]; // 값 증가 방식

            currTier++;
            
            if (type == Type.S_Hp)
                PlayerPrefs.SetFloat("unitHp", amt);
            else if (type == Type.S_Damage)
            {
                PlayerPrefs.SetFloat("unitDamage", amt);

            }
            else if (type == Type.S_Skill1)
            {
                PlayerPrefs.SetFloat("skill1", amt);
            }
            else if (type == Type.S_Skill2)
            {
                PlayerPrefs.SetFloat("skill2", amt);
            }
            else if (type == Type.S_Skill3)
            {
                PlayerPrefs.SetFloat("skill3", amt);
            }
            

            if (price.Length - 1 < currTier)
            {
                txt.text = "Max";
                b.onClick.RemoveAllListeners();
                return;
            }

            Refresh();
        }
    }


    public void Refresh()
    {
        txt.text = price[currTier].ToString();
        
    }

    public void Save()
    {
        PlayerPrefs.SetInt(this.gameObject.name, currTier);
    }
}
