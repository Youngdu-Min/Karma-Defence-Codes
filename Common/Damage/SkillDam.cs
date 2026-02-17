using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDam : MonoBehaviour
{
    public bool IsDestroy;
    public int DamageAmt;
    public float WaitTime;
    public float WaitDestroy;
    public int hitMax;
    private int hitCt;
    private bool waiting;
    private bool FirstAttack;
    BoxCollider coll;

    [System.Serializable]
    public enum skillType
    {
        LightRain,
        Explo
    }

    public skillType skill;
    // Start is called before the first frame update

    private void Awake()
    {
        if (skill == skillType.LightRain)
        {
            DamageAmt += (int)PlayerPrefs.GetFloat("skill2");
            coll = GetComponent<BoxCollider>();
            StartCoroutine(AttackDelay());
        }
        if (skill == skillType.Explo)
            DamageAmt += (int)PlayerPrefs.GetFloat("skill3");

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!FirstAttack)
            {
                other.GetComponent<UnitHp>().Damaged(DamageAmt, "Skill");
                FirstAttack = true;

            }
            else
                other.GetComponent<UnitHp>().Damaged(DamageAmt, "Null");

        }
        if (IsDestroy == true)
            gameObject.SetActive(false);
    }

    public void Upgrade(int per)
    {
        DamageAmt += DamageAmt * per / 100;
    }
    IEnumerator AttackDelay()
    {
        waiting = !waiting;
        coll.enabled = waiting;
        if (waiting)
        {
            hitCt++;
            yield return new WaitForSeconds(WaitTime);
        }

        if (hitCt >= hitMax)
        {
            yield return new WaitForSeconds(WaitDestroy);
            gameObject.SetActive(false);
        }
        else { StartCoroutine(AttackDelay()); }
    }

    private void OnEnable()
    {
        if (skill == skillType.LightRain)
        {
            waiting = false;
            if (coll.gameObject != null)
                coll.enabled = true;
            hitCt = 0;
            StartCoroutine(AttackDelay());

        }
    }

    private void OnDisable()
    {
        if (skill == skillType.LightRain)
        {
            SoundManager.instance.CancelLoop();
        }
        FirstAttack = false;
    }
}
