using System;
using UnityEngine;

public class SpownManager : MonoBehaviour
{
    private BattleField _battleField;
    // Start is called before the first frame update
    void Start()
    {
        _battleField = this.GetComponent<BattleField>();
    }
    public void CharacterSpown(GameObject characterPrefab,Character character)
    {
        if (character != null) 
        {
            Debug.LogWarning("キャラではない物をスポーンしようとしました");
            return; 
        }
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
    private void SpownPlayer(GameObject characterPrefab)
    {
        throw new NotImplementedException();
    }

    private void SpownEnemy(GameObject characterPrefab)
    {
        throw new NotImplementedException();
    }

    
}
