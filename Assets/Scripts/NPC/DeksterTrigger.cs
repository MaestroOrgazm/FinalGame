using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeksterTrigger : MonoBehaviour
{
    [SerializeField] private Dekster _dekster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _dekster.SetPlayer(player);
            player.SetDexter();
        }
    }
}
