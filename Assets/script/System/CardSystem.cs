using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CardSystem :Singleton<CardSystem>
{
    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawCardPoint;
    [SerializeField] private Transform discardPilePoint;
    private readonly List<Card> drawPile = new();//抽牌区
    private readonly List<Card> discardPile = new();//弃牌区
    private readonly List<Card> hand = new();//手牌区 
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardGA>(DrawCardPerformer);
        ActionSystem.AttachPerformer<DisAllCardsGA>(DiscardAllPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
  
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardGA>();
        ActionSystem.DetachPerformer<DisAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
       
    }
    public void SetUp(List<CardData> deckData)
    {
     foreach(var CardData in deckData)
        {
            Card card = new(CardData);//创造新的card为抽牌堆添加数据
            drawPile.Add(card);
        }   
    }

    private IEnumerator DrawCardPerformer(DrawCardGA drawCardGA)
    {
        int actualAmount = math.min(drawCardGA.mount, drawPile.Count);//得到抽牌的数量和抽牌堆的比
        int notDrawAmount = drawCardGA.mount - actualAmount;//检查抽牌区是否能够玩家抽取应有的牌
        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();//抽牌
        }
        if (notDrawAmount > 0)
        {
            RefillDeck();//补充牌库 
            for(int i = 0; i < notDrawAmount; i++)
            {
                if (!(drawPile.Count == 0 && discardPile.Count == 0))
                {
                    yield return DrawCard();
                }
            }
        }

    }
    private IEnumerator DiscardAllPerformer(DisAllCardsGA disAllCardsGA)
    {
        foreach (var card in hand)
        {
           
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }
    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();//抽牌堆返回card
        hand.Add(card);//手牌列表添加
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawCardPoint.position, Quaternion.identity);
        yield return handView.AddCard(cardView);//可视手牌添加到手牌
    }
    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);//将弃牌区的重新洗牌添加到抽牌区
        discardPile.Clear();
    }
    private IEnumerator DiscardCard(CardView cardView)
    {
         discardPile.Add(cardView.card);//将牌添加到丢牌区
        cardView.transform.DOScale(Vector3.zero, .15f);//将手牌缩小
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, .15f);//移动手牌
        yield return tween.WaitForCompletion();//暂停当前协程的执行，等待 DOTween 动画完成后再继续执行后续代码
        Destroy(cardView.gameObject);
    }
    private IEnumerator PlayCardPerformer(PlayCardGA playGardGA)
    {
        hand.Remove(playGardGA.card);//将卡牌移除手卡
        discardPile.Add(playGardGA.card);//添加到弃牌区
        CardView cardView = handView.RemoveCard(playGardGA.card);//执行移除动画和视图
        yield return DiscardCard(cardView);

        SpendCostGA spendCostGA = new(playGardGA.card.Cost);
        ActionSystem.Instance.AddReacion(spendCostGA);

        if (playGardGA.card.ManualTargeteffect != null)
        {
            PerformEffectGA performEffectGA = new(playGardGA.card.ManualTargeteffect, new() { playGardGA.ManualTarget });
            ActionSystem.Instance.AddReacion(performEffectGA);
        }
        
        foreach(var effectWrapper in playGardGA.card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.targetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.Effect,targets);
            ActionSystem.Instance.AddReacion(performEffectGA);
        }
        // 卡牌效果
    }

    
}
