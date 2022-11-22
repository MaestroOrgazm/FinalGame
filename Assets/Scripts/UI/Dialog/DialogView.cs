using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _nextButton;

    public event UnityAction OnNext;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(OnButtonClick);
    }

    public void SetDialog(Dialog dialog)
    {
        _image.sprite = dialog.Icon;
        _name.text = dialog.Name;
        _text.text = dialog.Text;
    }

    private void OnButtonClick()
    {
        OnNext?.Invoke();
    }
}
