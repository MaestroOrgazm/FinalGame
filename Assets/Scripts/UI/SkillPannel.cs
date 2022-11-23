using UnityEngine;
using UnityEngine.UI;

public class SkillPannel : MonoBehaviour
{
    [SerializeField] private PlayerMovment _playerMovment;
    [SerializeField] private Image _image;

    private float _maxAlpha = 1;
    private float _minAlpha = 0.5f;

    private void OnEnable()
    {
        _playerMovment.SkillReady += ChangePannel;
    }

    private void OnDisable()
    {
        _playerMovment.SkillReady -= ChangePannel;
    }

    private void ChangePannel(bool ready)
    {
        var color = _image.color;

        if (ready)
            color.a = _maxAlpha;
        else
            color.a = _minAlpha;

        _image.color = color;
    }
}
