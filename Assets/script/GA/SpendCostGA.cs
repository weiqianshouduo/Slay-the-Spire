using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendCostGA : GameAction
{
    public int Amount { get; set; }
    public SpendCostGA(int _Amount)
    {
        Amount = _Amount;
    }
}
