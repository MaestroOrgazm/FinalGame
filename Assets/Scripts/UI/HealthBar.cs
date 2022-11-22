using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private Image _fillImage;

    private Color _currentColor;
    private Color _healColor = Color.green;
    private Color _damageColor = Color.yellow;
    private int _recoveryRate = 7;
    private Coroutine _updateBar;

    private void OnEnable()
    {
        _slider.maxValue = _player.MaxHealth;
        _slider.value = _slider.maxValue;
        _currentColor = _fillImage.color;
        _tmpText.text = ($"{_player.MaxHealth}/{_player.MaxHealth}");
        _player.HealthChanged += RestartCorrutine;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= RestartCorrutine;
    }

    private IEnumerator UpdateBar(int currentHealth, int maxHealth)
    {
        _slider.maxValue = maxHealth;

        while (_slider.value != currentHealth)
        {
            if (currentHealth > _slider.maxValue)
                currentHealth = (int)_slider.maxValue;

            _slider.value = Mathf.MoveTowards(_slider.value, currentHealth, _recoveryRate * Time.deltaTime);

            if (_slider.value > currentHealth)
                _fillImage.color = _damageColor;
            else if (_slider.value < currentHealth)
                _fillImage.color = _healColor;

            _tmpText.text = ($"{_player.CurrentHealth}/{_player.MaxHealth}");
            yield return new WaitForEndOfFrame();
        }

        _fillImage.color = _currentColor;
    }

    private void RestartCorrutine(int currentHealth, int maxHealth)
    {
        if (_updateBar != null)
        {
            StopCoroutine(_updateBar);
        }

        _updateBar = StartCoroutine(UpdateBar(currentHealth, maxHealth));
    }
}