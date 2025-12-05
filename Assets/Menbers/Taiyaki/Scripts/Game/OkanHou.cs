using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkanHou : MonoBehaviour
{
    [SerializeField]private List<Sprite> _sprites = new List<Sprite>();
    [SerializeField] private float _moveValue;
    [SerializeField] private ParticleSystem _particle;

    private SpriteRenderer _renderer;
    private Vector3 _defaultPosition;
    private Vector3 _moveTo;

    private void Start()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _defaultPosition = this.transform.position;
        _moveTo = new Vector3(_defaultPosition.x,_defaultPosition.y+_moveValue,_defaultPosition.z);
    }

    /// <summary>
    ///オカン砲発射
    /// </summary>
    public async UniTask Fire()
    {
        Tween tween = transform.DOMove(_moveTo,1);
        await tween.ToUniTask();

        _renderer.sprite = _sprites[1];//怒る
        EffectStart();
        await UniTask.Delay(TimeSpan.FromSeconds(2));//エフェクト出した後少し留まる

        Tween backTween=transform.DOMove(_defaultPosition,1);
        await backTween.ToUniTask();

        _renderer.sprite = _sprites[0];//戻す
    }

    /// <summary>
    /// パーティクルとSEの操作はこっち
    /// </summary>
    private void EffectStart()
    {
        _particle.Play();
        SoundManager.Instance.PlaySE(SEAudioData.SEType.OkanVoice);
        SoundManager.Instance.PlaySE(SEAudioData.SEType.ShockWave);
    }
}
