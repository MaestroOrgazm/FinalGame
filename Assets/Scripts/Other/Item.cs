using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(BoxCollider2D))]

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected int _value;
    [SerializeField] protected int _price;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;

    private float _deleteTime = 1f;

    public string Name => _name;
    public string Description => _description;
    public int Price => _price;
    public Sprite Icon => _icon;
    public int Value => _value;

    public abstract void TakeEffect(Player player);

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            GetComponent<AudioSource>().Play();
            TakeEffect(player);
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject.GetComponentInChildren<SpriteRenderer>());
            Destroy(gameObject, _deleteTime);
        }
    }
}
