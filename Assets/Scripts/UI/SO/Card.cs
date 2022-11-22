using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Card", menuName = "Card/Create new Card", order = 51)]

public class Card : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;

    public string Name => _name;
    public int Health => _health;
    public int Damage => _damage;
    public int Reward => _reward;
    public Sprite Icon => _icon;
}
