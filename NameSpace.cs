using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    [System.Serializable]
    public struct QuestInfo
    {
        
        //public questType Type;
        //[System.Serializable]
        public enum questType
        {
            Clear, // 스테이지 클리어
            Spawn, // 유닛 소환
            Kill, // 적 처치
            KillbySkill, // 대 천사 스킬 적 처치
            KillbySword, // 전사 유닛으로 처치
            KillbyBow, // 궁수 유닛으로 처치
            BossHitbySkill, // 보스에게 스킬 사용
            Reinforce, // 강화
            SaveHP, // 석상 or 대천사 체력
            SaveUnit // 유닛 살려두기
        }

        public questType Type;

        public int QuestValue;

    }
    [System.Serializable]
    public struct QuestUI
    {
        public Image starObj;
        public Text text_Quest;
    }
}
