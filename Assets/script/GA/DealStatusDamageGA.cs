using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealStatusDamageGA :GameAction
{
    public int Damage { get; private set; }
    public CombatantView target { get; private set; }
    public DealStatusDamageGA(int _Damage,CombatantView _target)
    {
        Damage = _Damage;
        target = _target;
    }
   
}
