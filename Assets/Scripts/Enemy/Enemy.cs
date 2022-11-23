using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(AudioSource))]

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Card _card;
    [SerializeField] protected AudioClip _deathSound;

    protected Animator _animator;
    protected BoxCollider2D _boxCollider;
    protected Rigidbody2D _rb;
    protected AudioSource _audioSource;
    protected int _health;
    protected int _damage;
    protected int _reward;
    protected float _deathDelay = 0.5f;
    protected const string Hit = "Hit";
    protected const string IsDeath = "IsDeath";

    public int Reward => _reward;
    public int Damage => _damage;
    public int Health => _health;

    private void Start()
    {
        _health = _card.Health;
        _damage = _card.Damage;
        _reward = _card.Reward;
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger(Hit);

        if (_health <= 0)
        {
            Wallet.ChangeMoney(_reward);
            _boxCollider.enabled = false;
            _rb.bodyType = RigidbodyType2D.Static;
            _animator.SetBool(IsDeath, true);
            _audioSource.clip = _deathSound;
            _audioSource.Play();
            Destroy(gameObject, _deathDelay);
        }
    }
}