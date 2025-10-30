using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private GameObject arrowHead;//箭头头部
    [SerializeField] private LineRenderer lineRenderer;//线的渲染器
    private Vector3 startPos;
    private void Update()
    {
        Vector3 endPos = MouseUtil.GetMousePositionInWorldSpace();
        Vector3 direction = -(startPos - arrowHead.transform.position).normalized;
        lineRenderer.SetPosition(1, endPos - direction * 0.5f);//将线的末端稍短于箭头头部
        arrowHead.transform.position = endPos;
        arrowHead.transform.right = direction;//将x轴的值设为direction
    }
    public void SetUpArrow(Vector3 _startPos)
    {
        startPos = _startPos;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1,MouseUtil.GetMousePositionInWorldSpace());
    }
}
