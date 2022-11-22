using UnityEngine;
using UnityEngine.UI;

public class SkillPannel : MonoBehaviour
{
    [SerializeField] private PlayerMovment _playerMovment;
    [SerializeField] private Image _image;

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
            color.a = 1;
        else
            color.a = 0.5f;

        _image.color = color;
    }
}
