using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameRunning { get; private set; }

    private void Awake() => SetSingleton();

    public void Run()
    {
        LockCursor();
        Time.timeScale = 1;
        IsGameRunning = true;
    }

    public void Stop()
    {
        UnlockCursor();
        Time.timeScale = 0;
        IsGameRunning = false;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GameOver()
    {
        Stop();
    }

    private void SetSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }
}
