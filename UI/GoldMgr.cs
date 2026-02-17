using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoldMgr : MonoBehaviour
{
    public Text GoldText;
    private int goldCap = 999;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Gold > goldCap)
        {
            GameManager.instance.Gold = goldCap;
        }

        GoldText.text = GameManager.instance.Gold + "/" + goldCap;
    }

    /*
    public class ClassA : MonoBehaviour
    {
        public delegate void ChangeEvent(int numberOfApples); //I do declare!
        public static event ChangeEvent changeEvent;  // create an event variable 
        public int appleCount = 0;
        // trigger some change and call the event for all subscribers
        public void MoreApples()
        {
            appleCount++;
            if (changeEvent != null) // checking if anyone is on the other line.
                changeEvent(appleCount);
        }

    }


    // Then you subscribe to the even from any class. and if the event is triggered (changed) then your other class will be notified.

    public class ClassB : MonoBehaviour
    {
        void OnEnable()
        {
            ClassA.changeEvent += ApplesChanged; // subscribing to the event. 
        }

        void ApplesChanged(int newAppleCount)
        {
            Debug.Log("newAppleCount now = " + newAppleCount);  // This will trigger anytime you call MoreApples() on ClassA
        }
    }
    */
}

