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
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);////�����������������Ϸ��������enemyTurnGA��Ԥ��Ӧ�¼���
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReation, ReactionTiming.POST);////���鿨����enemyturnGA�ĺ�Ӧ�¼���//ע����˻غϵ�Ԥ��Ӧ�ͺ�Ӧ
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
        //���԰����� ���ǲ���
    }
}
