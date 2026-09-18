using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtonController : MonoBehaviour
{
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _toTitleButton;
    [SerializeField] private Button _exitGameButton;

    private void OnEnable()
    {
        BindButtonEvents();
    }

    private void OnDisable()
    {
        UnbindButtonEvents();
    }

    private void BindButtonEvents()
    {
        _tryAgainButton.onClick.AddListener(LoadGameScene);
        _toTitleButton.onClick.AddListener(LoadTitleScene);
    }

    private void UnbindButtonEvents()
    {
        _tryAgainButton.onClick.RemoveListener(LoadGameScene);
        _toTitleButton.onClick.RemoveListener(LoadTitleScene);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.Run();
    }

    private void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
    }
}
