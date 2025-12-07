using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class KakugoManager : MonoBehaviour
{
    [SerializeField,Label("ゲーム開始時の覚悟量")] private int _kakugoStartCount = 100;
    [SerializeField,Label("秒間いくつ覚悟が増えるか")] private int _kakugoAddPerSecond = 10;
    private int _kakugo;

    private TextMeshProUGUI _kakugoIntText;

    [SerializeField] private GameObject _buttonRoot;
    public readonly List<ButtonSetter> _buttons = new();

    private CancellationTokenSource _token = new();

    private void Awake()
    {
        _kakugo = _kakugoStartCount;
        _kakugoIntText = this.transform.Find("KakugoInt").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        CanSpawnCheck();
        _ = KakugoUpdate(_token.Token);
    }

    /// <summary>
    /// 毎秒覚悟を指定されただけ増やす
    /// </summary>
    /// <param name="token"></param>
    private async UniTask KakugoUpdate(CancellationToken token)
    {
        while (true)
        {
            _kakugo += _kakugoAddPerSecond;
            _kakugoIntText.text = _kakugo.ToString();
            CanSpawnCheck();
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
        }
    }

    /// <summary>
    /// 覚悟を使う(減少させる)
    /// </summary>
    /// <param name="useValue">減少させる量</param>
    public void KakugoUse(int useValue)
    {
        _kakugo -= useValue;
        _kakugoIntText.text = _kakugo.ToString();
        CanSpawnCheck();
    }

    /// <summary>
    /// 各キャラの必要コストが溜まっているかを確認
    /// </summary>
    private void CanSpawnCheck()
    {
        foreach (var button in _buttons)
        {
            if (button.Cost <= _kakugo) button.Interactable = true;
            else button.Interactable = false;
        }
    }

    private void OnDestroy()
    {
        _token.Cancel();
    }
}
