using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions :Singleton<Interactions>
{
    public bool PlayerIsDragging { get; set; } = false;//是否能后互动
    public bool PlayerCanInteract()
    {
        if (!ActionSystem.Instance.IsPerforming) return true;//是否正在执行GameAction
        else return false;
    }
    public bool PlayerCanHover()
    {
        if (PlayerIsDragging) return false;
        else return true;
    }
}
    
