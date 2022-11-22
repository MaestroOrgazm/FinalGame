using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

public class Dekster : MonoBehaviour
{   
    [SerializeField] private float _healDeley;
    [SerializeField] private int _heal;
    [SerializeField] private int _speed;
    [SerializeField] private Player _player;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _collider;
    private Vector3 _distanseLeft = new Vector3(0.8f, -1.7f,0);
    private Vector3 _distanseRight = new Vector3(0.8f, 1.7f, 0);
    private float _lastHealTime;
    private int _zero = 0;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        _collider.enabled = false;
    }

    private void Update()
    {
        if (_player != null)
        {
            Move();

            if (_player.CurrentHealth > _zero && _player.CurrentHealth < _player.MaxHealth)
                GiveHeal();
            else if (_player.CurrentHealth <= _zero)
                Death();
        }

        _lastHealTime -= Time.deltaTime;
    }

    public void SetPlayer(Player player)
    {
        player.SetDexter();
        _player = player;
    }

    private void Move()
    {
        if (_player.gameObject.GetComponent<Transform>().localScale.x > 0)
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;

        if (_spriteRenderer.flipX == false)
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position - _distanseLeft, _speed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position + _distanseRight, _speed * Time.deltaTime);
    }

    private void GiveHeal()
    {
        if (_lastHealTime <= _zero)
        {
            _animator.Play("Attack2"); 
            GetComponent<AudioSource>().Play();
            _lastHealTime = _healDeley;
            _player.ApplyHeal(_heal);
        }
    }

    private void Death()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _collider.enabled = true;
        _animator.Play("Death");
    }
}
