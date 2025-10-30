using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    [SerializeField] private ArrowView arrowView;
    [SerializeField] private LayerMask targetLayerMask;//���˵Ĳ�
    public void StartTargeting(Vector3 startPos)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.SetUpArrow(startPos);

    }
    public EnemyView EndTargeting(Vector3 endPos)
    {
        arrowView.gameObject.SetActive(false);//����ͷ����Ϊfalse
        RaycastHit2D hit = Physics2D.Raycast(endPos, Vector2.right, 1f, targetLayerMask);
        if (hit && hit.collider != null && hit.transform.TryGetComponent(out EnemyView enemyView))//���ҷ���һ����λ���߶ν��м���Ƿ�ΪĿ���
        {
            return enemyView;
        }
        return null;

    }
}
