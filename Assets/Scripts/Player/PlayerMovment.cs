using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]
[RequireComponent (typeof (Player))]

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForse;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GroundCheker _cheker;
    [SerializeField] private Skill _template;
    [SerializeField] private float _skillDelay;

    public bool NeedFlip { get; private set; } = false;

    private PlayerInput _playerInput;
    private Player _player;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Vector3 _defaultScale;
    private Vector3 _flip = new Vector3(-1f, 1, 1);
    private float _limit = 0.05f;
    private float _moveDirection;
    private int _deltaFlip = 0;
    private bool _isFly;
    private bool _isSkillReady = true;

    public event UnityAction StartAttack;
    public event UnityAction PressActive;
    public event UnityAction<bool> SkillReady;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Attack.performed += ctx => OnAttack();
        _playerInput.Player.Jump.performed += ctx => OnJump();
        _playerInput.Player.Active.performed += ctx => OnActive();
        _playerInput.Player.Skill.performed += ctx => OnSkill();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _defaultScale = transform.localScale;
    }

    private void OnEnable()
    {
        _player.PlayerDeath += PlayerDeath;
        _player.PlayerHurt += PlayerHurt;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _player.PlayerDeath -= PlayerDeath;
        _player.PlayerHurt -= PlayerHurt;
    }

    private void Update()
    {
        _isFly = _cheker.IsFly;
        _moveDirection = _playerInput.Player.Move.ReadValue<float>();
        Move();

        if (_isFly)
        {
            _animator.SetBool("IsFly", true);
            _animator.SetBool("IsJump", true);
        }
        else
        {
            _animator.SetBool("IsJump", false);
            _animator.SetBool("IsFly", false);
        }
    }

    public void OnAttack()
    {
        if (_isFly == false)
        {
            StartAttack?.Invoke();
        }
    }

    public void OnActive()
    {
        PressActive?.Invoke();
    }

    public void OnJump()
    {
        if (Mathf.Abs(_rigidbody2D.velocity.y) < _limit)
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpForse), ForceMode2D.Impulse);
            _animator.SetBool("IsJump", true);
        }
    }
    public void OnSkill()
    {
        if (_isSkillReady)
        {
            Skill skill = Instantiate(_template, _shootPoint.position, Quaternion.identity);
            skill.ChangeScale(transform);
            skill.SetPlayer(_player);
            _animator.SetTrigger("Skill");
            _isSkillReady = false;
            SkillReady?.Invoke(false);
            Invoke(nameof(OnReady), _skillDelay);
        }
    }

    public void OnOffInput(bool IsValue)
    {
        if (IsValue)
            _playerInput.Enable();
        else
            _playerInput.Disable();
    }

    private void OnReady()
    {
        _isSkillReady = true;
        SkillReady?.Invoke(true);
    }

    private void Move()
    {
        if (_moveDirection != 0)
            _animator.SetBool("IsRun", true);
        else
            _animator.SetBool("IsRun", false);

        float scaledMoveSpeed = _speed * Time.deltaTime;
        transform.position += new Vector3(_moveDirection, 0, 0) * scaledMoveSpeed;

        if (_moveDirection > _deltaFlip)
        {
            transform.localScale = _defaultScale;
            NeedFlip = false;
        }
        else if (_moveDirection < _deltaFlip)
        {
            transform.localScale = _flip;
            NeedFlip = true;
        }
    }

    private void PlayerHurt()
    {
        _animator.SetTrigger("Hurt");
    }

    private void PlayerDeath()
    {
        _animator.SetBool("IsDeath", true);
        _playerInput.Disable();
    }
}