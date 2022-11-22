using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;

    public Item Item { get; private set; }
    public int Price { get; private set; }

    public event UnityAction<ItemView, Button> OnBuy;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnButtonClick);
    }

    public void SetItem(Item item)
    {
        _image.sprite = item.Icon;
        _label.text = item.Name;
        _description.text = item.Description;
        Price = item.Price;
        _priceText.text = $"{item.Price}";
        Item = item;
    }

    private void OnButtonClick()
    {
        OnBuy?.Invoke(this, _buyButton);
    }
}
