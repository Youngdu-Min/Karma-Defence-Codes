using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExp : ProjectileDefault
{
    List<GameObject> currentCollisions = new List<GameObject>();

    protected override void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            currentCollisions.Add(coll.gameObject);
            
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            transform.GetChild(transform.childCount - 2).gameObject.SetActive(false);
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            for (int i = 0; i < currentCollisions.Count; i++)
            {
                currentCollisions[i].GetComponent<UnitHp>().Damaged(int.Parse(this.gameObject.name), "Magic");
                currentCollisions.Remove(currentCollisions[i]);
                Invoke("False", 1);
            }
        }
    }
}
