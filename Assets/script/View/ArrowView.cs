using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private GameObject arrowHead;//��ͷͷ��
    [SerializeField] private LineRenderer lineRenderer;//�ߵ���Ⱦ��
    private Vector3 startPos;
    private void Update()
    {
        Vector3 endPos = MouseUtil.GetMousePositionInWorldSpace();
        Vector3 direction = -(startPos - arrowHead.transform.position).normalized;
        lineRenderer.SetPosition(1, endPos - direction * 0.5f);//���ߵ�ĩ���Զ��ڼ�ͷͷ��
        arrowHead.transform.position = endPos;
        arrowHead.transform.right = direction;//��x���ֵ��Ϊdirection
    }
    public void SetUpArrow(Vector3 _startPos)
    {
        startPos = _startPos;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1,MouseUtil.GetMousePositionInWorldSpace());
    }
}
