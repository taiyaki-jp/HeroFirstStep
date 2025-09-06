using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [Header("基本ステータス")]
    [SerializeField, Label("攻撃力")] private int _attack = 10;
    public int Attack => _attack;
    [SerializeField, Label("HP")] private int _hp = 20;
    public int HP => _hp;
    [SerializeField, Label("移動速度")] private float _moveSpeed=0.3f;

    [SerializeField, Label("(味方)必要コスト/(敵)倒すともらえるコスト")]
    private int _cost = 10;

    public int Cost => _cost;
    public bool IsPlayer { get; private set; } //プレイヤーかどうか
    public int AttackTiming { get; private set; } //攻撃タイミング分類

    public enum CharacterState
    {
        Death,
        Walk,
        Battle,
        Knockback,
    }
    private CharacterState _state;
    public CharacterState State { set => _state = value; }

    private Renderer _renderer; //↓をとるため
    public Bounds Bounds { get; private set; } //現在位置

    [Header("演出系")]
    private int _moveMultiplier; //後ろ回転の方向
    private GameObject _deathMoveTo; //死亡演出でどこにすっ飛ぶか

    [SerializeField, Label("ノックバックでどれだけ飛ぶか")] private int _knockbackForce = 3;
    [SerializeField, Label("死亡演出の時間")] private float _deathEffectTime = 0.7f;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private void Awake()
    {
        //どのタイミングに属すか最初に決める
        AttackTiming = Random.Range(0, 3);
        //タグからプレイヤーかどうかを見る
        IsPlayer = this.CompareTag("Player");
        _renderer = this.GetComponent<Renderer>();
    }

    //private void Start()
    public void StartMove()
    {
        if (IsPlayer)
        {
            _moveMultiplier = -1; //後ろ回転 前進
            _deathMoveTo = GameObject.Find("PlayerDeathPoint");
        }
        else
        {
            _moveMultiplier = 1; //後ろ回転 前進
            _deathMoveTo = GameObject.Find("EnemyDeathPoint");
        }

        //行動開始
        this.GetComponent<Animator>().enabled = false;
        _state = CharacterState.Walk;
        _ = Moving(_cancellationTokenSource.Token);
    }

    private void LateUpdate()
    {
        //現在位置の当たり判定を取得
        Bounds = _renderer.bounds;
    }

    /// <summary>
    /// 基本のステート分岐(もどき？)
    /// </summary>
    /// <param name="token"></param>
    private async UniTask Moving(CancellationToken token)
    {
        while (_state != CharacterState.Death)
        {
            switch (_state)
            {
                case CharacterState.Walk:
                    await Walk(token);
                    break;
                case CharacterState.Knockback:
                    await Knockback(token);
                    break;
                case CharacterState.Battle:
                    await UniTask.WaitUntil(() => _state != CharacterState.Battle, cancellationToken: token);
                    break;
            }
        }

        await Death();
    }

    /// <summary>
    /// 歩き
    /// </summary>
    /// <param name="token"></param>
    private async UniTask Walk(CancellationToken token)
    {
        while (_state == CharacterState.Walk)
        {
            this.transform.position =
                new Vector3(this.transform.position.x + (_moveSpeed * _moveMultiplier) * Time.deltaTime,
                    this.transform.position.y, this.transform.position.z);
            await UniTask.Yield(token);
        }
    }

    /// <summary>
    /// ダメージを与える際に呼び出す
    /// </summary>
    /// <param name="damage"></param>
    public void DoDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0) _state = CharacterState.Death;
        else _state = CharacterState.Knockback;
    }

    /// <summary>
    /// ノックバックを発生させる
    /// </summary>
    private async UniTask Knockback(CancellationToken token)
    {
        SoundManager.Instance.PlaySE(SEAudioData.SEType.Damage);

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + new Vector3(-_moveMultiplier * _knockbackForce,0,0);

        Tween jump = transform.DOJump(targetPos, 1, 1, 0.4f).SetEase(Ease.OutCubic);
        Tween rotate = transform.DORotate(new Vector3(0, 0, 360 * _moveMultiplier), 0.4f, RotateMode.FastBeyond360);

        await UniTask.WhenAll(
            jump.ToUniTask(cancellationToken: token),
            rotate.ToUniTask(cancellationToken: token)
        );
        if (_state != CharacterState.Battle) _state = CharacterState.Walk; //バトルなら戦闘続行　違うなら歩き状態に
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private async UniTask Death()
    {
        _cancellationTokenSource.Cancel(); //既存UniTaskを全部中止

        SoundManager.Instance.PlaySE(SEAudioData.SEType.Death);

        Tween move = transform.DOMove(_deathMoveTo.transform.position, _deathEffectTime).SetEase(Ease.OutCubic);
        Tween rotate = transform.DORotate(new Vector3(0, 0, 720 * _moveMultiplier), _deathEffectTime,
            RotateMode.FastBeyond360);
        Tween scale = transform.DOScale(Vector3.zero, _deathEffectTime).SetEase(Ease.InQuad);

        //全部終わるまで待つ
        await UniTask.WhenAll(
            move.ToUniTask(),
            rotate.ToUniTask(),
            scale.ToUniTask()
        );

        // 全部終わったら削除
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}