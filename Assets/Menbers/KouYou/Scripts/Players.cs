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
    [SerializeField]private BattleField _battleField; 
    private IEnumerator Summon_Otaku()
    {
        _playerGate.SetBool("Open", true);
        GameObject chara = (GameObject)Instantiate(_otaku, _playerPos, Quaternion.identity);
        _battleField.AddCharacter(chara.GetComponent<Character>());
        yield return new WaitForSeconds(0.5f);
        _playerGate.SetBool("Open", false);
    }
    private IEnumerator Summon_Bro()
    {
        _playerGate.SetBool("Open", true);
        GameObject chara = (GameObject)Instantiate(_bro, _playerPos, Quaternion.identity);
        _battleField.AddCharacter(chara.GetComponent<Character>());
        yield return new WaitForSeconds(0.5f);
        _playerGate.SetBool("Open", false);
    }
    private IEnumerator Summon_SuperMan()
    {
        _playerGate.SetBool("Open", true);
        GameObject chara = (GameObject)Instantiate(_superman, _playerPos, Quaternion.identity);
        _battleField.AddCharacter(chara.GetComponent<Character>());
        yield return new WaitForSeconds(0.5f);
        _playerGate.SetBool("Open", false);
    }

    public void StartSummon_Otaku()
    {
        StartCoroutine(Summon_Otaku());
    }
    public void StartSummon_Bro()
    {
        StartCoroutine(Summon_Bro());
    }
    public void StartSummon_SuperMan()
    {
        StartCoroutine(Summon_SuperMan());
    }
}
