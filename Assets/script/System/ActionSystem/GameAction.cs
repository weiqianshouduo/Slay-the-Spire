using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction
{
    public List<GameAction> PreReactions { get; private set; } = new();//��ִ�б�����¼�֮ǰ������û�а���˵��¼�
    public List<GameAction> PerformReactions { get; private set; } = new();
    public List<GameAction> PostformReactions { get; private set; } = new();
}
