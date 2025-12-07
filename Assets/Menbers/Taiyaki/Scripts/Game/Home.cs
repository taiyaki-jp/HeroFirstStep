using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Home : MonoBehaviour, ICharacter
{
    [Header("基本ステータス")]
    [SerializeField,Label("名前")]private string _name="Home-none";
    [SerializeField,Label("攻撃力"),InfoBox("↓自動迎撃システムみたいなことができるかも↓")]private int _attack = 0;
    [SerializeField,Label("HP")] private int _hp = 300;

    public string Name => _name;
    public int Attack => _attack;
    public int HP => _hp;
    public SpriteRenderer CharaRenderer { get; private set; }
    public bool IsPlayer { get; private set; }
    public CharacterState State { private get; set; }
    public int AttackTiming { get; private set; }

    [SerializeField] private GameObject _homeObjet;
    [SerializeField]private TextMeshPro _hpText;
    [SerializeField,Scene] private string _resultScene;
    [SerializeField]private TextMeshProUGUI _resultText;
    private BattleField _battleField;
    private CancellationTokenSource _token;
    private bool _isBreak;

    private void Awake()
    {
        CharaRenderer = GetComponent<SpriteRenderer>();
        IsPlayer = this.CompareTag("Player");
        AttackTiming = Random.Range(0, 3);
        _token = new CancellationTokenSource();
    }

    private void Start()
    {
        _battleField = GameObject.Find("BattleField").GetComponent<BattleField>();
        _battleField.AddCharacter(this);
        _hpText.text = _hp.ToString();
        _isBreak = false;
    }

    public void DoDamage(int damage)
    {
        if(_isBreak)return;
        _hp -= damage;
        _hpText.text = _hp.ToString();
        if (_hp <= 0)
        {
            _isBreak = true;
            _ = Break();
        }
        else
        {
            // ここで前回のをキャンセル
            _token.Cancel();
            _token.Dispose();

            // 新しいトークン発行
            _token = new CancellationTokenSource();

            _ = DamageEffect(_token.Token);
        }
    }

    public void DoStan(float stanTime)
    {
        //拠点はスタン効果を受けない
    }

    /// <summary>
    /// 破壊エフェクトからシーンフェードまで
    /// </summary>
    private async UniTask Break()
    {
        SingletonDatas.Instance.IsWin = IsPlayer;
        Destroy(_homeObjet);
        SoundManager.Instance.PlaySE(SEAudioData.SEType.Brake);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        _ = FadeManager.Instance.Fade<Enum>(_resultScene);
        _token.Cancel();
    }

    /// <summary>
    /// 被ダメ時に揺らしたり
    /// </summary>
    /// <param name="token"></param>
    private async UniTask DamageEffect(CancellationToken token)
    {
        SoundManager.Instance.PlaySE(SEAudioData.SEType.Damage);
        Tween tween = _homeObjet.transform.DOShakePosition(0.5f);
        await tween.ToUniTask(cancellationToken: token);
    }

    private void OnDestroy()
    {
        _token.Cancel();
    }
}
