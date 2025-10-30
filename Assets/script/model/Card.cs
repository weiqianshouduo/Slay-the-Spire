using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Card
{
    public string Title => cardData.name;
    public string Description => cardData.Description;
    public Sprite Image => cardData.Image;
    public int Cost { get; private set; }
    public Effect ManualTargeteffect => cardData.ManualTargetEffect;
    public List<AutoTargetEffect> OtherEffects => cardData.OtherEffects;
    private readonly CardData cardData;
    public Card(CardData _cardData)
    {
        cardData = _cardData;
        Cost = _cardData.cost;
    }
}
