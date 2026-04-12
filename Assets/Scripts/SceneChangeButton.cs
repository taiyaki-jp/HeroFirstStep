using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    private Button _button;
    [SerializeField,Scene,Label("遷移先")] private string _sceneName;
    private void Awake()
    {
        _button = this.GetComponent<Button>();
        _button.onClick.AddListener(Change);
    }

    private void Change()
    {
        SoundManager.Instance.PlaySE(SETypeEnum.Button);
        _ = FadeManager.Instance.FadeAndSceneChange<Enum>(_sceneName);
    }
}
