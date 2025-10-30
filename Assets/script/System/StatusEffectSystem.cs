using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<AddStatusEffectGA>(AddStatusEffectPerformer);
        ActionSystem.AttachPerformer<AddEnemyStatusGA>(AddEnemyStatusPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();
        ActionSystem.DetachPerformer<AddEnemyStatusGA>();
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
    
}
