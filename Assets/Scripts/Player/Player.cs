using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovment))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;

    private int _currentHealth;

    public bool IsDekster { get; private set; }
    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public int Damage => _damage;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> DamageChanged;
    public event UnityAction PlayerDeath;
    public event UnityAction PlayerHurt;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth,_maxHealth);
        PlayerHurt?.Invoke();

        if (_currentHealth <= 0)
        {
            PlayerDeath?.Invoke();
            Time.timeScale = 0;
        }
    }

    public void ApplyHeal(int heal)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += heal;
            HealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
        }
    }

    public void ChangeDamage(int damage)
    {
        _damage += damage;
        DamageChanged?.Invoke(_damage);
    }

    public void SetDexter()
    {
        IsDekster = true;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}