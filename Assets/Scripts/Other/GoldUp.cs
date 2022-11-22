using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldUp : Item
{
    public override void TakeEffect(Player player)
    {
        Wallet.ChangeMoney(_value);
    }
}
