using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (EnemyStateMashine))]

public class EnemyMoveToPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDistanse;

    private EnemyStateMashine _stateMashine;
    private Animator _animator;
    private Coroutine _coroutine;

    public event UnityAction<Player> PlayerReached;

    private void Awake()
    {
        _stateMashine = GetComponent<EnemyStateMashine>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _stateMashine.PlayerVisible += StartMove;
    }

    private void OnDisable()
    {
        _stateMashine.PlayerVisible -= StartMove;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void StartMove(Player player)
    {
        _coroutine = StartCoroutine(MoveToPlayer(player));
    }

    private IEnumerator MoveToPlayer(Player player)
    {
        _animator.SetBool("IsRun", true);

        while (Vector3.Distance(transform.position, player.transform.position) > _attackDistanse)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        _animator.SetBool("IsRun", false);
        PlayerReached?.Invoke(player);
    }
}
