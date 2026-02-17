using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    public float Speed;
    [HideInInspector]
    public bool Fighting;  // 근접 공격 상태
    [HideInInspector]
    public bool Shooting;  // 원거리 공격 상태
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Shooting && !Fighting)
        { transform.Translate(Vector3.forward * Time.deltaTime * Speed); }
    }
}
