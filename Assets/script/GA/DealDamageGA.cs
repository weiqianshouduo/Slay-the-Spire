using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DealDamageGA : GameAction,IHaveCaster
{
    public int Damage { get; private set; }
    public CombatantView Caster{ get; set; }
    public List<CombatantView> Targets { get; set; }
    public DealDamageGA(int _Damage,List<CombatantView> _Targets,CombatantView caster)
    {
        Damage = _Damage;
        Targets = new(_Targets);
        Caster = caster;
    }
}
