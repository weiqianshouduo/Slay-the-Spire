using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnemyNextActionGA : GameAction
{
   public Sprite Image { get; private set; }
    public EnemyView Caster { get; private set; }
    public string stack = null;
   public ShowEnemyNextActionGA(Sprite sprite,EnemyView _Caster,string _stack)
    {
        Image = sprite;
        Caster = _Caster;
        stack = _stack;
        
    }
   
}
