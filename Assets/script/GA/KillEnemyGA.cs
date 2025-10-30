using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyGA : GameAction
{
    public EnemyView target { get; private set; }
    public KillEnemyGA(EnemyView _target){
        target = _target;
    }

}
