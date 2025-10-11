using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetter : MonoBehaviour
{
    [SerializeField,Label("召喚するキャラのプレハブ")] private GameObject _characterPrefab;
    private Image _charaImage;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _AtkText;
    private TextMeshProUGUI _HPText;
    private TextMeshProUGUI _CostText;

    private Character _character;
    // Start is called before the first frame update
    void Start()
    {
        _charaImage = this.transform.Find("Image")   .GetComponent<Image>();
        _CostText   = this.transform.Find("CostText").GetComponent<TextMeshProUGUI>();
        _AtkText    = this.transform.Find("ATKText") .GetComponent<TextMeshProUGUI>();
        _HPText     = this.transform.Find("HPText")  .GetComponent<TextMeshProUGUI>();
        _nameText   = this.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

        _character = _characterPrefab.GetComponent<Character>();
        if(_character == null)
        {
            Debug.LogWarning($"{this.name} 召喚対象を認識できませんでした");
            return;
        }
        //_charaImage.sprite = _character.image;
        //_nameText.text = _character.CharaName;
        _CostText.text = _character.Cost.ToString();
        _AtkText.text = _character.Attack.ToString();
        _HPText.text = _character.HP.ToString();
    }
}
