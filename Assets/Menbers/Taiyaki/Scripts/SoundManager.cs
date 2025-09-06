using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBase<SoundManager>
{
    [SerializeField] private List<BGMAudioData> _bgmClips = new();
    [SerializeField] private List<SEAudioData> _seClips = new();
    [SerializeField] private AudioSource _seAudioSource;
    [SerializeField] private AudioSource _bgmAudioSource;

    protected override void Awake()
    {
        base.Awake();
    }

    public void PlayBGM(BGMAudioData.BGMType type)
    {
        var data = _bgmClips.Find(bgm => bgm._type == type);
        if (data == null)
        {
            Debug.LogWarning("SoundManager::PlayBGM::BGM Not Found");
            return;
        }

        _bgmAudioSource.clip = data._audioClip;
        _bgmAudioSource.volume = data._volume;
        _bgmAudioSource.Play();
    }

    public void EndBGM()
    {
        _bgmAudioSource.Stop();
    }

    public void PlaySE(SEAudioData.SEType type)
    {
        var data = _seClips.Find(se => se._type == type);
        if (data == null)
        {
            Debug.LogWarning("SoundManager::PlaySE::SE Not Found");
            return;
        }

        _seAudioSource.clip = data._audioClip;
        _seAudioSource.volume = data._volume;
        _seAudioSource.Play();
    }
}

[System.Serializable]
public class BGMAudioData
{
    public enum BGMType
    {
        Title,
        Game,
    }

    public BGMType _type;
    public AudioClip _audioClip;
    [Range(0, 1)] public float _volume = 1;
}

[System.Serializable]
public class SEAudioData
{
    public enum SEType
    {
        Button,
        Damage,
        Death,
    }

    public SEType _type;
    public AudioClip _audioClip;
    [Range(0, 1)] public float _volume = 1;
}