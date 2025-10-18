using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHome : MonoBehaviour
{
    [SerializeField] private Sprite _close;
    [SerializeField] private Sprite _helf;
    [SerializeField] private Sprite _open;
    private SpriteRenderer _renderer;
    private bool _opened;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _opened = false;
    }
    public async UniTask Open()
    {
        if (_opened) return;
        await UniTask.DelayFrame(25);
        _renderer.sprite=_helf;
        await UniTask.DelayFrame(15);
        _renderer.sprite=_open;
        _opened = true;
        _ = OpenCount();
    }
    private async UniTask OpenCount()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1.5));
    }
}
