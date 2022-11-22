using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _dekster = null;

    private static int _money = 0;
    private static int _damage = 2;
    private static bool _isDekster = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Wallet.SetMoney(_money);
            player.SetDamage(_damage);

            if (_isDekster)
                _dekster.SetActive(true);

            Destroy(this.gameObject);
        }
    }

    public static void SetValue(int money, int damage, bool isDekster)
    {
        _isDekster = isDekster;
        _money = money;
        _damage = damage;
    }

    public static void DeleteValue()
    {
        _isDekster = false;
        _money = 0;
        _damage = 2;
    }
}
