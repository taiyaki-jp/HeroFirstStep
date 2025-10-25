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
    private TextMeshProUGUI _AtkText;
    private TextMeshProUGUI _HPText;
    private TextMeshProUGUI _CostText;

    private Character _character;
    private SpownManager _spownManager;
    // Start is called before the first frame update
    void Start()
    {
        _spownManager = GameObject.Find("BattleField").GetComponent<SpownManager>();
        _button = this.GetComponent<Button>();

        //各要素を拾ってくる
        _charaImage = this.transform.Find("Image")   .GetComponent<Image>();
        _CostText   = this.transform.Find("CostText").GetComponent<TextMeshProUGUI>();
        _AtkText    = this.transform.Find("ATKText") .GetComponent<TextMeshProUGUI>();
        _HPText     = this.transform.Find("HPText")  .GetComponent<TextMeshProUGUI>();
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
        _CostText.text += _character.Cost.ToString();
        _AtkText.text += _character.Attack.ToString();
        _HPText.text += _character.HP.ToString();


        _button.onClick.AddListener(()=> _spownManager.CharacterSpown(_characterPrefab));
    }
}
