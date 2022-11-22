using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SetPlayerTrigger : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private GameObject _border;

    private void OnDisable()
    {
        _boss.BossDie -= BossDie;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_boss.Target == null)
        {
            if (collision.TryGetComponent(out Player player))
            {
                GetComponent<AudioSource>().Play();
                _boss.SetPlayer(player);
                _border.SetActive(true);
                _boss.BossDie += BossDie;
            }
        }
    }

    private void BossDie()
    {
        Destroy(_border);
        Destroy(this.gameObject);
    }
}
