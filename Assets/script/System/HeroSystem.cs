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
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);////�����������������Ϸ��������enemyTurnGA��Ԥ��Ӧ�¼���
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReation, ReactionTiming.POST);////���鿨����enemyturnGA�ĺ�Ӧ�¼���//ע����˻غϵ�Ԥ��Ӧ�ͺ�Ӧ
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
        ActionSystem.Instance.AddReacion(disAllCardsGA);//����
    }
    private void EnemyTurnPostReation(EnemyTurnGA enemyTurnGA)
    {
        int burnStacks = heroView.GetStatusEffectStack(StatusEffectType.BURN);//����ȼ��Ч��
        if (burnStacks > 0)
        {
            BurnTargetGA burnTargetGA = new(burnStacks, heroView);
            ActionSystem.Instance.AddReacion(burnTargetGA);
        }

        if (heroView.GetStatusEffectStack(StatusEffectType.Vulner) > 0)
        {
            heroView.RemoveStatusEffect(StatusEffectType.Vulner, 1);//ÿ�غϼ���һ��
        }
        if (heroView.GetStatusEffectStack(StatusEffectType.ARMOR) > 0)
        {
            ClearAllStatusGA clearAllStatusGA = new(heroView, StatusEffectType.ARMOR);//������ȫ��clear
            ActionSystem.Instance.AddReacion(clearAllStatusGA);
        }
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.AddReacion(drawCardGA);//�鿨
        //RefillCostGA refillCostGA = new();
        //ActionSystem.Instance.AddReacion(refillCostGA);
        //���԰����� ���ǲ���
    }
    private IEnumerator KillHeroPerformer(KillHeroGA killHeroGA)
    {
        Tween tween = heroView.transform.DOScale(Vector3.zero, 0.15f);
        yield return tween.WaitForCompletion();//�ȴ����ﶯ�����
    }
}
