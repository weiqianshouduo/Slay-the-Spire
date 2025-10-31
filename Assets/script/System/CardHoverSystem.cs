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

        SetTheMoveState(true);//正在移动

        cardViewHover.gameObject.SetActive(true);
        cardViewHover.SetUp(card);//设置视图的属性
        cardViewHover.transform.position = startPos;//将视图移动到选取的卡牌位置
        cardViewHover.transform.DOMove(endPos, 0.15f).OnComplete(()=>SetTheMoveState(false));//动画才能看下一张牌
    }
    public void Hide()
    {
    if (cardViewHover != null)
    {
        cardViewHover.transform.DOKill(); // 终止动画
        cardViewHover.gameObject.SetActive(false);
    }
    SetTheMoveState(false); // 强制重置状态

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
