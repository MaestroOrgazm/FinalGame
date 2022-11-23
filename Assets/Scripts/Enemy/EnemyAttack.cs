using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(EnemyStateMashine))]
[RequireComponent (typeof(Enemy))]
[RequireComponent(typeof(AudioSource))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] protected AudioClip _attackSound;

    private EnemyStateMashine _stateMashine;
    private Coroutine _startAttack;
    private Enemy _enemy;
    private WaitForSeconds _wait;
    private Animator _animator;
    protected AudioSource _audioSource;
    private int _styleAttack;
    private const string Attack1 = "Attack1";
    private const string Attack2 = "Attack2";

    private void Awake()
    {
        _stateMashine = GetComponent<EnemyStateMashine>();
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _wait = new WaitForSeconds(_delay);
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _stateMashine.PlayerReached += StartAttackCoroutine;
    }

    private void OnDisable()
    {
        _stateMashine.PlayerReached -= StartAttackCoroutine;

        if (_startAttack != null)
            StopCoroutine(_startAttack);
    }

    private void StartAttackCoroutine(Player player)
    {
        _startAttack = StartCoroutine(StartAttack(player));
    }

    private IEnumerator StartAttack(Player player)
    {
        while (player.CurrentHealth >= 0)
        {
            _styleAttack = Random.Range(0, 2);

            if (_styleAttack > 0)
                _animator.SetTrigger(Attack1);
            else
                _animator.SetTrigger(Attack2);

            _audioSource.clip = _attackSound;
            _audioSource.Play();
            player.ApplyDamage(_enemy.Damage);
            yield return _wait;
        }
    }
}