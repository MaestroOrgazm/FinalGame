using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _shopPannel;
    [SerializeField] private GameObject _content;
    [SerializeField] private List<Item> _items;
    [SerializeField] private ItemView _template;
    [SerializeField] private GameObject _activeButton;

    private Player _player;
    private PlayerMovment _playerMovment;
    private List<ItemView> _itemViews = new();

    private void OnDisable()
    {
        for (int i = 0; i < _itemViews.Count; i++)
        {
            _itemViews[i].OnBuy -= ItemSell;
        }

        if (_playerMovment != null)
            _playerMovment.PressActive -= ActiveObject;
    }

    private void Start()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _itemViews.Add(Instantiate(_template, _content.transform));
            _itemViews[i].SetItem(_items[i]);
            _itemViews[i].OnBuy += ItemSell;
        }
    }

    private void ItemSell(ItemView itemView, Button button)
    {
        if (Wallet.Money >= itemView.Price)
        {
            Wallet.ChangeMoney(-itemView.Price);
            itemView.Item.TakeEffect(_player);
            button.interactable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _activeButton.SetActive(true);
            _player = player;
            _playerMovment = _player.GetComponent<PlayerMovment>();
            _playerMovment.PressActive += ActiveObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _activeButton.SetActive(false);
    }

    private void ActiveObject()
    {
        _shopPannel.SetActive(true);
        _activeButton.SetActive(false);
        Time.timeScale = 0;
    }
}
