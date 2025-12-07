using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ButtonSetter : MonoBehaviour
{
    [SerializeField,Label("召喚するキャラのプレハブ")] private GameObject _characterPrefab;
    private Button _button;
    public bool Interactable
    {
        get => _button.interactable;
        set => _button.interactable = value;
    }

    private Image _charaImage;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _atkText;
    private TextMeshProUGUI _hpText;
    private TextMeshProUGUI _costText;

    private Character _character;
    private SpawnManager _spawnManager;
    private KakugoManager _kakugoManager;

    public int Cost{get; private set;}
    // Start is called before the first frame update
    private void Awake()
    {
        _spawnManager = GameObject.Find("BattleField").GetComponent<SpawnManager>();
        _kakugoManager = GameObject.Find("KakugoBG").GetComponent<KakugoManager>();
        _button = this.GetComponent<Button>();

        //各要素を拾ってくる
        _charaImage = this.transform.Find("Image").GetComponent<Image>();
        _costText = this.transform.Find("CostText").GetComponent<TextMeshProUGUI>();
        _atkText = this.transform.Find("ATKText").GetComponent<TextMeshProUGUI>();
        _hpText = this.transform.Find("HPText").GetComponent<TextMeshProUGUI>();
        _nameText = this.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

        //拾ってきたやつを初期化
        _character = _characterPrefab.GetComponent<Character>();
        if (_character == null) //プレハブが正しくない場合終了
        {
            Debug.LogWarning($"{this.name} 召喚対象を認識できませんでした");
        }
    }

    private void Start(){
        _charaImage.sprite = _characterPrefab.GetComponent<SpriteRenderer>().sprite;
        _nameText.text = _character.Name;
        _costText.text += _character.Cost.ToString();
        _atkText.text += _character.Attack.ToString();
        _hpText.text += _character.HP.ToString();

        Cost = _character.Cost;
        _kakugoManager._buttons.Add(this);
        _button.onClick.AddListener(()=>
        {
            _spawnManager.CharacterSpawn(_characterPrefab);
            _kakugoManager.KakugoUse(Cost);
        });
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
