using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] private SpriteRenderer EnemyNextActionSr;
    [SerializeField] private TMP_Text WantText;
  
    public List<EnemyAutoEffect> enemyLogic = new();
    public int currentStep = 0;
    public void SetUp(EnemyData enemyData)
    {
        SetUpBase(enemyData.Hp, enemyData.Image);
        enemyLogic = enemyData.EnemyLogic;//´«ÈëenemyÂß¼­
    }
    public void UpdateNextActionText(Sprite sprite,string  _stack)
    {
        WantText.text = _stack;
        EnemyNextActionSr.sprite = sprite;
    }
    
}
