using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayCardGA : GameAction
{
    public EnemyView ManualTarget;//手动获取敌人
    //人物的应用
    public Card card;
    public PlayCardGA(Card _card)
    {
        card = _card;
    }
    public PlayCardGA(Card _card, EnemyView _Target)
    {
        card = _card;
        ManualTarget = _Target;
    }
    
}
