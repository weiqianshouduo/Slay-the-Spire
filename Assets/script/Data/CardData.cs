
using System.Collections.Generic;
using SerializeReferenceEditor;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/card")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public string Description;
    [field: SerializeField] public  int cost;
    [field: SerializeField] public Sprite Image;
    [field: SerializeReference, SR] public Effect ManualTargetEffect { get; private set; } = null;
    [field:SerializeField] public List<AutoTargetEffect> OtherEffects{ get; private set; }
    
}
