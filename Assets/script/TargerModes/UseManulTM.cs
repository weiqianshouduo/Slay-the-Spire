using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseManulTM : TargetMode
{
    public CombatantView Target;
    public override List<CombatantView> GetTargets()
    {
        return new() { Target };
    }
 
}
