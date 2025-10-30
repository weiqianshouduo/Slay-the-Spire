using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatusEffectGA : GameAction
{
    public StatusEffectType type;
    public int stackCount;
    public List<CombatantView> Targets{ get; private set; }
    public AddStatusEffectGA(StatusEffectType _type,int _stackCount,List<CombatantView> _Targets)
    {
        type = _type;
        stackCount = _stackCount;
        Targets = _Targets;
    }
}
