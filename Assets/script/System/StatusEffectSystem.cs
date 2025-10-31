using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<AddStatusEffectGA>(AddStatusEffectPerformer);
        ActionSystem.AttachPerformer<AddEnemyStatusGA>(AddEnemyStatusPerformer);
        ActionSystem.AttachPerformer<ClearAllStatusGA>(ClearAllStatusPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();
        ActionSystem.DetachPerformer<AddEnemyStatusGA>();
        ActionSystem.DetachPerformer<ClearAllStatusGA>();
    }

    private IEnumerator AddStatusEffectPerformer(AddStatusEffectGA addStatusEffectGA)
    {
        foreach (var target in addStatusEffectGA.Targets)
        {
            target.AddStatusEffect(addStatusEffectGA.type, addStatusEffectGA.stackCount);
            yield return null;
        }
    }
    private IEnumerator AddEnemyStatusPerformer(AddEnemyStatusGA addEnemyStatusGA)
    {
        addEnemyStatusGA.me.AddStatusEffect(addEnemyStatusGA.type, addEnemyStatusGA.Stacks);
        yield return null;
    }
    private IEnumerator ClearAllStatusPerformer(ClearAllStatusGA clearAllStatusGA)
    {
        CombatantView target = clearAllStatusGA.target;
        StatusEffectType statusEffectType = clearAllStatusGA.statusEffectType;
        if (target.GetStatusEffectStack(statusEffectType) > 0)
        {
            target.RemoveStatusEffect(statusEffectType, target.GetStatusEffectStack(statusEffectType));
        }
        yield return null;
    }
    
}
