using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayCardGA : GameAction
{
    public EnemyView ManualTarget;//�ֶ���ȡ����
    //�����Ӧ��
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
