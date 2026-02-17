using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quest;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    

    public static GameManager instance;

    public StageQuest[] stQuest;
    // public QuestInfo[] Quests;
    // private List<bool> IsComplete = new List<bool> { false, false, false };
    private QuestUI[] ui;
    [SerializeField]
    private QuestUI[] winUi, questInfoUi;
    public int fast;
    [HideInInspector]
    public bool killed, Invincible;
    [HideInInspector]
    public int CurrentMoney, Gold;
    [HideInInspector]
    public int starCt;
    [HideInInspector]
    public int stageStar;
    public Sprite starSprite;
    public Sprite nullStarSprite;
    [HideInInspector]
    public bool toggle;
    public GameObject winObj;
    public GameObject loseObj;
    public GameObject questPopUpObj;
    private GameObject stageInfo;
    public GameObject currStar;
    public Text currStage;

    [ReadOnly] public int spawnCt, killCt, killbySkillCt, killbySwordCt, killbyBowCt, bossHitbySkillCt, reinforceCt, saveHQCt, saveUnitCt;

    public Reinforce_Grid[] rgs;

    [System.Serializable]
    public class StageQuest
    {
        public QuestInfo[] Quests;
    }
    [HideInInspector]
    public StageQuest quest;
    bool isWin;
    [HideInInspector]
    public int lastScene;
    void Awake()
    {
        instance = this;


    }
    // Start is called before the first frame update
    private void OnLevelWasLoaded(int level)
    {
        // ToggleTime();
        if (level == 2)
        {
            ui = winUi;
            stageInfo = winObj;
        }
        else
        {
            // quest = stQuest[PlayerPrefs.GetInt("level") - 1];
            currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
            ui = questInfoUi;
            stageInfo = questPopUpObj;
        }

        quest = stQuest[PlayerPrefs.GetInt("level") - 1];
        starCt = PlayerPrefs.GetInt("saveStarCt");
        toggle = false;
        isWin = false;
    }

    void Start()
    {
        currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
        ui = questInfoUi;
        stageInfo = questPopUpObj;
    }

    public void Lose()
    {
        SoundManager.instance.PlaySE(SoundManager.instance.defeat);
        SoundManager.instance.StopClip();
        loseObj.SetActive(true);
    }

    public void Win()
    {
        isWin = true;
        SoundManager.instance.PlaySE(SoundManager.instance.win);
        SoundManager.instance.StopClip();
    }

    public void Result()
    {
        toggle = !toggle;
        stageInfo.SetActive(toggle);
        
        currStage.text = "스테이지 " + PlayerPrefs.GetInt("level");
        for (int i = 0; i < quest.Quests.Length; i++)
        {
            if (quest.Quests[i].Type == QuestInfo.questType.Clear)
            {
                ui[i].text_Quest.text = "스테이지 " + PlayerPrefs.GetInt("level") + " 클리어 하기";
                CheckOrRefresh(i, 1);
            }
            else if(quest.Quests[i].Type == QuestInfo.questType.Spawn)
            {
                ui[i].text_Quest.text = "천사 유닛 " + quest.Quests[i].QuestValue + "명 이상 소환하기";
                CheckOrRefresh(i, spawnCt);
            }
            else if (quest.Quests[i].Type == QuestInfo.questType.Kill)
            {
                ui[i].text_Quest.text = "악마 유닛 " + quest.Quests[i].QuestValue + "마리 처치하기";
                CheckOrRefresh(i, killCt);
            }
            else if (quest.Quests[i].Type == QuestInfo.questType.KillbySkill)
            {
                ui[i].text_Quest.text = "대 천사 스킬로 악마 유닛 " + quest.Quests[i].QuestValue + "마리 처치하기";
                CheckOrRefresh(i, killbySkillCt);
            }
            else if (quest.Quests[i].Type == QuestInfo.questType.KillbySword)
            {
                ui[i].text_Quest.text = "전사 유닛으로 악마 유닛 " + quest.Quests[i].QuestValue + "마리 처치하기";
                CheckOrRefresh(i, killbySwordCt);
            }
            else if (quest.Quests[i].Type == QuestInfo.questType.KillbyBow)
            {
                ui[i].text_Quest.text = "궁수 유닛으로 악마 유닛 " + quest.Quests[i].QuestValue + "마리 처치하기";
                CheckOrRefresh(i, killbyBowCt);
            }
            else if (quest.Quests[i].Type == QuestInfo.questType.BossHitbySkill)
            {
                ui[i].text_Quest.text = "보스 캐릭터에게 스킬 3번 " + quest.Quests[i].QuestValue + "사용하기";
                CheckOrRefresh(i, bossHitbySkillCt);
            }
            else if (quest.Quests[i].Type == QuestInfo.questType.Reinforce)
            {
                ui[i].text_Quest.text = "강화 " + quest.Quests[i].QuestValue + "회 시도하기";
                CheckOrRefresh(i, reinforceCt);
            }
            else if (quest.Quests[i].Type == QuestInfo.questType.SaveHP)
            {
                ui[i].text_Quest.text = "천사의 석상 체력 " + quest.Quests[i].QuestValue + "% 이상 유지하기";
                Debug.Log("석상" + saveHQCt);
                CheckOrRefresh(i, saveHQCt);
            }
            else if (quest.Quests[i].Type == QuestInfo.questType.SaveUnit)
            {
                ui[i].text_Quest.text = "천사 유닛 " + quest.Quests[i].QuestValue + "명 이상 살려둔 채 클리어하기";
                CheckOrRefresh(i, saveUnitCt);
            }
        }
        
    }
    
    void CheckOrRefresh(int i, int val)
    {
        if (SceneManager.GetActiveScene().name == "Main_Scene" || !isWin)
            RefreshStar(i);
        else
            CheckStar(i, val);

    }

    void CheckStar(int i, int val)
    {
        
        int lv = PlayerPrefs.GetInt("level");
        // Get boolean using PlayerPrefs
        var isDuplicate = PlayerPrefs.GetInt(lv + i.ToString()) == 1 ? true : false;
        if (!isDuplicate && val >= quest.Quests[i].QuestValue)
        {
            isDuplicate = true;
            PlayerPrefs.SetInt(lv + i.ToString(), isDuplicate ? 1 : 0);
            starCt++;
            Debug.Log(starCt);
            PlayerPrefs.SetInt("saveStarCt", starCt);
            ui[i].starObj.sprite = starSprite;
        }
        else if (isDuplicate)
            ui[i].starObj.sprite = starSprite;
        else
            ui[i].starObj.sprite = nullStarSprite;
        Debug.Log(lv + i.ToString());
        /*
        if(star[i] == 0)
        {
            
            stageStar++;
            star[i] = 1;
            PlayerPrefs.SetInt("saveInt", star[i]);
            PlayerPrefs.SetInt("saveStarCt", stageStar);
            PlayerPrefs.SetInt(strings[i], stageStar);
            IsComplete[i] = true;
            ui[i].starObj.sprite = starSprite;

        }
        */
        currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
    }
    public void RefreshStar(int i)
    {
        toggle = false;
        int lv = PlayerPrefs.GetInt("level");
        // Get boolean using PlayerPrefs
        var isDuplicate = PlayerPrefs.GetInt(lv + i.ToString()) == 1 ? true : false;
        if (isDuplicate)
        {
            ui[i].starObj.sprite = starSprite;
        }
        else
        {
            ui[i].starObj.sprite = nullStarSprite;
        }
    }

    public void ToggleTime()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void TestStar()
    {
        starCt++;
        PlayerPrefs.SetInt("saveStarCt", starCt);
        currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
    }
    

    public void CancelUpgrade() 
    {
        PlayerPrefs.SetInt("saveStarCt", starCt);
        currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
        Debug.Log("cancelUpgrade");
        foreach (Reinforce_Grid rg in rgs)
        {
            Debug.Log(PlayerPrefs.GetInt(rg.gameObject.name));
            rg.currTier = PlayerPrefs.GetInt(rg.gameObject.name);
            rg.Refresh();
        }
    }

    public void SaveUpgrade()
    {
        foreach (Reinforce_Grid rg in rgs)
        {
            rg.Save();
        }
        starCt = int.Parse(FindObjectOfType<Reinforce_Grid>().starTxt.text);
        PlayerPrefs.SetInt("saveStarCt", starCt);
        currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
    }

}
