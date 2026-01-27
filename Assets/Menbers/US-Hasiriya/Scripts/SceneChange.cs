using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField, Scene] private string _sceneName;

    private void Start()
    {
        _startButton.onClick.AddListener(Change);
        SoundManager.Instance.PlayBGM(BGMTypeEnum.Title);
    }

    private void Change()
    {
        SoundManager.Instance.PlaySE(SETypeEnum.Button);
        _ = FadeManager.Instance.FadeAndSceneChange<Enum>(_sceneName);
    }
}