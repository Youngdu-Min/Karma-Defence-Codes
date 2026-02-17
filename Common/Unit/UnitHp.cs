using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UnitHp : MonoBehaviour
{
    [ReadOnly(true)]public int Hp;
    [HideInInspector]
    public float initHp;


    //생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모가 될 Canvas 객체
    private Canvas uiCanvas;
    //생명 수치에 따라 fillAmount 속성을 변경할 Image
    private Image hpBarImage;
    GameObject hpBar;

    void Start()
    {
        SetHp();
        //생명 게이지의 생성 및 초기화
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        //overlayCanvas = GameObject.Find("UICanvas_Overlay").GetComponent<Canvas>();
        SetHpBar(uiCanvas);
    }
    private void OnEnable()
    {

        if (uiCanvas != null)
            RestartHpBar(uiCanvas);
    }

    public void Damaged(int damage, string str)
    {
        SoundManager.instance.PlaySE(SoundManager.instance.damaged);

        if (GameManager.instance.Invincible == false || this.gameObject.tag == "Enemy")
        {
            Hp -= damage;
            HitCheck(str);
        }

        hpBarImage.fillAmount = Hp / initHp;

        if (Hp <= 0)
        {
            hpBarImage.transform.parent.gameObject.SetActive(false);
            KillCheck(str);
            Disable();
        }
    }

    void HitCheck(string str)
    {
        if (this.gameObject.name == "Dragon" && str == "Skill")
            GameManager.instance.bossHitbySkillCt++;
        else if (this.gameObject.name == "MyHq_HitBox")
            GameManager.instance.saveHQCt = (int)(Hp / initHp) * 100;
    }

    protected virtual void Disable()
    {
        GameManager.instance.saveUnitCt--;
        Hp = (int)initHp;
        hpBar.SetActive(false);
        gameObject.SetActive(false);
    }

    protected virtual void KillCheck(string str)
    {
        return;
    }

    protected virtual void SetHp()
    {
        initHp = Hp;
    }

    void SetHpBar(Canvas canvas)
    {
        hpBar = Instantiate(hpBarPrefab, canvas.transform);

        HpFollow();
    }

    void RestartHpBar(Canvas canvas)
    {
        hpBar = hpBarPrefab;
        for (int i = 0; i < canvas.transform.childCount - 1; i++)
        {
            if (!canvas.transform.GetChild(i).gameObject.activeInHierarchy && canvas.transform.GetChild(i).gameObject.name.Substring(0, 1) == this.gameObject.tag.Substring(0, 1))
            {
                hpBar = canvas.transform.GetChild(i).gameObject;
                hpBarImage.fillAmount = 1;
                hpBar.SetActive(true);
                break;
            }
            else if (i + 1 >= canvas.transform.childCount - 1)
            {
                hpBar = Instantiate(hpBarPrefab, canvas.transform);

            }
        }
        HpFollow();
    }

    void HpFollow()
    {
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[0];

        var _hpBar = hpBar.GetComponent<HpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;

    }

}