using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.ApplyDamage(player.MaxHealth);
        }
        else if (collision.TryGetComponent(out Enemy enemy))
        {
            Destroy(enemy.gameObject);
        }
    }
}
