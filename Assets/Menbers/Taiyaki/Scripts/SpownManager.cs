using System.Collections.Generic;
using UnityEngine;

public class SpownManager : MonoBehaviour
{
    private BattleField _battleField;
    [SerializeField]List<Character> _characters = new ();
    // Start is called before the first frame update
    void Start()
    {
        _battleField = GameObject.Find("BattleField").GetComponent<BattleField>();
        foreach (var character in _characters)
        {
            _battleField.AddCharacter(character);
        }
    }
}
