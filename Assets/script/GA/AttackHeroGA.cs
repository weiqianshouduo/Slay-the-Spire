using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHeroGA :GameAction,IHaveCaster
{
    public EnemyView Attacker { get; private set; }
    public CombatantView Caster { get;set; }
     public AttackHeroGA(EnemyView _Attacker)
    {
        Attacker = _Attacker;
        Caster = Attacker;
    }
}
