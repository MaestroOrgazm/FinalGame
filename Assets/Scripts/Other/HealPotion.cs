using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : Item
{
    public override void TakeEffect(Player player)
    {
        player.ApplyHeal(_value);
    }
}
