using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogInst : MonoBehaviour
{
    [SerializeField] private DialogView _template;

    private List<Dialog> _dialogs;
    private Dialog _currentDialog;
    private DialogTrigger _trigger;
    private PlayerMovment _movment;
    private int _index = 0;

    private void OnEnable()
    {
        _template.OnNext += NextDialog;
    }

    private void OnDisable()
    {
        _template.OnNext -= NextDialog;
    }

    public void SetDialog(List<Dialog> dialogs, DialogTrigger trigger, PlayerMovment movment)
    {
        _movment = movment;
        _template.gameObject.SetActive(true);
        _dialogs = dialogs;
        _trigger = trigger;
        InstantiateDialog();
    }

    private void InstantiateDialog()
    {
        _currentDialog = _dialogs[_index];
        _template.SetDialog(_currentDialog);
        _index++;
    }

    private void NextDialog()
    {
        if (_index < _dialogs.Count)
        {
            InstantiateDialog();
        }
        else
        {
            _template.gameObject.SetActive(false);
            _trigger.gameObject.SetActive(false);
            _movment.OnOffInput(true);
            _index = 0;
        }
    }
}
