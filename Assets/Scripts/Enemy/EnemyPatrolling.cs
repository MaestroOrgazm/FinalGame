using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField] private float _speed;

    public bool NeedFlip { get; private set; } = true;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait = new WaitForSeconds(3);
    private Coroutine _waiting;
    private Coroutine _moving;
    private Vector3 _direction = new Vector3(5, 0, 0);
    private Vector3 _needPoint;
    private const string IsRun = "IsRun";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ChangeDirection();
    }

    private void OnDisable()
    {
        if (_waiting != null)
            StopCoroutine(_waiting);
        
        if (_moving != null)
            StopCoroutine(_moving);
    }

    private void ChangeDirection()
    {
        _needPoint = transform.position + _direction;
        _direction = -_direction;
        NeedFlip = !NeedFlip;
        _spriteRenderer.flipX = NeedFlip;
        _animator.SetBool(IsRun, true); 
        _moving = StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        while (transform.position.x != _needPoint.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (_needPoint.x, transform.position.y), _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        _waiting = StartCoroutine(Waiting());
    }

    private IEnumerator Waiting()
    {
        _animator.SetBool(IsRun, false);
        yield return _wait;
        ChangeDirection();
    }
}