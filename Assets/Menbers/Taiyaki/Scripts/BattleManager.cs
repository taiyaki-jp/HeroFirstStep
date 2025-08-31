using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    private List<Character>[] _player = new List<Character>[4];
    private List<Character>[] _enemies = new List<Character>[4];
    private int[] _playerAttackValue = { 0, 0, 0, 0 };
    private int[] _enemyAttackValue = { 0, 0, 0, 0 };

    /// <summary>
    /// Listに追加し戦闘態勢に入る
    /// </summary>
    /// <param name="character">追加するキャラ</param>
    public void AddList(Character character)
    {
        if (character.IsPlayer)
        {
            _player[character.AttackTiming].Add(character);
            _playerAttackValue[character.AttackTiming] += character.Attack;
        }
        else
        {
            _enemies[character.AttackTiming].Add(character);
            _enemyAttackValue[character.AttackTiming] += character.Attack;
        }
    }


    /// <summary>
    /// 攻撃処理
    /// </summary>
    /// <param name="timing">どの集団に攻撃させるか</param>
    public void Attack(int timing)
    {
        //プレイヤーの攻撃処理
        var noDamageEnemy= Damage(_enemies[Random.Range(0, 3)], _playerAttackValue[timing]);

        //エネミーの攻撃処理
        var noDamagePlayer= Damage(_player[Random.Range(0, 3)], _enemyAttackValue[timing]);

        //攻撃後は死亡かノックバックで前線にいないのでリストを空にする
        _player[timing].Clear();
        _enemies[timing].Clear();
        _playerAttackValue[timing] = 0;
        _enemyAttackValue[timing] = 0;

        //運良く攻撃を受けなかったやつはそのまま前線にいる
        foreach (var character in noDamagePlayer)
        {
            AddList(character);
        }

        foreach (var character in noDamageEnemy)
        {
            AddList(character);
        }
    }

    /// <summary>
    /// 集団でのダメージ処理
    /// </summary>
    /// <param name="targetList">攻撃する対象の集団</param>
    /// <param name="damage">与える総ダメージ</param>
    /// <returns>ノーダメなやつら</returns>
    private List<Character> Damage(List<Character> targetList, int damage)
    {
        var remainingDamage = damage;
        var noDamageList = new List<Character>();
        foreach (var target in targetList)
        {
            if (remainingDamage == 0)//残ダメージが0なら
            {
                noDamageList.Add(target);//ターゲットをノーダメListに入れて
                continue;//次のループ
            }
            if (target.HP > remainingDamage) //残ダメージがターゲットのHP以下なら
            {
                target.DoDamage(remainingDamage); //ダメージを与えて
                remainingDamage =0;//残ダメージを0にして
                continue; //次のループ
            }
            //以上なら
            target.DoDamage(target.HP); //その敵のHPを0にして
            remainingDamage -= target.HP; //その分残ダメージを減らす
        }
        return noDamageList;
    }
}