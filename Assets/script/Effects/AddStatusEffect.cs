using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatusEffect : Effect
{
    [SerializeField] private int stackCount;
    [SerializeField] private StatusEffectType statusEffectType;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        AddStatusEffectGA addStatusEffectGA = new(statusEffectType, stackCount, targets);
        return addStatusEffectGA;
    }
}
