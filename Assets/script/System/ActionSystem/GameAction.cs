using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction
{
    public List<GameAction> PreReactions { get; private set; } = new();//在执行本体的事件之前检测得有没有绑别人的事件
    public List<GameAction> PerformReactions { get; private set; } = new();
    public List<GameAction> PostformReactions { get; private set; } = new();
}
