using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [SerializeField,Label("攻撃力")] private int _attack;
    public int Attack => _attack;
    [SerializeField,Label("HP")] private int _hp;
    public int HP => _hp;
    [SerializeField,Label("移動速度")] private float _moveSpeed;
    [SerializeField,Label("(味方)必要コスト/(敵)倒すともらえるコスト")] private int _cost;
    public int Cost => _cost;
    public bool IsPlayer { get; private set; }//プレイヤーかどうか
    public int AttackTiming { get; private set; }//攻撃タイミング分類

    private Renderer _renderer;//↓をとるため
    public Bounds Bounds { get; private set; }//現在位置

    private bool _doKnockback = false;
    public bool DoKnockback { set => _doKnockback = value; }
    

    private CancellationTokenSource _cancellationTokenSource= new ();

    private void Awake()
    {
        //どのタイミングに属すか最初に決める
        AttackTiming = Random.Range(0, 3);
        //タグからプレイヤーかどうかを見る
        IsPlayer = this.CompareTag("Player");
        _renderer = this.GetComponent<Renderer>();
    }

    private void Start() //行動開始
    {
        if (IsPlayer) _moveSpeed *= -1;//敵味方で移動方向を制御
        Bounds = _renderer.bounds;//最初の位置取得
        //_cancellationTokenSource = new CancellationTokenSource();
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
            Bounds = _renderer.bounds;
            this.transform.position = new Vector3(this.transform.position.x + _moveSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
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

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}