using Cysharp.Threading.Tasks;
using System.Threading;
using NaughtyAttributes;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField,Label("プレイヤー側ならチェックを入れる")] private bool _player;
    public bool IsPlayer => _player;
    [SerializeField,Label("攻撃力")] private int _attack;
    public int Attack => _attack;
    [SerializeField,Label("HP")] private int _hp;
    public int HP => _hp;
    [SerializeField,Label("移動速度")] private int _moveSpeed;
    [SerializeField,Label("(味方)必要コスト/(敵)倒すともらえるコスト")] private int _cost;
    public int Cost => _cost;
    public int AttackTiming { get; private set; }

    private bool _doKnockback = false;
    public bool DoKnockback { set => _doKnockback = value; }

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()//どのタイミングに属すか最初に決める
    {
        AttackTiming = Random.Range(0, 3);
    }

    private void Start() //行動開始
    {
        _ = Moving(_cancellationTokenSource.Token);
    }

    /// <summary>
    /// 移動に使う
    /// </summary>
    /// <param name="token"></param>
    private async UniTask Moving(CancellationToken token)
    {
        while (true)
        {
            if (_doKnockback) await Knockback(token);
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
        if (_hp <= 0)
        {
            _cancellationTokenSource.Cancel();
            _ = Death();
        }
        else
        {
            _ = Knockback(_cancellationTokenSource.Token);
        }
    }

    /// <summary>
    /// ノックバックを発生させる
    /// </summary>
    private async UniTask Knockback(CancellationToken token)
    {
        await UniTask.Yield(token);
        DoKnockback = false;
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private async UniTask Death()
    {
    }
}