using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchSystem : MonoBehaviour
{
    [SerializeField] private HeroData heroData;//将设置人物属性转移到一个地方 解耦
    [SerializeField] private PerkData perkData;
    [SerializeField] private List<EnemyData> enemyDatas;
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SystemStartGA>(SystemStartGAPerformer);
    }
    private void ODisable()
    {
        ActionSystem.DetachPerformer<SystemStartGA>();
    }
    private void Start()
    {
         HeroSystem.Instance.SetUp(heroData);//给人物初始化
        CardSystem.Instance.SetUp(heroData.Decks);//给牌组初始化
        EnemySystem.Instance.SetUp(enemyDatas);//给敌人列表初始化
        if (perkData != null)
        {
            PerkSystem.Instance.AddPerk(new Perk(perkData));
        }
        SystemStartGA systemStartGA = new();
        ActionSystem.Instance.Perform(systemStartGA);
      
    }

    private IEnumerator SystemStartGAPerformer(SystemStartGA systemStartGA)
    {
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.AddReacion(drawCardGA);
        yield return null;
    }
}//全盘的初始化逻辑都由此处来处理
 