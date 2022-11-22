using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Skill : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] private float _rotation;

    private float _deleteTime = 2f;
    private Player _player;
    private Transform _child;
    private Vector3 _direction;


    private void Start()
    {
        _direction = transform.right;

        if (_particleSystem.transform.localScale.x == -1)
        {
            _rotation = -_rotation;
            _direction = -transform.right;
        }

        _child = transform.GetChild(0);
        Destroy(gameObject, _delay);
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
        _child.transform.Rotate(new Vector3(0, 0, -_rotation));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            _player.ApplyHeal(_damage);
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject.GetComponentInChildren<SpriteRenderer>().GetComponentInChildren<ParticleSystem>());
            Destroy(gameObject.GetComponentInChildren<SpriteRenderer>());
            Destroy(gameObject, _deleteTime);
        }
    }

    public void ChangeScale(Transform transform)
    {
        _particleSystem.transform.localScale = transform.localScale;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}

