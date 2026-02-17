using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuySomething : MonoBehaviour
{
    //your button
    public Button b;
    Slider slider;

    public enum Type
    {
        Skill,
        Unit
    }

    public Type Sale;

    //control the value to pass to event as you need
    public int Amt;
    public float coolTime;
    string unitObj = "MyHq";
    string skillObj = "Player";
    public string Message;
    bool isWait;
    float secCheck;

    void Start()
    {
        slider = transform.GetChild(1).GetComponent<Slider>();
        //register new event to onclick with the variables that control your args
        b.onClick.AddListener(() => BuySome(Amt));
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = txt.text = Amt.ToString();
    }

    private void Update()
    {
        if (isWait) { 
            secCheck += Time.deltaTime;
            if(slider != null) 
                slider.value = secCheck / coolTime;
            if(secCheck >= coolTime) { 
                isWait = false;
                secCheck = 0;
            }
        }

    }

    public void BuySome(int Amt)
    {
        if (GameManager.instance.CurrentMoney >= Amt && !isWait)
        {
            isWait = true;
            GameManager.instance.CurrentMoney -= Amt;
            SoundManager.instance.PlaySE(SoundManager.instance.click);
            if(Sale == Type.Unit)
            {
                var spawn = GameObject.Find(unitObj);
                var i = int.Parse(this.gameObject.name.Substring(this.gameObject.name.Length - 1));
                spawn.GetComponent<UnitSpawn>().Spawner(i);
            }
            else
            {
                var spawn = GameObject.Find(skillObj);
                switch (Message)
                {
                    case "Mercy":
                        spawn.GetComponent<SkillMgr>().Mercy();
                        break;
                    case "LightRain":
                        spawn.GetComponent<SkillMgr>().LightRain();
                        break;
                    case "LightExp":
                        spawn.GetComponent<SkillMgr>().LightExp();
                        break;
                    default:
                        Debug.Log("There is no skill");
                        break;
                }

            }
            //spawn.SendMessage(Message, obj); // 인스펙터에서 입력한 메세지 전송

        }
    }
}