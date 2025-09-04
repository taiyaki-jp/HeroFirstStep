using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    private readonly List<Character>[] _player = new List<Character>[4];
    private readonly List<Character>[] _enemies = new List<Character>[4];
    private readonly int[] _playerAttackValue = { 0, 0, 0, 0 };
    private readonly int[] _enemyAttackValue = { 0, 0, 0, 0 };

    public void Init()
    {
        for (var i = 0; i < _player.Length; i++)
        {
            _player[i] = new List<Character>();
            _enemies[i] = new List<Character>();
        }
    }

    /// <summary>
    /// Listに追加し戦闘態勢に入る
    /// </summary>
    /// <param name="characterObject">追加するキャラ</param>
    public void AddList(GameObject characterObject)
    {
        var character = characterObject.GetComponent<Character>();
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
        var noDamageEnemy = Damage(_enemies, Random.Range(0, 3), _playerAttackValue[timing]);

        //エネミーの攻撃処理
        var noDamagePlayer = Damage(_player, Random.Range(0, 3), _enemyAttackValue[timing]);

        //攻撃後は死亡かノックバックで前線にいないのでリストを空にする
        _player[timing].Clear();
        _enemies[timing].Clear();
        _playerAttackValue[timing] = 0;
        _enemyAttackValue[timing] = 0;

        //運良く攻撃を受けなかったやつはそのまま前線にいる
        foreach (var character in noDamagePlayer)
        {
            AddList(character.gameObject);
        }

        foreach (var character in noDamageEnemy)
        {
            AddList(character.gameObject);
        }
    }

    /// <summary>
    /// 集団でのダメージ処理
    /// </summary>
    /// <param name="targetList">攻撃する対象</param>
    /// <param name="targetIndex">どのグループか</param>
    /// <param name="damage">与える総ダメージ</param>
    /// <returns>ノーダメなやつら</returns>
    private List<Character> Damage(List<Character>[] targetList, int targetIndex, int damage)
    {
        var remainingDamage = damage;
        var i = targetIndex;
        var noDamageList = new List<Character>();
        do //残ダメージある限りループ
        {
            //リスト内にダメージを与える
            foreach (var target in targetList[i])
            {
                if (remainingDamage == 0) //残ダメージが0なら
                {
                    noDamageList.Add(target); //ターゲットをノーダメListに入れて
                    continue; //次のループ
                }

                if (target.HP > remainingDamage) //残ダメージがターゲットのHP以下なら
                {
                    target.DoDamage(remainingDamage); //ダメージを与えて
                    remainingDamage = 0; //残ダメージを0にして
                    continue; //次のループ
                }

                //以上なら
                remainingDamage -= target.HP; //ターゲットのHP分残ダメージを減らして
                target.DoDamage(target.HP); //その敵のHPを0にする
                
            }

            i = (i + 1) % targetList.Length;
            if (i == targetIndex) break;//一周してしまったら終了
        } while (remainingDamage > 0);

        return noDamageList;
    }

    public void March()
    {
        
    }
}
