using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomEnemyTM : TargetMode
{
  public override List<CombatantView> GetTargets()
    {
        CombatantView combatantView = EnemySystem.Instance.enemyViews[Random.Range(0, EnemySystem.Instance.enemyViews.Count)];
        return new() { combatantView };
        
    }
}
