using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStar : MonoBehaviour
{
    public Sprite fullStar;
    // public Image image;
    
    public void SetStarSprite(int starAmt)
    {
        for (int i = 0; i < starAmt; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = fullStar;
        }
        /*
        switch (starAmt)
        {
            case 0:
                image.sprite = starSprite0;
                break;
            case 1:
                image.sprite = starSprite0;
                break;
            case 2:
                image.sprite = starSprite0;
                break;
            case 3:
                image.sprite = starSprite0;
                break;

        }
        */
    }
    void Start()
    {
        
    }
}
