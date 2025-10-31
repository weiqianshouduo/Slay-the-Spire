
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CardOnlySee : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Image sprite;
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Cost;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private Card card;
    public void SetUp(Card _card)
    {
        sprite.sprite = _card.Image;
        Title.text = _card.Title;
        Cost.text = _card.Cost.ToString();
        Description.text = _card.Description;
        card = _card;
    }
     public void OnPointerDown(PointerEventData eventData)
    {
        ShowCardDescriptionSystem.Instance.Show(card);
    }
}
