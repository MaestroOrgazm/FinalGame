using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/Create new Dialog", order = 51)]

public class Dialog : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _text;

    public Sprite Icon => _icon;
    public string Name => _name;
    public string Text => _text;
}
