using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemyStatusEffect : Effect
{
    [SerializeField] int StatusStacks;
    [SerializeField] StatusEffectType statusEffectType;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        AddEnemyStatusGA addEnemyStatusGA = new(caster, StatusStacks, statusEffectType);
        return addEnemyStatusGA;
    }
}
