using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _resultText;
    [SerializeField]private TextMeshProUGUI _stageText;
    [SerializeField]private string _winText;
    [SerializeField]private string _loseText;
    private void Awake()
    {
        
    }

    private void Start()
    {
        if (SingletonDatas.Instance.IsWin)
        {
            _resultText.text = _winText;
        }
        else
        {
            _resultText.text = _loseText;
        }
        _stageText.text = "ステージ1";//まだ1ステージしかないから
    }
}
