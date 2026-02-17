using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableOff : MonoBehaviour
{
    public float time;
    private void OnEnable()
    {
        StartCoroutine(OffTimer());
    }

    IEnumerator OffTimer()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}