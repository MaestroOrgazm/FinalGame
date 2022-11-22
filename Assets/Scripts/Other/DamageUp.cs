using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Item
{
    public override void TakeEffect(Player player)
    {
        player.ChangeDamage(_value);
    }
}
