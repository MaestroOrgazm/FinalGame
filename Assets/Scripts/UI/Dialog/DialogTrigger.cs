using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private List<Dialog> _dialogsLsit;

    PlayerMovment _playerMovment;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DialogInst>(out DialogInst dialogInst))
        {
            _playerMovment = dialogInst.gameObject.GetComponent<PlayerMovment>();
            _playerMovment.OnOffInput(false);
            dialogInst.SetDialog(_dialogsLsit, this, _playerMovment);
        }
    }
}
