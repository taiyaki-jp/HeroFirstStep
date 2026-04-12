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
    [SerializeField,Label("耐性")]private ResistanceData _resists;

    public string Name => _name;
    public int Attack => _attack;
    public int HP => _hp;
    public SpriteRenderer CharaRenderer { get; private set; }
    public bool IsPlayer { get; private set; }
    public CharacterState State { private get; set; }
    public int AttackTiming { get; private set; }

    public ResistanceData ResistData { get => _resists; }
    [Header("演出系")]
    [SerializeField] private GameObject _homeObjet;
    [SerializeField]private TextMeshPro _hpText;
    [SerializeField]private TextMeshProUGUI _resultText;
    private BattleField _battleField;
    private GameSceneManager _gameSceneManager;
    private CancellationTokenSource _token;
    private bool _isBreak;

    private void Awake()
    {
        CharaRenderer = GetComponent<SpriteRenderer>();
        _gameSceneManager = GameObject.Find("GameSceneManager").GetComponent<GameSceneManager>();
        _battleField = GameObject.Find("BattleField").GetComponent<BattleField>();
        IsPlayer = this.CompareTag("Player");
        AttackTiming = Random.Range(0, 3);
        _token = new CancellationTokenSource();
        _isBreak = false;
    }

    private void Start()
    {
        _battleField.AddCharacter(this);
        _hpText.text = _hp.ToString();
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
        SingletonDatas.Instance.IsWin = ! IsPlayer;//壊されたほうが動くので反転
        Destroy(_homeObjet);
        SoundManager.Instance.PlaySE(SETypeEnum.Brake);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        _gameSceneManager.GameEnd();
        _token.Cancel();
    }

    /// <summary>
    /// 被ダメ時に揺らしたり
    /// </summary>
    /// <param name="token"></param>
    private async UniTask DamageEffect(CancellationToken token)
    {
        SoundManager.Instance.PlaySE(SETypeEnum.Damage);
        Tween tween = _homeObjet.transform.DOShakePosition(0.5f);
        await tween.ToUniTask(cancellationToken: token);
    }

    private void OnDestroy()
    {
        _token.Cancel();
    }
}
