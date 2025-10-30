using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    [SerializeField] private ArrowView arrowView;
    [SerializeField] private LayerMask targetLayerMask;//敌人的层
    public void StartTargeting(Vector3 startPos)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.SetUpArrow(startPos);

    }
    public EnemyView EndTargeting(Vector3 endPos)
    {
        arrowView.gameObject.SetActive(false);//将箭头设置为false
        RaycastHit2D hit = Physics2D.Raycast(endPos, Vector2.right, 1f, targetLayerMask);
        if (hit && hit.collider != null && hit.transform.TryGetComponent(out EnemyView enemyView))//向右发射一个单位的线段进行检测是否为目标层
        {
            return enemyView;
        }
        return null;

    }
}
