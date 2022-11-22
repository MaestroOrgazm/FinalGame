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
        _health -= damage;
        _animator.SetTrigger("Hit");

        if (_health <= 0)
        {
            Wallet.ChangeMoney(_reward);
            _boxCollider.enabled = false;
            _rb.bodyType = RigidbodyType2D.Static;
            _animator.SetBool("IsDeath", true);
            BossDie?.Invoke();
            _audioSource.clip = _deathSound;
            _audioSource.Play();
            Destroy(gameObject, _deathDelay);
        }
    }
}
