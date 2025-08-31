using UnityEngine;

public class BattleField : MonoBehaviour
{
    private int _battlePlayerCount = 0;
    private int _battleEnemyCount = 0;

    private void Start()
    {
        _battlePlayerCount = 0;
        _battleEnemyCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _battlePlayerCount++;
            if (_battleEnemyCount==0)
            {
                
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            _battleEnemyCount++;
        }
    }
}