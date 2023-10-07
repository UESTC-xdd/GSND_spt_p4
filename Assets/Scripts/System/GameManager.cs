using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using StarterAssets;

public class GameManager : Singleton<GameManager>
{
    public FirstPersonController PlayerC;

    private GameMode m_CurGameMode = GameMode.GAME;
    public GameMode CurGameMode
    {
        get { return m_CurGameMode; }
    }

    public UnityAction<GameMode> OnEnterModeAction;
    public UnityAction<GameMode> OnLeaveModeAction;

    protected override void Awake()
    {
        base.Awake();
        GameObject player = GameObject.FindWithTag("Player");
        PlayerC = player.GetComponent<FirstPersonController>();
    }

    private void Start()
    {
        LevelMgr.Instance.OnSceneLoadedAction -= OnSceneLoaded;
        LevelMgr.Instance.OnSceneLoadedAction += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene toScene)
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerC = player.GetComponent<FirstPersonController>();
    }

    [ContextMenu("测试")]
    public void ChangeToMovieMode()
    {
        UpdateGameMode(GameMode.MOVIE);
    }

    public void ChangeToGameMode()
    {
        UpdateGameMode(GameMode.GAME);
    }

    public void UpdateGameMode(GameMode toMode)
    {
        Debug.Log(toMode);
        if(toMode!=m_CurGameMode)
        {
            switch (toMode)
            {
                case GameMode.GAME:
                    {
                        OnEnterModeAction?.Invoke(toMode);
                        m_CurGameMode = toMode;
                        InputMgr.Instance.EnableInput();
                        break;
                    }
                case GameMode.MOVIE:
                    {
                        OnEnterModeAction?.Invoke(toMode);
                        m_CurGameMode = toMode;
                        InputMgr.Instance.DisableInput();
                        break;
                    }
                default:
                    break;
            }
        }


    }

    public void GamePause()
    {

    }

    public void GameResume()
    {
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

public enum GameMode
{
    GAME,
    MOVIE
}
