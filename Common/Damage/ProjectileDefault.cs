using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefault : ProjectileSet
{
    protected virtual void Start()
    {

        //Fire();
    }

    private void OnEnable()
    {
        SoundManager.instance.PlaySE(attackSound);
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(!isMove);
        Fire();
    }

    protected virtual void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            coll.GetComponent<UnitHp>().Damaged(int.Parse(this.gameObject.name), "Bow");
             
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            if (isMove == true)
            {
                //transform.parent = coll.transform;
                //transform.localPosition = Vector3.zero;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
                Invoke("False", 1);
            }
            else if(!coll.gameObject.activeSelf)
            {
                False();
            }

        }


    }

    protected virtual void Fire()
    {
        if (isMove == true)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        }
        else
        {
            Invoke("False", 2);
        }
        Debug.Log(this.gameObject.name);
    }

    protected void False()
    {
        this.gameObject.SetActive(false);
    }
}
