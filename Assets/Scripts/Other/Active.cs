using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.GetComponent<PlayerMovment>().PressActive += ActiveObject;
        }
    }

    private void ActiveObject()
    {
        Destroy(gameObject);
    }
}
