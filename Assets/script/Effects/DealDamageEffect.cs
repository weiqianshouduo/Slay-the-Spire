using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damgaeAmount;
    public override GameAction GetGameAction(List<CombatantView> targets,CombatantView caster)
    {
        DealDamageGA dealDamageGA = new(damgaeAmount, targets,caster);
        return dealDamageGA;
    }
   
    }

