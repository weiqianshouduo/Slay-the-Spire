using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTargetGA : GameAction
{
    public int burnDamage;
    public CombatantView target;
    public BurnTargetGA(int _burnDamage,CombatantView _target)
    {
        burnDamage = _burnDamage;
        target = _target;
    }
   
}
