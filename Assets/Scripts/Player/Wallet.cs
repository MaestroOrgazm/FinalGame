using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public static int Money { get; private set; }


    public static event UnityAction<int> MoneyChanged;

    public static void ChangeMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public static void SetMoney(int money)
    {
        Money = money;
        MoneyChanged?.Invoke(Money);
    }
}
