using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    [SerializeField] private List<Object> _enemyList;
    [SerializeField] private Object _boss;
    private int _index;
    [SerializeField] private Vector3 _enemyPos;
    [SerializeField] private Animator _enemyGate;
    void Start()
    {
        StartCoroutine(CreateEnemys());
    }
    void Update()
    {
        if(_index == _enemyList.Count)
        {
            Instantiate(_boss, _enemyPos, Quaternion.identity);
            _index = 0;         
        }
    }
    private IEnumerator CreateEnemys()
    {

        for ( _index = 0; _index < _enemyList.Count; _index++)
        {
            _enemyGate.SetBool("Open", true);
            Instantiate(_enemyList[_index], _enemyPos, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            _enemyGate.SetBool("Open", false);
            yield return new WaitForSeconds(10.0f);
        }    
    }
}
