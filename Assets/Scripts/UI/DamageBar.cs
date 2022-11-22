using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _tmpText;

    private void OnEnable()
    {
        _tmpText.text = $"{_player.Damage}";
        _player.DamageChanged += DamageChange;
    }

    private void OnDisable()
    {
        _player.DamageChanged -= DamageChange;
    }

    private void DamageChange(int damage)
    {
        _tmpText.text = $"{damage}";
    }
}
