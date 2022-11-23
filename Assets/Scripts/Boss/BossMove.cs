using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class BossMove : MonoBehaviour
{
    [SerializeField] private BossAttack _bossAttack;
    [SerializeField] private BossTeleport _bossTeleport;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDistanse;

    public float AttackDistanse => _attackDistanse;
    public float HeightDifference => _heightDifference;

    private Animator _animator;
    private Boss _boss;
    private Vector3 _needPoint;
    private int _heightDifference = 4;
    private Coroutine _movingToPlayer;
    private SpriteRenderer _spriteRenderer;
    private bool _needFlip = true;
    private const string IsRun = "IsRun";


    private void OnEnable()
    {
        _bossAttack.enabled = false;
        _bossTeleport.enabled = false;
        _boss = GetComponent<Boss>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartMove();
    }

    private void OnDisable()
    {
        _animator.SetBool(IsRun, false);
        StopCoroutine(_movingToPlayer);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.y - _boss.Target.transform.position.y) > _heightDifference || Mathf.Abs(_boss.Target.transform.position.y - transform.position.y) > _heightDifference)
        {
            _bossTeleport.enabled = true;
        }

        if (_boss.Target.transform.position.x > transform.position.x)
            _spriteRenderer.flipX = _needFlip;
        else
            _spriteRenderer.flipX = !_needFlip;
    }

    private IEnumerator MoveToPlayer()
    {
        while (Vector3.Distance(transform.position, _boss.Target.transform.position) > _attackDistanse)
        {
            transform.position = Vector2.MoveTowards(transform.position, _boss.Target.transform.position, _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        _bossAttack.enabled = true;
    }

    private void StartMove()
    {
        _animator.SetBool(IsRun, true);
        _movingToPlayer = StartCoroutine(MoveToPlayer());
    }
}