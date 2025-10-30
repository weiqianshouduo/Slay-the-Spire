using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView heroView { get; private set; }
    public void SetUp(HeroData heroData)
    {
        heroView.SetUp(heroData);
    }
    void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);////将丢弃所有牌这个游戏动作绑再enemyTurnGA的预反应事件中
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReation, ReactionTiming.POST);////将抽卡绑定在enemyturnGA的后反应事件中//注册敌人回合的预反应和后反应
    }
    void OnDisable()
    {
        ActionSystem.UnsubscribeReation<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReation<EnemyTurnGA>(EnemyTurnPostReation, ReactionTiming.POST);
    }


    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DisAllCardsGA disAllCardsGA = new();
        ActionSystem.Instance.AddReacion(disAllCardsGA);
    }
    private void EnemyTurnPostReation(EnemyTurnGA enemyTurnGA)
    {
        int burnStacks = heroView.GetStatusEffectStack(StatusEffectType.BURN);
        if (burnStacks > 0)
        {
            BurnTargetGA burnTargetGA = new(burnStacks, heroView);
            ActionSystem.Instance.AddReacion(burnTargetGA);
        }
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.AddReacion(drawCardGA);
        //RefillCostGA refillCostGA = new();
        //ActionSystem.Instance.AddReacion(refillCostGA);
        //可以绑在着 但是不好
    }
}
