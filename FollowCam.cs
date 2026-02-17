using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float Xclamp;
    public Vector3 Offset;
    public Transform PlayerTr;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(PlayerTr.position.x) < Mathf.Abs(Xclamp))
        transform.position = PlayerTr.position + Offset;
        
    }
}
