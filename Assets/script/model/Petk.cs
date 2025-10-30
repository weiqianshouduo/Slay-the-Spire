using System.Collections.Generic;
using UnityEngine;

public class Perk
{
    public Sprite Image => data.Image;
    private readonly PerkData data;
    private readonly PerkConditon perkConditon;
    private readonly AutoTargetEffect effect;
    public Perk(PerkData perkData)
    {
        data = perkData;
        perkConditon = data.perkConditon;
        effect = data.autoTargetEffect;
    }
    public void OnAdd()
    {
        perkConditon.SubscribeCondition(Reaction);//��Ч������ע�ᵽһ��gameAction
    }
    public void OnMove()
    {
        perkConditon.UnsubscribeCondition(Reaction);
    }
    public void Reaction(GameAction gameAction)
    {
        if (perkConditon.SubCondtionIsMet(gameAction))
        {
            List<CombatantView> targets = new();
            if (data.UseActionCasterAsTarget && gameAction is IHaveCaster haveCaster)
            {
                targets.Add(haveCaster.Caster);//��ʩ������ӵ�perkִ�е�target��
            }
            if (data.UseAutoTarget)
            {
                targets.AddRange(effect.targetMode.GetTargets());
            }
            GameAction perkEffectAction = effect.Effect.GetGameAction(targets, HeroSystem.Instance.heroView);//��ȡeffect��ע���gameaction
            ActionSystem.Instance.AddReacion(perkEffectAction);
        }
    }
}

