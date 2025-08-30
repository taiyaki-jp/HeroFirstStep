using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private Object _otaku;
    [SerializeField] private Object _bro;
    [SerializeField] private Object _superman;
    [SerializeField] private Vector3 _playerPos;
    [SerializeField] private Animator _playerGate;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private IEnumerator Summon_Otaku()
    {
        _playerGate.SetBool("Open", true);
        Instantiate(_otaku, _playerPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        _playerGate.SetBool("Open", false);
    }
    private IEnumerator Summon_Bro()
    {
        _playerGate.SetBool("Open", true);
        Instantiate(_bro, _playerPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        _playerGate.SetBool("Open", false);
    }
    private IEnumerator Summon_SuperMan()
    {
        _playerGate.SetBool("Open", true);
        Instantiate(_superman, _playerPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        _playerGate.SetBool("Open", false);
    }
}
