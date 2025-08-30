using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private List<PlayerCharactor> _playerCharactors = new ();
    private List<Enemy> _enemies = new ();

    private List<PlayerCharactor> _sortedPlayer;
    private List<Enemy> _sortedEnemy;

    private async UniTask MainTask()
    {
        while (true)
        {
            //複数いるなら近い順に並び替え
            if (_playerCharactors.Count > 1) _sortedPlayer = _playerCharactors.OrderBy(player => player.transform.position.x).ToList();
            else _sortedPlayer = _playerCharactors;

            if (_enemies.Count > 1) _sortedEnemy = _enemies.OrderByDescending(enemy => enemy.transform.position.x).ToList();
            else _sortedEnemy = _enemies;

            if ((_sortedEnemy[0].transform.position.x - _sortedPlayer[0].transform.position.x) < 0.1)//先頭同士の位置を比較し近ければ攻撃
            {
                Attack();
            }
            await UniTask.Yield();
        }
    }

    private void Attack()
    {
        var enemyHead = _sortedEnemy[0];
        var playerHead = _sortedPlayer[0];

        //プレイヤーの攻撃処理
        enemyHead.HP -= playerHead.Attack;

        //エネミーの攻撃処理
        playerHead.HP -= enemyHead.Attack;

        //攻撃後は死亡かノックバックなのでリストから削除
        _enemies.RemoveAt(0);
        _playerCharactors.RemoveAt(0);

        if (enemyHead.HP <= 0) enemyHead.Deth();
        else 
        { 
            enemyHead.DoKnockback=true; 
        }

        if (playerHead.HP <= 0) playerHead.Deth();
        else 
        {
            playerHead.DoKnockback=true; 
        }
    }

    private void AddList<T>(T charactor)where T : CharactorBase
    {

    }
}
