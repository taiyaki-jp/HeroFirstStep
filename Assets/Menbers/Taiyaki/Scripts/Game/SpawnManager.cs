using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private BattleField _battleField;
    private readonly GameObject[] _home = new GameObject[2];
    private readonly SpawnDoorEffect[] _spawnDoorEffect  = new SpawnDoorEffect[2];
    private readonly Vector3[] _homeSpawnPoint = new Vector3[2];
    // Start is called before the first frame update
    private void Awake()
    {
        _battleField = this.GetComponent<BattleField>();
        _home[0] = GameObject.Find("EnemyHomeRoot");
        _home[1] = GameObject.Find("PlayerHomeRoot");

        for (var i = 0; i < _home.Length; i++)
        {
            var home = _home[i];
            _spawnDoorEffect[i] = home.GetComponentInChildren<SpawnDoorEffect>();
            _homeSpawnPoint[i] = home.transform.Find("HomeChara").position;
        }
    }
    /// <summary>
    /// 引数のキャラをスポーンさせる
    /// </summary>
    /// <param name="characterPrefab">スポーンさせるキャラのプレハブ</param>
    public void CharacterSpawn(GameObject characterPrefab)
    {
        var isPlayer = Convert.ToInt32(characterPrefab.CompareTag("Player"));//プレイヤーなら1 エネミーなら0

        _spawnDoorEffect[isPlayer].Spawn();
        var character = Instantiate(characterPrefab, _homeSpawnPoint[isPlayer], Quaternion.identity)
            .GetComponent<Character>();
        _battleField.AddCharacter(character);

    }
}
