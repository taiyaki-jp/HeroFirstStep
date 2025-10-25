using System;
using UnityEngine;

public class SpownManager : MonoBehaviour
{
    private BattleField _battleField;
    private GameObject _playerHomeSpownPoint;
    private GameObject _enemyHomeSpownPoint;
    // Start is called before the first frame update
    void Start()
    {
        _battleField = this.GetComponent<BattleField>();
    }
    /// <summary>
    /// 引数のキャラをスポーンさせる
    /// </summary>
    /// <param name="characterPrefab">スポーンさせるキャラのプレハブ</param>
    /// <param name="character">そのキャラのCharacterクラス</param>
    public void CharacterSpown(GameObject characterPrefab)
    {
        var character = characterPrefab.GetComponent<Character>();
        _battleField.AddCharacter(character);
        if (characterPrefab.CompareTag("Player"))
        {
            SpownPlayer(characterPrefab);
        }
        else
        {
            SpownEnemy(characterPrefab);
        }

    }
    /// <summary>
    /// プレイヤー側のスポーン
    /// </summary>
    /// <param name="characterPrefab">スポーンさせるキャラのプレハブ</param>
    private void SpownPlayer(GameObject characterPrefab)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// エネミー側のスポーン
    /// </summary>
    /// <param name="characterPrefab">スポーンさせるキャラのプレハブ</param>
    private void SpownEnemy(GameObject characterPrefab)
    {
        throw new NotImplementedException();
    }

    
}
