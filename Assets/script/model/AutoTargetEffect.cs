
using SerializeReferenceEditor;
using UnityEngine;
[System.Serializable]
public class AutoTargetEffect
{
    [field: SerializeReference, SR] public TargetMode targetMode { get; private set; }
    [field: SerializeReference, SR] public Effect Effect { get; private set; }

  //��Ч�������ö����ڷ�װ��һ��
    }

