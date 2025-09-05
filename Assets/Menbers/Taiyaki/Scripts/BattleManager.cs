using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    private readonly HashSet<Character>[] _player = new HashSet<Character>[4];
    private readonly HashSet<Character>[] _enemies = new HashSet<Character>[4];
    private readonly int[] _playerAttackValue = { 0, 0, 0, 0 };
    private readonly int[] _enemyAttackValue = { 0, 0, 0, 0 };

    public void Init()
    {
        for (var i = 0; i < _player.Length; i++)
        {
            _player[i] = new HashSet<Character>();
            _enemies[i] = new HashSet<Character>();
        }
    }

    /// <summary>
    /// リストに追加し戦闘態勢に入る
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
    /// リストから削除し戦闘態勢を解除
    /// </summary>
    /// <param name="character">削除するキャラ</param>
    public void RemoveList(Character character)
    {
        if (character.IsPlayer)
        {
            _player[character.AttackTiming].Remove(character);
            _playerAttackValue[character.AttackTiming] -= character.Attack;
        }
        else
        {
            _enemies[character.AttackTiming].Remove(character);
            _enemyAttackValue[character.AttackTiming] -= character.Attack;
        }
    }


    /// <summary>
    /// 攻撃処理
    /// </summary>
    /// <param name="timing">どの集団に攻撃させるか</param>
    public void Attack(int timing)
    {
        for (var i = 0; i < _player.Length; i++)
        {
            _player[i].RemoveWhere(c => c == null);
            _enemies[i].RemoveWhere(c => c == null);
        }
        //プレイヤーの攻撃処理
        Damage(_enemies, Random.Range(0, 3), _playerAttackValue[timing]);
        //エネミーの攻撃処理
        Damage(_player, Random.Range(0, 3), _enemyAttackValue[timing]);

    }

    /// <summary>
    /// 集団でのダメージ処理
    /// </summary>
    /// <param name="targetList">攻撃する対象</param>
    /// <param name="targetIndex">どのグループか</param>
    /// <param name="damage">与える総ダメージ</param>
    /// <returns>ノーダメなやつら</returns>
    private void Damage(HashSet<Character>[] targetList, int targetIndex, int damage)
    {
        var remainingDamage = damage;
        var i = targetIndex;
        do //残ダメージある限りループ
        {
            //リスト内にダメージを与える
            foreach (var target in targetList[i])
            {
                if (remainingDamage == 0) break; //残ダメージが0なら//終了

                if (target.HP > remainingDamage) //残ダメージがターゲットのHP以下なら
                {
                    target.DoDamage(remainingDamage); //ダメージを与えて
                    remainingDamage = 0; //残ダメージを0にして
                    break; //終了
                }

                //以上なら
                remainingDamage -= target.HP; //ターゲットのHP分残ダメージを減らして
                target.DoDamage(target.HP); //その敵のHPを0にする
            }

            i = (i + 1) % targetList.Length;
            if (i == targetIndex) break;//一周してしまったら終了
        } while (remainingDamage > 0);
    }
}
