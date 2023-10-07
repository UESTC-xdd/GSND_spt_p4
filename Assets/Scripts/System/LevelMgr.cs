using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelMgr : Singleton<LevelMgr>, ISaveable
{
    public int CurIndex = 0;
    public int CurSceneIndex => SceneManager.GetActiveScene().buildIndex;

    public UnityAction<Scene> OnSceneLoadedAction;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.RegisterSavable();
    }

    private void OnSceneLoaded(Scene toScene, LoadSceneMode arg1)
    {
        OnSceneLoadedAction?.Invoke(toScene);
    }

    public void GenerateSaveData()
    {
        SaveMgr.Instance.CurSaveData.Index = CurIndex;

    }

    public void RestoreSaveData(SaveData data)
    {
        CurIndex = data.Index;

    }

    public void LoadLevel(int index)
    {
        StartCoroutine(LoadLevelCou(index));
    }

    [ContextMenu("加载下一关")]
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelCou(CurSceneIndex + 1));
    }

    //TODO:需要禁用的操作
    private IEnumerator LoadLevelCou(int index)
    {
        UIMgr.Instance.BG.FadeIn(1, Color.white);
        yield return new WaitUntil(() => UIMgr.Instance.BG.IsDone);
        AsyncOperation sceneOpe = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while(!sceneOpe.isDone)
        {
            //UIMgr.Instance.UpdateProgressBar(sceneOpe.progress, true);

            //假进度条
            if(sceneOpe.progress>=0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                //UIMgr.Instance.UpdateProgressBar(1, true);
                yield return new WaitForSeconds(0.5f);
            }

            yield return null;
        }
        //UIMgr.Instance.UpdateProgressBar(sceneOpe.progress, false);
        UIMgr.Instance.BG.FadeOut(1);
        yield return new WaitUntil(() =>UIMgr.Instance.BG.IsDone);
    }
}
