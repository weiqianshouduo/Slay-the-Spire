using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndTheTurnButton : MonoBehaviour
{
 public  void OnButtonClick()
    {
        EnemyTurnGA enemyTurnGA = new();
        ActionSystem.Instance.Perform(enemyTurnGA);
        Debug.Log("Start click");
    }
   
}
