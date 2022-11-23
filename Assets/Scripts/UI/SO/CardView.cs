using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Card _card;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _reward;

    private void Start()
    {
        _image.sprite = _card.Icon;
        _name.text = _card.Name;
        _health.text = _card.Health.ToString();
        _damage.text = _card.Damage.ToString();
        _reward.text = _card.Reward.ToString();
    }
}
