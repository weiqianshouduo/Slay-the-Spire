using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAllStatusGA : GameAction
{
    public CombatantView target { get; private set; }
    public StatusEffectType statusEffectType;
    public ClearAllStatusGA(CombatantView _target,StatusEffectType _statusEffectType)
    {
        target = _target;
        statusEffectType = _statusEffectType;
    }
    
}
