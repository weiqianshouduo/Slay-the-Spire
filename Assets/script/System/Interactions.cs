using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions :Singleton<Interactions>
{
    public bool PlayerIsDragging { get; set; } = false;//�Ƿ��ܺ󻥶�
    public bool PlayerCanInteract()
    {
        if (!ActionSystem.Instance.IsPerforming) return true;//�Ƿ�����ִ��GameAction
        else return false;
    }
    public bool PlayerCanHover()
    {
        if (PlayerIsDragging) return false;
        else return true;
    }
}
    
