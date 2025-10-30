using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D.IK;

public class CardHoverSystem : Singleton<CardHoverSystem>
{
    [SerializeField] private CardView cardViewHover;
    private bool isMove;
    public void Show(Card card, Vector3 startPos,Vector3 endPos)
    {
        if (isMove)
        {
            Debug.Log("IsMoving");
            return;
        }
        SetTheMoveState(true);
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.SetUp(card);
        cardViewHover.transform.position = startPos;
        cardViewHover.transform.DOMove(endPos, 0.15f).OnComplete(()=>SetTheMoveState(false));//�������ܿ���һ����
    }
    public void Hide()
    {
    if (cardViewHover != null)
    {
        cardViewHover.transform.DOKill(); // ��ֹ����
        cardViewHover.gameObject.SetActive(false);
    }
    SetTheMoveState(false); // ǿ������״̬

    }
    public void SetTheMoveState(bool b)
    {
        isMove = b;
    }
    public bool GetTheState()
    {
        return isMove;
    }
  
}
