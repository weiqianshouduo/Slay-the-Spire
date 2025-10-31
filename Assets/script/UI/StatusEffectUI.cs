
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text stackCountText;//?
    public void SetUp(Sprite sprite,int _stackCountText)
    {
        image.sprite = sprite;
        stackCountText.text = _stackCountText.ToString();
    }
}
