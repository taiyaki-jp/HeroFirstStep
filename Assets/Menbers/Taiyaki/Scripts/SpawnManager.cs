using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private BattleField _battleField;
    private Vector3 _playerHomeSpawnPoint;
    private Vector3 _enemyHomeSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        _battleField = this.GetComponent<BattleField>();
        _playerHomeSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform.position;
        _enemyHomeSpawnPoint = GameObject.Find("EnemySpawnPoint").transform.position;
    }
    /// <summary>
    /// 引数のキャラをスポーンさせる
    /// </summary>
    /// <param name="characterPrefab">スポーンさせるキャラのプレハブ</param>
    public void CharacterSpawn(GameObject characterPrefab)
    {
        if (characterPrefab.CompareTag("Player"))
        {
            SpawnPlayer(characterPrefab);
        }
        else
        {
            SpawnEnemy(characterPrefab);
        }

    }
    /// <summary>
    /// プレイヤー側のスポーン
    /// </summary>
    /// <param name="characterPrefab">スポーンさせるキャラのプレハブ</param>
    private void SpawnPlayer(GameObject characterPrefab)
    {
        var character = Instantiate(characterPrefab, _playerHomeSpawnPoint, Quaternion.identity)
            .GetComponent<Character>();
        _battleField.AddCharacter(character);
    }

    /// <summary>
    /// エネミー側のスポーン
    /// </summary>
    /// <param name="characterPrefab">スポーンさせるキャラのプレハブ</param>
    private void SpawnEnemy(GameObject characterPrefab)
    {
        var character = Instantiate(characterPrefab, _enemyHomeSpawnPoint, Quaternion.identity)
            .GetComponent<Character>();
        _battleField.AddCharacter(character);
    }
}
