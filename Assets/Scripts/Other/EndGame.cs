using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject _endPannel;
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _audioSource.Play();
            _endPannel.SetActive(true);
            Time.timeScale = 0;
            LevelLoader.DeleteValue();
        }
    }
}
