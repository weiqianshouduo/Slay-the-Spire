using System.Collections;
using System.Collections.Generic;
using SerializeReferenceEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Perk")]
public class PerkData : ScriptableObject
{
   [field: SerializeField] public Sprite Image { get; private set; }//图标 可设置在物下
    [field: SerializeReference, SR] public PerkConditon perkConditon { get; private set; }//perk应该触发的时机
    [field: SerializeReference, SR] public AutoTargetEffect autoTargetEffect { get; private set; }//效果
    [field: SerializeField] public bool UseAutoTarget { get; private set; } = true;
    [field: SerializeField] public bool UseActionCasterAsTarget { get; private set; } = false;

}
