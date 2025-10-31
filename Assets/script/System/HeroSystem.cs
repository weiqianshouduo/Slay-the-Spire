using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        ActionSystem.AttachPerformer<KillHeroGA>(KillHeroPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);////将丢弃所有牌这个游戏动作绑再enemyTurnGA的预反应事件中
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReation, ReactionTiming.POST);////将抽卡绑定在enemyturnGA的后反应事件中//注册敌人回合的预反应和后反应
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<KillHeroGA>();
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
        if (heroView.GetStatusEffectStack(StatusEffectType.Vulner) > 0)
        {
            heroView.RemoveStatusEffect(StatusEffectType.Vulner, 1);
        }
        if (heroView.GetStatusEffectStack(StatusEffectType.ARMOR) > 0)
        {
            ClearAllStatusGA clearAllStatusGA = new(heroView, StatusEffectType.ARMOR);
            ActionSystem.Instance.AddReacion(clearAllStatusGA);
        }
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.AddReacion(drawCardGA);
        //RefillCostGA refillCostGA = new();
        //ActionSystem.Instance.AddReacion(refillCostGA);
        //可以绑在着 但是不好
    }
    private IEnumerator KillHeroPerformer(KillHeroGA killHeroGA)
    {
        Tween tween = heroView.transform.DOScale(Vector3.zero, 0.15f);
        yield return tween.WaitForCompletion();//等待人物动画完结
    }
}
