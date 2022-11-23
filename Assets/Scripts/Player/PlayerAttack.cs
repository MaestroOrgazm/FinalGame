using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(Player))]
[RequireComponent (typeof(AudioSource))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _rayPoint;
    [SerializeField] private float _delay;
    [SerializeField] private PlayerMovment _playerMovment;

    private Player _player;
    private Animator _animator;
    private Coroutine _startAttack;
    private WaitForSeconds _attackDelay;
    private RaycastHit2D[] _hit;
    private int _damage;
    private bool _isAttack = false;
    private float _distance = 1.5f;
    private const string IsAttack = "Attack";

    private void OnEnable()
    {
        _playerMovment.StartAttack += Attack;
    }

    private void OnDisable()
    {
        _playerMovment.StartAttack -= Attack;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _attackDelay = new WaitForSeconds(_delay);
        _damage = _player.Damage;
    }

    private void FixedUpdate()
    {
        if (_playerMovment.NeedFlip == false)
            _hit = Physics2D.RaycastAll(_rayPoint.position, transform.right, _distance);
        else
            _hit = Physics2D.RaycastAll(_rayPoint.position, -transform.right, _distance);
    }

    private void Attack()
    {
        if (!_isAttack)
        {
            if (_startAttack != null)
                StopCoroutine(_startAttack);

            _isAttack = true;
            _startAttack = StartCoroutine(StartAttack());
        }
    }

    private IEnumerator StartAttack()
    {
        _animator.SetTrigger(IsAttack); 
        GetComponent<AudioSource>().Play();

        if (_hit.Length > 1)
        {
            if (_hit[1].collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }

        yield return _attackDelay;
        _isAttack = false;
    }
}