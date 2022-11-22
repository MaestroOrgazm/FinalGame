using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateMashine : MonoBehaviour
{
    [SerializeField] private EnemyPatrolling _patrolling;
    [SerializeField] private EnemyMoveToPlayer _moveToPlayer;
    [SerializeField] private EnemyAttack _attack;
    [SerializeField] private Transform _rayPoint;

    private RaycastHit2D[] _hit;
    private float _distance = 4f;
    private bool _isMoveTo = false;

    public event UnityAction<Player> PlayerVisible; 
    public event UnityAction<Player> PlayerReached;

    private void OnEnable()
    {
        _attack.enabled = false;
        _moveToPlayer.enabled = false;
        _patrolling.enabled = true;
        _moveToPlayer.PlayerReached += StartAttack;
    }

    private void OnDisable()
    {
        _moveToPlayer.PlayerReached -= StartAttack;
    }

    private void FixedUpdate()
    {
        if (_patrolling.NeedFlip == false)
            _hit = Physics2D.RaycastAll(_rayPoint.position, transform.right, _distance);
        else
            _hit = Physics2D.RaycastAll(_rayPoint.position, -transform.right, _distance);

        if (_hit.Length > 1)
        {
            if (_hit[1].collider.gameObject.TryGetComponent(out Player player) == true)
            {
                if (!_isMoveTo)
                {
                    StartMove();
                    PlayerVisible?.Invoke(player);
                }
            }
        }

        if (_isMoveTo)
        {
            if (_hit.Length == 1)
                StartPatrol();
        }
    }

    private void StartPatrol()
    {
        _isMoveTo = false;
        _moveToPlayer.enabled = false;
        _attack.enabled = false;
        _patrolling.enabled = true;
    }

    private void StartMove()
    {
        _isMoveTo = true;
        _patrolling.enabled = false;
        _moveToPlayer.enabled = true;
    }

    private void StartAttack(Player player)
    {
        _moveToPlayer.enabled = false;
        _attack.enabled = true;
        PlayerReached?.Invoke(player);
    }
}
