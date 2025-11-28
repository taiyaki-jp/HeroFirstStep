using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class AutoEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _useEnemies = new();
    [SerializeField] private GameObject _bossEnemy;
    [SerializeField, Label("スポーン間隔")] private float _spawnRate = 1.5f;
    private SpawnManager _spawnManager;
    [SerializeField,Label("ボスを出すまでに出す雑魚の数")] private int _bossCount = 7;

    private void Start()
    {
        _spawnManager = GameObject.Find("BattleField").GetComponent<SpawnManager>();
        _ = Spawn();
    }

    private async UniTaskVoid Spawn()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_spawnRate));
        for (var i = 0; i < _bossCount; i++)
        {
            _spawnManager.CharacterSpawn(_useEnemies[Random.Range(0, _useEnemies.Count)]);
            await UniTask.Delay(TimeSpan.FromSeconds(_spawnRate));
        }
        _spawnManager.CharacterSpawn(_bossEnemy);
    }
}
