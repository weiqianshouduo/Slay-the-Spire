using System.Collections;
using System.Collections.Generic;
using SerializeReferenceEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/enemydata")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public Sprite Image{ get; private set; }
    [field: SerializeField] public int Hp { get; private set; }
    [field: SerializeField] public int AttackPower { get; private set; }
    [field: SerializeReference, SR] public List<EnemyAutoEffect> EnemyLogic;//用于记录敌人应该做的逻辑

}
