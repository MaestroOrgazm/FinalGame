using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rectTransform;
    private Vector3 _defaultScale;
    private Vector3 _enter = new Vector3(0.9f, 0.9f, 0.9f);

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultScale = _rectTransform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.localScale = _enter;
        GetComponent<AudioSource>().Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.localScale = _defaultScale;
    }
}
