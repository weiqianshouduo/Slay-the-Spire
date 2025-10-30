using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDardsEffect : Effect
{
    [SerializeField] private int drawCardMount;
    public override GameAction GetGameAction(List<CombatantView> targets,CombatantView caster)
    {
        DrawCardGA drawCardGA = new(drawCardMount);
        return drawCardGA;
    }
   
}
