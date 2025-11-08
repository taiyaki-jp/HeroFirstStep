using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkanHou : MonoBehaviour
{
    [SerializeField]private List<Sprite> _sprites = new List<Sprite>();
    [SerializeField]private Button _button;
    [SerializeField] private float _moveValue;

    private SpriteRenderer _renderer;
    [SerializeField]private ParticleSystem _particle;

    private void Start()
    {
        _button.onClick.AddListener(() => _ = Move());
        _renderer = this.GetComponent<SpriteRenderer>();
    }

    private async UniTask Move()
    {
        var defPos=this.transform.position;
        var moveTo=new Vector3(this.transform.position.x,this.transform.position.y+_moveValue,this.transform.position.z);
        Tween tween = transform.DOMove(moveTo,1);
        await tween.ToUniTask();
        await EffectStart();
        Tween backTween=transform.DOMove(defPos,1);
        await backTween.ToUniTask();
        _renderer.sprite = _sprites[0];
    }

    private async UniTask EffectStart()
    {
        _renderer.sprite = _sprites[1];
        _particle.Play();
        SoundManager.Instance.PlaySE(SEAudioData.SEType.OkanVoice);
        SoundManager.Instance.PlaySE(SEAudioData.SEType.ShockWave);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
    }
}
