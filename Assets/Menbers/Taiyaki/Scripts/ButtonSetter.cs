using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ButtonSetter : MonoBehaviour
{
    [SerializeField,Label("召喚するキャラのプレハブ")] private GameObject _characterPrefab;
    private Button _button;

    private Image _charaImage;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _atkText;
    private TextMeshProUGUI _hpText;
    private TextMeshProUGUI _costText;

    private Character _character;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("BattleField").GetComponent<SpawnManager>();
        _button = this.GetComponent<Button>();

        //各要素を拾ってくる
        _charaImage = this.transform.Find("Image")   .GetComponent<Image>();
        _costText   = this.transform.Find("CostText").GetComponent<TextMeshProUGUI>();
        _atkText    = this.transform.Find("ATKText") .GetComponent<TextMeshProUGUI>();
        _hpText     = this.transform.Find("HPText")  .GetComponent<TextMeshProUGUI>();
        _nameText   = this.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

        //拾ってきたやつを初期化
        _character = _characterPrefab.GetComponent<Character>();
        if(_character == null)//プレハブが正しくない場合終了
        {
            Debug.LogWarning($"{this.name} 召喚対象を認識できませんでした");
            return;
        }
        _charaImage.sprite = _characterPrefab.GetComponent<SpriteRenderer>().sprite;
        _nameText.text = _character.Name;
        _costText.text += _character.Cost.ToString();
        _atkText.text += _character.Attack.ToString();
        _hpText.text += _character.HP.ToString();


        _button.onClick.AddListener(()=> _spawnManager.CharacterSpawn(_characterPrefab));
    }
}
