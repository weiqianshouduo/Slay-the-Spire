using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformEffectGA :GameAction
{
    public Effect effect;
    public List<CombatantView> targets;
   public  PerformEffectGA(Effect _effect,List<CombatantView> _targets)
    {
        effect = _effect;
        targets = _targets == null?null : new(_targets);
    } 
}
