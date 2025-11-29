using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
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
    private BattleField _battleField;
    private CancellationTokenSource _token;

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
    }

    public void DoDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
            Break();
        else
        {
            // ここで前回のをキャンセル
            _token?.Cancel();
            _token?.Dispose();

            // 新しいトークン発行
            _token = new CancellationTokenSource();

            _ = DamageEffect(_token.Token);
        }
    }

    private void Break()
    {
        Destroy(_homeObjet);
    }

    private async UniTask DamageEffect(CancellationToken token)
    {
        Tween tween = _homeObjet.transform.DOShakePosition(0.5f);
        await tween.ToUniTask(cancellationToken: token);
    }
}
