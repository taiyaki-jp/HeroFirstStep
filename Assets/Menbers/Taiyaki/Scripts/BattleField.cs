using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    private int _battlePlayerCount = 0; //戦闘エリアにいるプレイヤーの総数
    private int _battleEnemyCount = 0; //戦闘エリアにいる敵の総数

    private Renderer _renderer;
    private Bounds _thisBound;

    private readonly HashSet<Character> _moveCharacter = new();//HashSetは処理が軽いらしい 順序が消える代償があるけど…
    private readonly HashSet<Character> _battleCharacter = new();//2つとも順序は関係ないからHashSet
    private BattleManager _battleManager;

    private readonly HashSet<Character> _modeChangeCharacter = new(); //バッファとして使うからhashSet

    private void Start()
    {
        _battlePlayerCount = 0;
        _battleEnemyCount = 0;
        _battleManager = new BattleManager();
        _battleManager.Init();
        _renderer = GetComponent<Renderer>();
    }

    //Rigidbody使おうとしたけどかなり動くやつの数が多いので自作
    private void LateUpdate() //Linqにできるものが多いが毎フレームするのでこのまま
    {
        _thisBound = _renderer.bounds;//毎フレーム位置を更新しないといけない
        //戦闘開始ロジック
        foreach (var character in _moveCharacter)
        {
            if (_thisBound.Intersects(character.Bounds) == false) continue; //もしキャラが戦闘エリアに被っていれば
            //戦闘開始
            _modeChangeCharacter.Add(character);
            if (character.IsPlayer)
                _battlePlayerCount++;
            else
                _battleEnemyCount++;
        }

        _battleCharacter.UnionWith(_modeChangeCharacter); //モードチェンジの奴らをバトル状態に .AddRange(_modeChangeCharacter);みたいな物
        _moveCharacter.ExceptWith(_modeChangeCharacter); //移動状態からは削除 .RemoveAll(c => _modeChangeCharacter.Contains(c));と同じ
        _modeChangeCharacter.Clear(); //↓用にリセット

        //戦闘終了ロジック
        foreach (var character in _battleCharacter)
        {
            if (_thisBound.Intersects(character.Bounds)) continue; //もしキャラが戦闘エリアから離れていれば
            _modeChangeCharacter.Add(character);
            if (character.IsPlayer)
                _battlePlayerCount--;
            else
                _battleEnemyCount--;
        }

        _moveCharacter.UnionWith(_modeChangeCharacter); //モードチェンジの奴らを移動状態に
        _battleCharacter.ExceptWith(_modeChangeCharacter); //バトル状態からは削除
        _modeChangeCharacter.Clear(); //次のUpdate用にリセット

        //戦場の内部を確認し
        if (_battleEnemyCount == 0&&_battlePlayerCount>0) //戦場に敵がいない&プレイヤーはいる
            MoveField(-1);//プレイヤー進軍

        else if (_battlePlayerCount == 0 &&_battleEnemyCount>0) //戦場にプレイヤーがいない＆敵はいる
            MoveField(1);//敵進軍

        Debug.Log($"移動:{_moveCharacter.Count}\n戦闘:{_battleCharacter.Count}");
    }

    private void MoveField(int moveDirection)
    {
        this.gameObject.transform.position += new Vector3(_moveSpeed * moveDirection, 0, 0);
        _battleManager.March();
    }

    public void AddCharacter(Character character)
    {
        _moveCharacter.Add(character);
    }
}