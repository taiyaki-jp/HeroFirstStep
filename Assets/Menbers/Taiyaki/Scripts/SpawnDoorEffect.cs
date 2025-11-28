using System;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class SpawnDoorEffect : MonoBehaviour
{
    [SerializeField,Label("ドアが開きっぱな時間")] private float _doorCloseTimerDef = 1.0f;
    [SerializeField,Label("0:全閉1:半開き,2:全開")]private Sprite[] _doorCloseSprites=new Sprite[3];

    private SpriteRenderer _renderer;
    private float _closeTimer;
    private bool _isOpen;

    private void Start()
    {
        _closeTimer = _doorCloseTimerDef;
        _renderer = this.gameObject.GetComponent<SpriteRenderer>();
        _renderer.sprite = _doorCloseSprites[0];
    }

    public void Spawn()
    {
        _closeTimer = _doorCloseTimerDef;
        if (_isOpen)return;
        _ = Door();
        _isOpen = true;
    }

    /// <summary>
    /// ドアSpriteを切り替えて開閉させる
    /// </summary>
    private async UniTask Door()
    {
        _ = DoorTimerUpdate();
        //DoorOpen
        _renderer.sprite = _doorCloseSprites[1];
        await UniTask.Delay(TimeSpan.FromSeconds(0.25));
        _renderer.sprite = _doorCloseSprites[2];
        //DoorWait
        await UniTask.WaitUntil(() => _closeTimer <= 0f);
        //DoorClose
        _renderer.sprite = _doorCloseSprites[1];
        await UniTask.Delay(TimeSpan.FromSeconds(0.4));
        _renderer.sprite = _doorCloseSprites[0];

        _isOpen = false;
    }

    private async UniTask DoorTimerUpdate()
    {
        while (_closeTimer>0)
        {
            _closeTimer -= Time.deltaTime;
            await UniTask.Yield();
        }
    }
}
