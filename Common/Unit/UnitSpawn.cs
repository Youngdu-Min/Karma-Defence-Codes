using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitSpawn : MonoBehaviour
{
    public bool IsSpawn;
    public Transform SpawnPos;

    public GameObject[] Units;
    public int spawnCt;
    public float zAxis;
    public int chooseUnit;
    public int[] spawnTime;

    private GameObject unitObj;

    void Start()
    {
        PoolSpawn(Units, SpawnPos);
        

        if (IsSpawn == true)
        {
            StartCoroutine(AutoSpawner());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PoolSpawn(GameObject[] obj, Transform tr)
    {
        for (int i = 0; i < spawnCt; i++) // 인스펙터에 있는 적 생성
        {
            for (int j = 0; j < obj.Length; j++)
            {
                unitObj = Instantiate(obj[j], transform.position, Quaternion.identity);
                unitObj.transform.parent = tr.transform;
                unitObj.SetActive(false);

            }
        }
    }
    IEnumerator AutoSpawner()
    {
        Vector3 Pos = new Vector3(SpawnPos.position.x, SpawnPos.position.y, Random.Range(-zAxis, zAxis));
        int random;
        if(chooseUnit != 0)
        {
            random = chooseUnit - 1;
        }
        else { random = Random.Range(0, Units.Length); }
        
        for (int i = 0; i < Units.Length * spawnCt; i += Units.Length)
        {
            if (i < random) { i = random; }
            var spawnObj = SpawnPos.transform.GetChild(i).gameObject;
            if (!spawnObj.activeSelf)
            {
                spawnObj.SetActive(true);
                spawnObj.transform.position = Pos;
                yield return new WaitForSeconds(spawnTime[spawnTime.Length / Units.Length * random + PlayerPrefs.GetInt("level") - 1]);
                break;
            }
            
            else if (i + Units.Length >= Units.Length * spawnCt)
            {
                spawnCt += spawnCt;
                PoolSpawn(Units, SpawnPos);
            }
        }
        if (IsSpawn == true)
            StartCoroutine(AutoSpawner());
    }

    public void Spawner(int idx)
    {
        Vector3 Pos = new Vector3(SpawnPos.position.x, SpawnPos.position.y, Random.Range(-zAxis, zAxis));
        Debug.Log(idx);
        for (int i = 0; i < Units.Length * spawnCt; i += Units.Length)
        {
            if(i < idx) { i = idx; }
            var spawnObj = SpawnPos.transform.GetChild(i).gameObject;
            if (!spawnObj.activeSelf)
            {
                spawnObj.SetActive(true);
                spawnObj.transform.position = Pos;
                break;
            }

            else if (i + Units.Length >= Units.Length * spawnCt)
            {
                spawnCt += spawnCt;
                PoolSpawn(Units, SpawnPos);
            }
        }
        //Instantiate(obj, Pos, Quaternion.identity);
        GameManager.instance.spawnCt++;
        GameManager.instance.saveUnitCt++;
    }


}
