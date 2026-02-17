using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    public float PlayerClamp;
    public Transform rainTr;
    private float rainX = 10;
    private float rainY = 0;
    // Rigidbody rb;
    Vector3 playerDirection;
    float ScreenWidth;

    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        ScreenWidth = Screen.width;
        rainTr.localPosition = new Vector3(rainX, rainY, -transform.position.z);
    }


    void Update()
    {

        // float mH = Input.GetAxis("Horizontal");
        // rb.velocity = new Vector3(mH * MoveSpeed, 0, 0);
    
        int i = 0;
        //loop over every touch found
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                //move right
                playerDirection = Vector3.right;
                RunCharacter(1.0f);
            }
            else if (Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                //move left
                playerDirection = Vector3.left;
                RunCharacter(-1.0f);
            }
            else
            {
                playerDirection = Vector3.zero;
                RunCharacter(0);
            }
            ++i;
        }
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Clamp(pos.x, -PlayerClamp, PlayerClamp);
        this.transform.position = pos;
        // RunCharacter(0);
    }
    
    void FixedUpdate()
    {
#if UNITY_EDITOR
        var h = Input.GetAxis("Horizontal");
        if (h > 0)
            playerDirection = Vector3.right;
        else if (h < 0)
            playerDirection = Vector3.left;
        else
            playerDirection = Vector3.zero;
        RunCharacter(h);
#endif
    }

    private void RunCharacter(float horizontalInput)
    {
        Vector3 local = transform.localScale;
        if(horizontalInput > 0)
        {
            local.x = 1;
            rainTr.localPosition = new Vector3(1 * rainX, rainY, 0);
        }
        else if(horizontalInput < 0)
        {
            local.x = -1;
            rainTr.localPosition = new Vector3(-1 * rainX, rainY, 0);
        }
        transform.localScale = local;
        transform.Translate(playerDirection * MoveSpeed * Time.deltaTime);
        // rb.velocity = new Vector3(horizontalInput * MoveSpeed, 0, 0);
    }
    
        /*
        void Update()
        {
            int i = 0;
            float tH = Input.GetTouch(i).position.x;
            float mH = Input.GetAxis("Horizontal");

            if (tH < ScreenWidth / 2)
                rb.velocity = new Vector3(-1 * MoveSpeed, 0, 0);
            else if (tH > ScreenWidth / 2)
                rb.velocity = new Vector3(1 * MoveSpeed, 0, 0);
            // rb.velocity = new Vector3(mH * MoveSpeed, 0, 0);

            Vector3 pos = rb.position;
            Vector3 local = transform.localScale;
            pos.x = Mathf.Clamp(pos.x, -PlayerClamp, PlayerClamp);
            rb.position = pos;

            if (mH < 0 || Input.GetTouch(i).position.x < ScreenWidth / 2)
            {

                local.x = -1;
                rainTr.localPosition = new Vector3(-rainX, rainY, 0);
            }
            else if (mH > 0 || Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                local.x = 1;
                rainTr.localPosition = new Vector3(rainX, rainY, 0);
            }

            transform.localScale = local;

        }
       */
    }
