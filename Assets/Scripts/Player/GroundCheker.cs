using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheker : MonoBehaviour
{
    public bool IsFly => _groundList.Count == 0;

    private List<IsGround> _groundList = new List<IsGround>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IsGround ground))
        {
            _groundList.Add(ground);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IsGround ground))
        {
            if (_groundList.Contains(ground))
                _groundList.Remove(ground);
        }
    }
}
