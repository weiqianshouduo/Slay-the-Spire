
using SerializeReferenceEditor;
using UnityEngine;
[System.Serializable]
public class EnemyAutoEffect 
{
     public int Stack;
    [field: SerializeReference, SR] public TargetMode targetMode { get; private set; }
    [field: SerializeReference, SR] public Effect Effect { get; private set; }
    [field:SerializeField] public Sprite Image{ get; private set; }
}
