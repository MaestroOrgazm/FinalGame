using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _pausePannel;
    [SerializeField] private GameObject _infoPannel;
    [SerializeField] private GameObject _shopPannel;
    [SerializeField] private GameObject _activeButton;
    [SerializeField] private GameObject _deathPannel;
    [SerializeField] private Player _player;


    private void OnEnable()
    {
        _player.PlayerDeath += ActiveDeathPannel;
    }

    private void OnDisable()
    {
        _player.PlayerDeath -= ActiveDeathPannel;
    }

    public void PlayButton()
    {
        Time.timeScale = 1;
        _pausePannel.SetActive(false);
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        _pausePannel.SetActive(true);
    }

    public void InfoButton()
    {
        _infoPannel.SetActive(true);
    }

    public void ExitInfo()
    {
        _infoPannel.SetActive(false);
    }

    public void ShopButton()
    {
        Time.timeScale = 0;
        _shopPannel.SetActive(true);
    }

    public void ExitShop()
    {
        Time.timeScale = 1;
        _shopPannel.SetActive(false);
        _activeButton.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
        LevelLoader.DeleteValue();
    }

    private void ActiveDeathPannel()
    {
        _deathPannel.SetActive(true);
    }
}
