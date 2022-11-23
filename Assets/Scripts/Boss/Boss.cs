using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(AudioSource))]

public class Boss : Enemy
{
    [SerializeField] private BossMove _move;

    private Player _player;

    public Player Target => _player;

    public event UnityAction BossDie;

    public void SetPlayer(Player player)
    {
        _player = player;
        _move.enabled = true;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (_health <= 0)
        {
            BossDie?.Invoke();
        }
    }
}
