using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(Boss))]

public class BossAttack : MonoBehaviour
{
    [SerializeField] private BossMove _bossMove;
    [SerializeField] private float _delay;
    [SerializeField] protected AudioClip _attackSound;


    private float _lastAttackTime;
    private Animator _animator;
    private Boss _boss;
    protected AudioSource _audioSource;
    private const string Attack = "Attack";

    private void OnEnable()
    {
        _boss = GetComponent<Boss>();
        _animator = GetComponent<Animator>();
        _bossMove.enabled = false;
        _audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Vector3.Distance(transform.position, _boss.Target.transform.position) > _bossMove.AttackDistanse)
            _bossMove.enabled = true;

        if (_boss.Target.CurrentHealth > 0)
        {
            if (_lastAttackTime <= 0)
            {
                _animator.SetTrigger(Attack);
                _audioSource.clip = _attackSound;
                _audioSource.Play();
                _lastAttackTime = _delay;
                _boss.Target.ApplyDamage(_boss.Damage);
            }
        }

        _lastAttackTime -= Time.deltaTime;
    }
}