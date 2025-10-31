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
            target.AddStatusEffect(addStatusEffectGA.type, addStatusEffectGA.stackCount);//敌人视图里面的添加逻辑
            yield return null;
        }
    }
    private IEnumerator AddEnemyStatusPerformer(AddEnemyStatusGA addEnemyStatusGA)
    {
        addEnemyStatusGA.me.AddStatusEffect(addEnemyStatusGA.type, addEnemyStatusGA.Stacks);//就是添加到caster的属性
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
