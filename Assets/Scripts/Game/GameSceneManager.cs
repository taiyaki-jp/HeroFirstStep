using System;
using NaughtyAttributes;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField,Scene] private string _resultScene;
    private AutoEnemySpawner _enemySpawner;

    private void Awake()
    {
        _enemySpawner = this.GetComponent<AutoEnemySpawner>();
    }

    void Start()
    {
        FadeManager.Instance.AddAction(FadeActionMode.BeforeFade,StopGame);
        FadeManager.Instance.AddAction(FadeActionMode.FinishFade,StartGame);
    }

    public void GameEnd()
    {
        _ = FadeManager.Instance.FadeAndSceneChange<Enum>(_resultScene);
    }

    private void StartGame()
    {
        _enemySpawner.GameStart();
    }

    private void StopGame()
    {
        SoundManager.Instance.StopAllSE();
    }

    private void OnDestroy()
    {
        FadeManager.Instance.RemoveAction(FadeActionMode.BeforeFade,StopGame);
        FadeManager.Instance.RemoveAction(FadeActionMode.FinishFade,StartGame);
    }
}
