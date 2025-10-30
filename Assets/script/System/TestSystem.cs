using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    [SerializeField] private List<CardData> deckDatas;
    private void Start()
    {
        CardSystem.Instance.SetUp(deckDatas);
    }
}
