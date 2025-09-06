using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField, Scene] private string _sceneName;

    //[SerializeField] TitleAudio _titleAudio;

    private void Start()
    {
        _startButton.onClick.AddListener(Change);
        SoundManager.Instance.PlayBGM(BGMAudioData.BGMType.Title);
    }

    private void Change()
    {
        //StartCoroutine(ChangeScene());
        SoundManager.Instance.EndBGM();
        SoundManager.Instance.PlaySE(SEAudioData.SEType.Button);
        _ = FadeManager.Instance.Fade<Enum>(_sceneName);
    }
/*
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.5f);
        _titleAudio.AudioStop();
        yield return new WaitForSeconds(2.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }*/
}