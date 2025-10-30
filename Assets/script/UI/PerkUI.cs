using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public Perk perk { get; private set; }
    public void SetUp(Perk _perk)
    {
        perk = _perk;
        image.sprite = _perk.Image;
        
    }
    
}
