using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmpText;

    private void OnEnable()
    {
        _tmpText.text = $"{Wallet.Money}";
        Wallet.MoneyChanged += GoldChange;
    }

    private void OnDisable()
    {
        Wallet.MoneyChanged -= GoldChange;
    }

    private void GoldChange(int gold)
    {
        _tmpText.text = $"{gold}";
    }
}
