using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEnemyLogicGA : GameAction
{
    public CombatantView Caster;//Ö´ÐÐÕß
    public List<CombatantView> targets;
    public Effect effect;
    public PlayEnemyLogicGA(CombatantView _Caster,List<CombatantView> _targets,Effect _effect)
    {
        Caster = _Caster;
        targets = _targets;
        effect = _effect;
    }

}
