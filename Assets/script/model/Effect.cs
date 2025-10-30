using System.Collections.Generic;
using UnityEngine.Windows.WebCam;

[System.Serializable]
public abstract class Effect
{
    public abstract GameAction GetGameAction(List<CombatantView> targets,CombatantView caster);
 
}
